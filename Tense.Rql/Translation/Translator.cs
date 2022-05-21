using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Tense.Rql
{
	/// <summary>
	/// Mapper
	/// </summary>
	public class Translator
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IMapper _mapper;

		/// <summary>
		/// Instantiates a translator
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <param name="mapper">The <see cref="IMapper"/> used to translate from entity to resource models, and vice versa.</param>
		public Translator(IServiceProvider serviceProvider, IMapper mapper)
        {
			_serviceProvider = serviceProvider;
			_mapper = mapper;
        }

        #region Translation Functions
        #region Translate Member Functions
        /// <summary>
        /// Returns the list of entity members that are used to construct this resource member
        /// </summary>
        /// <typeparam name="TResource">The resource type</typeparam>
        /// <param name="resourceMember">The resource member property node</param>
        /// <returns></returns>
        public IEnumerable<string> TranslateMemberR2E<TResource>(in RqlNode resourceMember)
		{
			return TranslateMemberR2E(typeof(TResource), resourceMember.NonNullValue<string>(0));
		}

		/// <summary>
		/// Returns the list of entity members that are used to construct this resource member
		/// </summary>
		/// <param name="TResource">The resource type</param>
		/// <param name="resourceMember">The resource member name</param>
		/// <returns></returns>
		public IEnumerable<string> TranslateMemberR2E(Type TResource, string resourceMember)
		{
			var entityAttribute = TResource.GetCustomAttribute<EntityAttribute>();

			if (entityAttribute != null)
			{
				var resourceMemberName = resourceMember;
				var propertyName = FindPropertyName(TResource, resourceMemberName);

				if (string.IsNullOrWhiteSpace(propertyName))
					return Array.Empty<string>();

				var plan = _mapper.ConfigurationProvider.BuildExecutionPlan(entityAttribute.EntityType, TResource);
				var visitor = new MemberExpressionVistor(TResource, entityAttribute.EntityType);

				visitor.Visit(plan);

				foreach (var translation in visitor.MemberTranslations)
                {
					if ( translation.Key.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
                    {
						return translation.Value.EntityMembers.ToArray();
                    }
                }

				return Array.Empty<string>();
			}

			throw new InvalidCastException($"The class {TResource.Name} is not a resource model");
		}
        #endregion

        #region Translate Value Functions
        /// <summary>
        /// Translate value from resource to entity
        /// </summary>
        /// <typeparam name="TResource">The resource type</typeparam>
        /// <param name="resourceMember">The resource property node</param>
        /// <param name="entityMember">The entity member</param>
        /// <param name="originalValue">The original resource value</param>
        /// <returns></returns>
        public object TranslateValueR2E<TResource>(RqlNode resourceMember, string entityMember, object originalValue)
		{
			if (typeof(TResource).GetCustomAttribute<EntityAttribute>() is not null)
			{
				if (GetInstance<TResource>() is TResource resource)
				{
					if (GetResourceProperty<TResource>(resourceMember) is PropertyInfo resourceProperty)
					{
						if (resourceProperty.PropertyType == typeof(DateTimeOffset) &&
							 originalValue.GetType() == typeof(DateTime))
						{
							resourceProperty.SetValue(resource, new DateTimeOffset((DateTime)originalValue));
						}
						else if (resourceProperty.PropertyType == typeof(DateTime) &&
							 originalValue.GetType() == typeof(DateTimeOffset))
						{
							resourceProperty.SetValue(resource, ((DateTimeOffset)originalValue).UtcDateTime);
						}
						else
							resourceProperty.SetValue(resource, originalValue);

						if (_mapper.Map(resource, typeof(TResource), GetEntityType<TResource>()) is object entity)
						{
							var entityProperty = entity.GetType().GetProperty(entityMember);
							return entityProperty.GetValue(entity);
						}
					}
				}
			}

			throw new InvalidCastException($"The class {typeof(TResource).Name} is not a resource model.");
		}

		/// <summary>
		/// Translate a query string from resource to Entity
		/// </summary>
		/// <typeparam name="TResource">The domain type</typeparam>
		/// <param name="queryString">The query string</param>
		/// <returns></returns>
		public RqlNode TranslateQueryStringR2E<TResource>(string? queryString)
		{
			return TranslateQueryR2E<TResource>(RqlParser.Parse(queryString));
		}
		#endregion

		#region Translate Query Functions
		/// <summary>
		/// Translate an RQL Statement obtained from the query string of the request from Resource to Entity representation
		/// </summary>
		/// <typeparam name="TResource">The Resource type</typeparam>
		/// <param name="request">The <see cref="HttpRequestMessage"/> that contains the query string.</param>
		/// <returns></returns>
		public RqlNode TranslateQueryStringR2E<TResource>(HttpRequestMessage request)
		{
			var queryString = HttpUtility.UrlDecode(request.RequestUri.Query);
			return TranslateQueryR2E<TResource>(RqlParser.Parse(queryString));
		}

		/// <summary>
		/// Translate an RQL Statement from Resource to Entity representation
		/// </summary>
		/// <typeparam name="TResource">The Resource type</typeparam>
		/// <param name="rqlStatement">The RQL Statement to translate.</param>
		/// <returns></returns>
		public RqlNode TranslateRQLR2E<TResource>(string rqlStatement)
		{
			var queryString = HttpUtility.UrlDecode(rqlStatement);
			return TranslateQueryR2E<TResource>(RqlParser.Parse(queryString));
		}

		/// <summary>
		/// Translate an RqlNode from resource to entity
		/// </summary>
		/// <typeparam name="TResource">The Resource Type</typeparam>
		/// <param name="node">The <see cref="RqlNode"/> that represents the query</param>
		/// <returns></returns>
		public RqlNode TranslateQueryR2E<TResource>(RqlNode node)
		{
			if (node.Operation == RqlOperation.NOOP)
				return node;

			RqlNode? result = null;
			var entityAttribute = typeof(TResource).GetCustomAttribute<EntityAttribute>();

			if (entityAttribute != null)
			{
				switch (node.Operation)
				{
					case RqlOperation.AND:
						result = new RqlNode(RqlOperation.AND);

						for (int i = 0; i < node.Count; i++)
						{
							if (TranslateQueryR2E<TResource>(node.NonNullValue<RqlNode>(i)) is RqlNode translated)
								result.Add(translated);
						}
						break;

					case RqlOperation.OR:
						result = new RqlNode(RqlOperation.OR);

						for (int i = 0; i < node.Count; i++)
						{
							if (TranslateQueryR2E<TResource>(node.NonNullValue<RqlNode>(i)) is RqlNode translated)
								result.Add(translated);
						}
						break;

					case RqlOperation.VALUES:
						{
							result = new RqlNode(RqlOperation.VALUES);

							for (int i = 0; i < node.Count; i++)
							{
								if (TranslateQueryR2E<TResource>(node.NonNullValue<RqlNode>(i)) is RqlNode translated)
									result.Add(translated);
							}
						}
						break;

					case RqlOperation.EQ:
					case RqlOperation.NE:
					case RqlOperation.LT:
					case RqlOperation.LE:
					case RqlOperation.GT:
					case RqlOperation.GE:
						{
							result = new RqlNode(node.Operation);

							var subNode = node.Value<RqlNode>(0);

							if (subNode != null)
							{
								var destinationMembers = TranslateMemberR2E<TResource>(subNode);

								if (destinationMembers.Count() == 1)
								{
									var translatedNode = TranslateQueryR2E<TResource>(subNode);

									if (translatedNode != null)
									{
										result.Add(translatedNode);

										if (result.NonNullValue<RqlNode>(0).Count == 1)
										{
											var destinationMember = destinationMembers.ToList()[0];
											var objectValue = node.NonNullValue<object>(1);
											var value = TranslateValueR2E<TResource>(subNode, destinationMember, objectValue);

											result.Add(value);
										}
										else if (result.NonNullValue<RqlNode>(0).Count == subNode.Count)
										{
											var objectValue = node.NonNullValue<object>(1);
											result.Add(objectValue);
										}
									}
								}
								else
								{
									var nodeList = new List<RqlNode>();

									foreach (var destinationMember in destinationMembers)
									{
										var value = TranslateValueR2E<TResource>(node.NonNullValue<RqlNode>(0), destinationMember, node.NonNullValue<object>(1));

										var childNode = new RqlNode(node.Operation);
										childNode.Add(new RqlNode(RqlOperation.PROPERTY));
										childNode.NonNullValue<RqlNode>(0).Add(destinationMember);
										childNode.Add(value);

										nodeList.Add(childNode);
									}

									result = new RqlNode(RqlOperation.AND);
									foreach (var childNode in nodeList)
										result.Add(childNode);
								}
							}
						}
						break;

					case RqlOperation.LIKE:
					case RqlOperation.CONTAINS:
					case RqlOperation.EXCLUDES:
					case RqlOperation.IN:
					case RqlOperation.OUT:
						{
							result = new RqlNode(node.Operation);
							if (TranslateQueryR2E<TResource>(node.NonNullValue<RqlNode>(0)) is RqlNode translatedNode)
								result.Add(translatedNode);

							for (int i = 1; i < node.Count; i++)
							{
								var entityMembers = TranslateMemberR2E<TResource>(node.NonNullValue<RqlNode>(0));

								foreach (var entityMember in entityMembers)
								{
									var entityValue = TranslateValueR2E<TResource>(node.NonNullValue<RqlNode>(0), entityMember, node.NonNullValue<object>(i));
									result.Add(entityValue);
								}
							}
						}
						break;

					case RqlOperation.SORT:
						{
							result = new RqlNode(RqlOperation.SORT);

							foreach (RqlNode member in node)
							{
								var newMember = new RqlNode(RqlOperation.SORTPROPERTY);
								newMember.Add(member.NonNullValue<RqlSortOrder>(0));

								if (TranslateQueryR2E<TResource>(member.NonNullValue<RqlNode>(1)) is RqlNode translatedNode)
								{
									newMember.Add(translatedNode);
									result.Add(newMember);
								}
								else
									throw new ArgumentException("Invalid RQL Syntax");
							}
						}
						break;

					case RqlOperation.SELECT:
						{
							result = new RqlNode(RqlOperation.SELECT);

							foreach (RqlNode member in node)
							{
								if ( TranslateQueryR2E<TResource>(member) is RqlNode translated )
									result.Add(translated);
							}
						}
						break;

					case RqlOperation.COUNT:
						{
							result = new RqlNode(RqlOperation.COUNT);

							for (int i = 0; i < node.Count; i++)
							{
								if (TranslateQueryR2E<TResource>(node.NonNullValue<RqlNode>(i)) is RqlNode translated)
									result.Add(translated);
							}
						}
						break;

					case RqlOperation.MIN:
					case RqlOperation.MAX:
					case RqlOperation.MEAN:
					case RqlOperation.SUM:
						{
							result = new RqlNode(node.Operation);

							for (int i = 0; i < node.Count; i++)
							{
								if (TranslateQueryR2E<TResource>(node.NonNullValue<RqlNode>(i)) is RqlNode translated)
									result.Add(translated);
							}
						}
						break;

					case RqlOperation.AGGREGATE:
						{
							result = new RqlNode(RqlOperation.AGGREGATE);

							for (int i = 0; i < node.Count; i++)
							{
								if (TranslateQueryR2E<TResource>(node.NonNullValue<RqlNode>(i)) is RqlNode translated)
									result.Add(translated);
							}
						}
						break;

					case RqlOperation.LIMIT:
						{
							result = new RqlNode(RqlOperation.LIMIT);

							for (int i = 0; i < node.Count; i++)
							{
								var value = node.NonNullValue<object>(i);
								result.Add(value);
							}
						}
						break;

					case RqlOperation.PROPERTY:
						{
							var destinationMembers = TranslateMemberR2E<TResource>(node);

							if (destinationMembers.Count() == 1)
							{
								result = new RqlNode(RqlOperation.PROPERTY);
								result.Add(destinationMembers.ToList()[0]);

								var entityNameList = GetEntitySubNames(typeof(TResource), entityAttribute.EntityType, node);
								foreach (var name in entityNameList)
									result.Add(name);
							}
						}
						break;

					case RqlOperation.SORTPROPERTY:
						{
							var destinationMembers = TranslateMemberR2E<TResource>(node);

							if (destinationMembers.Count() == 1)
							{
								result = new RqlNode(RqlOperation.PROPERTY);
								result.Add(destinationMembers.ToList()[0]);

								var entityNameList = GetEntitySubNames(typeof(TResource), entityAttribute.EntityType, node);
								foreach (var name in entityNameList)
									result.Add(name);
							}
						}
						break;

					case RqlOperation.DISTINCT:
						result = new RqlNode(RqlOperation.DISTINCT);
						break;

					case RqlOperation.FIRST:
						result = new RqlNode(RqlOperation.FIRST);
						break;

					case RqlOperation.ONE:
						result = new RqlNode(RqlOperation.ONE);
						break;
				}
			}
			else
				throw new InvalidCastException($"The class {typeof(TResource).Name} is not a domain model.");

			return result ?? new RqlNode(RqlOperation.NOOP);
		}
        #endregion
        #endregion

        #region Translate Key Functions
        /// <summary>
        /// Translates a list of key/value pairs from resource to entity representation
        /// </summary>
        /// <param name="kvp">The list of key/value pairs to translate</param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, object>> TranslateKeysR2E<TResource>(IEnumerable<KeyValuePair<string, object>> kvp)
		{
			var newList = new List<KeyValuePair<string, object>>();

			foreach (var pair in kvp)
			{
				var resourceMember = new RqlNode(RqlOperation.PROPERTY);
				resourceMember.Add(pair.Key);

				var entityMembers = TranslateMemberR2E<TResource>(resourceMember);

				foreach (var entityMember in entityMembers)
				{
					var value = TranslateValueR2E<TResource>(resourceMember, entityMember, pair.Value);
					newList.Add(new KeyValuePair<string, object>(entityMember, value));
				}
			}

			return newList;
		}
        #endregion

        #region Translate Values
		/// <summary>
		/// Translates an object value to a specified type
		/// </summary>
		/// <param name="propertyType">The type of value to return</param>
		/// <param name="value">The original value</param>
		/// <returns></returns>
		/// <exception cref="InvalidCastException"></exception>
        public object? TranslateValue(Type propertyType, object? value)
		{
			if (value == default)
				return default;

			if (propertyType.IsArray)
			{
				if (propertyType == typeof(byte[]))
                {
                    return TranslateByteArray(value);
                }
                else if (propertyType == typeof(sbyte[]))
                {
                    return TranslateSByteArray(value);
                }
                else if (propertyType == typeof(char[]))
                {
                    return TranslateCharArray(value);
                }
                else
					throw new InvalidCastException($"Unrecognized data type.");
			}
			else if (propertyType.IsEnum)
            {
                return TranslateEnumValue(propertyType, value);
            }
            else if (propertyType == typeof(bool))
            {
                return TranslateBooleanValue(value);
            }
            else if (propertyType == typeof(byte))
            {
                return TranslateByteValue(value);
            }
            else if (propertyType == typeof(sbyte))
            {
                return TranslateSByteValue(value);
            }
            else if (propertyType == typeof(short))
            {
                return TranslateShortValue(value);
            }
            else if (propertyType == typeof(ushort))
            {
                return TranslateUShortValue(value);
            }
            else if (propertyType == typeof(int))
            {
                return TranslateIntValue(value);
            }
            else if (propertyType == typeof(uint))
            {
                return TranslateUintValue(value);
            }
            else if (propertyType == typeof(long))
            {
                return TranslateLongValue(value);
            }
            else if (propertyType == typeof(ulong))
            {
                return TranslateULongValue(value);
            }
            else if (propertyType == typeof(float))
            {
                return TranslateFloatValue(value);
            }
            else if (propertyType == typeof(double))
            {
                return TranslateDoubleValue(value);
            }
            else if (propertyType == typeof(decimal))
            {
                return TranslateDecimalValue(value);
            }
            else if (propertyType == typeof(char))
            {
                return TranslateCharValue(value);
            }
            else if (propertyType == typeof(Guid))
            {
                return TranslateGuidValue(value);
            }
            else if (propertyType == typeof(DateTime))
            {
                return TranslateDateTimeValue(value);
            }
            else if (propertyType == typeof(DateTimeOffset))
            {
                return TranslateDateTimeOffsetValue(value);
            }
			else if (propertyType == typeof(TimeSpan))
			{
				return TranslateTimeSpanValue(value);
			}
			else if (propertyType == typeof(Uri))
			{
				return TranslateUriValue(value);
			}
			else if (propertyType == typeof(string))
			{
				return TranslateStringValue(value);
			}
			else if (propertyType == typeof(Image))
			{
				return TranslateImageValue(value);
			}
			else
				throw new InvalidCastException("Unrecognized property value.");
		}

		internal object? TranslateCharArray(object value)
        {
            if (value == null)
                return null;

            else if (value.GetType() == typeof(string))
            {
                return value.ToString().ToArray();
            }

            else if (value.GetType() == typeof(char[]))
                return (char[])value;

			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.String)
				{
					return element.GetString().ToArray();
				}
				else if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.Array)
				{
					var theArray = new List<char>();

					for (int i = 0; i < element.GetArrayLength(); i++)
					{
						var jsonElement = element.EnumerateArray().ElementAtOrDefault(i);

						if (jsonElement.ValueKind == JsonValueKind.String)
						{
							theArray.AddRange(jsonElement.GetString().ToArray());
						}
						else
							throw new InvalidCastException($"Cannot cast value {value} to char array.");
					}

					return theArray.ToArray();
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to char array.");
			}

			else
				throw new InvalidCastException($"Cannot cast value {value} to char array.");
        }

		internal object? TranslateSByteArray(object value)
        {
            if (value == null)
                return null;

            else if (value.GetType() == typeof(sbyte[]))
                return (sbyte[])value;

			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.Array)
				{
					var theArray = new List<sbyte>();

					for (int i = 0; i < element.GetArrayLength(); i++)
					{
						var jsonElement = element.EnumerateArray().ElementAtOrDefault(i);

						if (jsonElement.ValueKind == JsonValueKind.Number)
						{
							if ( jsonElement.TryGetSByte(out sbyte bval))
								theArray.Add(bval);
							else
								throw new InvalidCastException($"Cannot cast value {value} to sbyte array.");
						}
						else
							throw new InvalidCastException($"Cannot cast value {value} to sbyte array.");
					}

					return theArray.ToArray();
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to sbyte array.");
			}
			else
				throw new InvalidCastException($"Cannot cast value {value} to sbyte array.");
        }

		internal object? TranslateByteArray(object value)
        {
            if (value == null)
                return null;

            else if (value.GetType() == typeof(string))
                return Convert.FromBase64String((string)value);

            else if (value.GetType() == typeof(byte[]))
                return (byte[])value;

			else if (value.GetType() == typeof(JsonElement))
					{
						var element = (JsonElement)value;

						if (element.ValueKind == JsonValueKind.String)
						{
                            if (element.TryGetBytesFromBase64(out byte[]? byteArray))
                                return byteArray;
                            else
                                throw new InvalidCastException($"Cannot cast value {value} to byte array.");
                        }
						else if (element.ValueKind == JsonValueKind.Null)
							return null;
						else if (element.ValueKind == JsonValueKind.Array)
						{
							var theArray = new List<byte>();

							for (int i = 0; i < element.GetArrayLength(); i++)
							{
								var jsonElement = element.EnumerateArray().ElementAtOrDefault(i);

								if (jsonElement.ValueKind == JsonValueKind.Number)
									theArray.Add(jsonElement.GetByte());
								else
									throw new InvalidCastException($"Cannot cast value {value} to byte array.");
							}

							return theArray.ToArray();
						}
						else
							throw new InvalidCastException($"Cannot cast value {value} to byte array.");
					}
            else
                throw new InvalidCastException($"Cannot cast value {value} to byte array.");
        }

		internal object? TranslateEnumValue(Type propertyType, object value)
        {
            if (value.GetType() == typeof(string))
            {
				if (Enum.TryParse(propertyType, (string)value, true, out object eVal))
                    return eVal;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to {propertyType}.");
            }
            else if (propertyType == typeof(byte) ||
                      propertyType == typeof(sbyte) ||
                      propertyType == typeof(short) ||
                      propertyType == typeof(ushort) ||
                      propertyType == typeof(int) ||
                      propertyType == typeof(uint) ||
                      propertyType == typeof(long) ||
                      propertyType == typeof(ulong))
            {
                try
                {
                    return Convert.ChangeType(value, propertyType);
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to {propertyType}.");
                }
            }
			else if (value.GetType() == typeof(JsonElement))
				{
					var element = (JsonElement)value;

					if (element.ValueKind == JsonValueKind.Null)
						return null;
					else if (element.ValueKind == JsonValueKind.String)
					{
						var stringValue = element.GetString();

						if (Enum.TryParse(propertyType, stringValue, true, out object eVal))
							return eVal;
						else
							throw new InvalidCastException($"Cannot cast value {value} to {propertyType}.");
					}
					else if (element.ValueKind == JsonValueKind.Number)
					{
						try
						{
							if (element.TryGetByte(out byte bVal))
								return Convert.ChangeType(bVal, propertyType);
							else if (element.TryGetSByte(out sbyte sbVal))
								return Convert.ChangeType(sbVal, propertyType);
							else if (element.TryGetInt16(out short shVal))
								return Convert.ChangeType(shVal, propertyType);
							else if (element.TryGetUInt16(out ushort ushVal))
								return Convert.ChangeType(ushVal, propertyType);
							else if (element.TryGetInt32(out int iVal))
								return Convert.ChangeType(iVal, propertyType);
							else if (element.TryGetUInt32(out uint uiVal))
								return Convert.ChangeType(uiVal, propertyType);
							else if (element.TryGetInt64(out long lVal))
								return Convert.ChangeType(lVal, propertyType);
							else if (element.TryGetUInt64(out ulong ulVal))
								return Convert.ChangeType(ulVal, propertyType);
							else
								throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
						}
						catch (Exception)
						{
							throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
						}
					}
					else
						throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
				}
            else
                throw new InvalidCastException($"Cannot cast value {value} to {propertyType}.");
        }

		internal object? TranslateUriValue(object value)
		{
			if (value.GetType() == typeof(string))
			{
				try
                {
					var url = value.ToString();

					if (url.StartsWith("/"))
					{
						return new Uri(new Uri("http://localhost"), url);
					}
					else
                    {
						return new Uri(url);
                    }
                }
				catch ( Exception )
                {
					throw new InvalidCastException($"Cannot cast value {value} to Uri.");
				}
			}
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
				{
					return null;
				}
				else if (element.ValueKind == JsonValueKind.String)
				{
					try
					{
						if (element.GetString() is string url)
						{
							if (url.StartsWith("/"))
							{
								return new Uri(new Uri("http://localhost"), url);
							}
							else
							{
								return new Uri(url);
							}
						}
						else
							return null;
					}
					catch (Exception)
					{
						throw new InvalidCastException($"Cannot cast value {value} to Uri.");
					}
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
			}
			else
			{
				try
				{
					var uri = (Uri) Convert.ChangeType(value, typeof(Uri));

					if (uri.IsAbsoluteUri)
					{
						return uri;
					}
					else
					{
						return new Uri(new Uri("http://localhost"), uri.ToString());
					}
				}
				catch (Exception)
				{
					throw new InvalidCastException($"Cannot cast value {value} to Uri.");
				}
			}
		}

		internal object? TranslateSByteValue(object value)
		{
			if (value.GetType() == typeof(string))
			{
				if (sbyte.TryParse(value.ToString(), out sbyte theValue))
					return theValue;
				else
					throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
			}
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if ( element.ValueKind == JsonValueKind.String)
                {
					var stringValue = element.GetString();

					if (sbyte.TryParse(stringValue, out sbyte theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetSByte(out sbyte bval))
						return bval;
					else
						throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
			}

			else
			{
				try
				{
					return Convert.ChangeType(value, typeof(sbyte));
				}
				catch (Exception)
				{
					throw new InvalidCastException($"Cannot cast value {value} to sbyte.");
				}
			}
		}

		internal object? TranslateShortValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (short.TryParse(value.ToString(), out short theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to short.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				if (element.ValueKind == JsonValueKind.String)
                {
					var strValue = element.GetString();

					if (short.TryParse(strValue, out short theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to short.");

				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetInt16(out short val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to short.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to short.");
			}

			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(short));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to short.");
                }
            }
        }

		internal object? TranslateUShortValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (ushort.TryParse(value.ToString(), out ushort theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to ushort.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
                {
					var strValue = element.GetString();
					if (ushort.TryParse(strValue, out ushort theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to ushort.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetUInt16(out ushort val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to ushort.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to ushort.");
			}

			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(ushort));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to ushort.");
                }
            }
        }

		internal object? TranslateIntValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (int.TryParse(value.ToString(), out int theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to int.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (int.TryParse(strValue, out int theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to int.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetInt32(out int val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to int.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to int.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(int));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to int.");
                }
            }
        }

		internal object? TranslateUintValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (uint.TryParse(value.ToString(), out uint theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to uint.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (uint.TryParse(strValue, out uint theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to uint.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetUInt32(out uint val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to uint.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to uint.");
			}

			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(uint));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to uint.");
                }
            }
        }

		internal object? TranslateLongValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (long.TryParse(value.ToString(), out long theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to long.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (long.TryParse(strValue, out long theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to long.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetInt64(out long val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to long.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to long.");
			}

			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(long));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to long.");
                }
            }
        }

		internal object? TranslateULongValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (ulong.TryParse(value.ToString(), out ulong theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to ulong.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (ulong.TryParse(strValue, out ulong theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to ulong.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetUInt64(out ulong val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to ulong.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to ulong.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(ulong));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to ulong.");
                }
            }
        }

		internal object? TranslateFloatValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (float.TryParse(value.ToString(), out float theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to float.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (float.TryParse(strValue, out float theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to float.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetSingle(out float val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to float.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to float.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(float));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to float.");
                }
            }
        }

		internal object? TranslateDoubleValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (double.TryParse(value.ToString(), out double theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to double.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (double.TryParse(strValue, out double theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to double.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetDouble(out double val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to double.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to double.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(double));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to double.");
                }
            }
        }

		internal object? TranslateDecimalValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (decimal.TryParse(value.ToString(), out decimal theValue))
                    return theValue;
				else
					throw new InvalidCastException($"Cannot cast value {value} to decimal.");
			}
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (decimal.TryParse(strValue, out decimal theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to decimal.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if ( element.TryGetDecimal(out decimal val))
						return val;
					else
						throw new InvalidCastException($"Cannot cast value {value} to decimal.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to decimal.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(decimal));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to decimal.");
                }
            }
        }

		internal object? TranslateCharValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (char.TryParse(value.ToString(), out char theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to char.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var strValue = element.GetString();
					if (char.TryParse(strValue, out char theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot cast value {value} to char.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to char.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(char));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to char.");
                }
            }
        }

		internal object? TranslateGuidValue(object value)
        {
            if (value == null)
                return null;

            else if (value.GetType() == typeof(string))
            {
                if (Guid.TryParse(value.ToString(), out Guid theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to Guid.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					if (element.TryGetGuid(out Guid guid))
						return guid;
					else
						throw new InvalidCastException($"Cannot cast value {value} to Guid.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to Guid.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(Guid));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to Guid.");
                }
            }
        }

		internal object? TranslateDateTimeValue(object value)
        {
            if (value == null)
                return null;

            else if (value.GetType() == typeof(string))
            {
                if (DateTime.TryParse(value.ToString(), out DateTime theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to DateTime.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					if (element.TryGetDateTime(out DateTime dval))
						return dval;
					else
						throw new InvalidCastException($"Cannot cast value {value} to DateTime.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to DateTime.");
			}
			else if ( value.GetType() == typeof(DateTimeOffset))
            {
				var dateValue = (DateTimeOffset)value;
				return dateValue.DateTime;
            }
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(DateTime));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to DateTime.");
                }
            }
        }

		internal object? TranslateTimeSpanValue(object value)
        {
            if (value == null)
                return null;

            else if (value.GetType() == typeof(string))
            {
                if (TimeSpan.TryParse(value.ToString(), out TimeSpan theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to TimeSpan.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var str = element.GetString();

					if (TimeSpan.TryParse(str, out TimeSpan tval))
						return tval;

					else
						throw new InvalidCastException($"Cannot cast value {value} to TimeSpan.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to TimeSpan.");
			}
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(TimeSpan));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to TimeSpan.");
                }
            }
        }

		internal object? TranslateDateTimeOffsetValue(object value)
        {
            if (value == null)
                return null;

            else if (value.GetType() == typeof(string))
            {
                if (DateTimeOffset.TryParse(value.ToString(), out DateTimeOffset theValue))
                    return theValue;
                else
                    throw new InvalidCastException($"Cannot cast value {value} to DateTimeOffset.");
            }
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					if (element.TryGetDateTimeOffset(out DateTimeOffset dval))
						return dval;
					else
						throw new InvalidCastException($"Cannot cast value {value} to DateTimeOffset.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to DateTimeOffset.");
			}
			else if ( value.GetType() == typeof(DateTime))
            {
				var dateValue = (DateTime)value;
				return new DateTimeOffset(dateValue);
            }
			else
			{
                try
                {
                    return Convert.ChangeType(value, typeof(DateTimeOffset));
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Cannot cast value {value} to DateTimeOffset.");
                }
            }
        }

		internal object? TranslateStringValue(object value)
        {
            if (value.GetType() == typeof(string))
                return value.ToString();
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.String)
					return element.GetString();
				else if (element.ValueKind == JsonValueKind.Null)
					return default;
				else
					throw new InvalidCastException($"Cannot cast value {value} to string.");
			}
			else
				throw new InvalidCastException($"Cannot cast value {value} to string.");
        }

		internal object? TranslateByteValue(object value)
		{
			if (value.GetType() == typeof(string))
			{
				if (byte.TryParse(value.ToString(), out byte theValue))
					return theValue;
				else
					throw new InvalidCastException($"Cannot case value {value} to byte.");
			}
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					var stringValue = element.GetString();

					if (byte.TryParse(stringValue, out byte theValue))
						return theValue;
					else
						throw new InvalidCastException($"Cannot case value {value} to byte.");
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					if (element.TryGetByte(out byte bval))
						return bval;
					else
						throw new InvalidCastException($"Cannot cast value {value} to byte.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to byte.");
			}
			else
			{
				try
				{
					return Convert.ChangeType(value, typeof(byte));
				}
				catch (Exception)
				{
					throw new InvalidCastException($"Cannot cast value {value} to byte.");
				}
			}
		}
		internal object? TranslateImageValue(object value)
		{
			if (value.GetType() == typeof(string))
			{
				return Convert.FromBase64String(value.ToString());
			}
			else if (value.GetType() == typeof(byte[]))
			{
				return value;
			}
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.String)
				{
					return Convert.FromBase64String(element.GetString());
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to byte.");
			}
			else
			{
				try
				{
					return Convert.ChangeType(value, typeof(byte[]));
				}
				catch (Exception)
				{
					throw new InvalidCastException($"Cannot cast value {value} to byte.");
				}
			}
		}

		internal object? TranslateBooleanValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (string.Equals((string)value, "true", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals((string)value, "false", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals((string)value, "yes", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals((string)value, "no", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals((string)value, "t", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals((string)value, "f", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals((string)value, "1", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals((string)value, "0", StringComparison.OrdinalIgnoreCase))
                {
                    if (value.ToString()[..1].ToLower() == "t" || value.ToString()[..1].ToLower() == "y" || value.ToString()[..1].ToLower() == "1")
                        return true;
                    else
                        return false;
                }
                else
                    throw new InvalidCastException($"Cannot cast value {value} to boolean.");
            }

            else if (value.GetType() == typeof(bool))
                return (bool)value;
			else if (value.GetType() == typeof(JsonElement))
			{
				var element = (JsonElement)value;

				if (element.ValueKind == JsonValueKind.Null)
					return null;
				else if (element.ValueKind == JsonValueKind.False)
				{
					return false;
				}
				else if (element.ValueKind == JsonValueKind.True)
				{
					return true;
				}
				else if (element.ValueKind == JsonValueKind.Number)
				{
					var jsonValue = element.GetDecimal();
					return jsonValue != 0;
				}
				else if (element.ValueKind == JsonValueKind.String)
                {
					var stringValue = element.GetString();

					if (string.Equals(stringValue, "true", StringComparison.OrdinalIgnoreCase) ||
						string.Equals(stringValue, "false", StringComparison.OrdinalIgnoreCase) ||
						string.Equals(stringValue, "yes", StringComparison.OrdinalIgnoreCase) ||
						string.Equals(stringValue, "no", StringComparison.OrdinalIgnoreCase) ||
						string.Equals(stringValue, "t", StringComparison.OrdinalIgnoreCase) ||
						string.Equals(stringValue, "f", StringComparison.OrdinalIgnoreCase) ||
						string.Equals(stringValue, "1", StringComparison.OrdinalIgnoreCase) ||
						string.Equals(stringValue, "0", StringComparison.OrdinalIgnoreCase))
					{
						if (value.ToString()[..1].ToLower() == "t" || value.ToString()[..1].ToLower() == "y" || value.ToString()[..1].ToLower() == "1")
							return true;
						else
							return false;
					}
					else
						throw new InvalidCastException($"Cannot cast value {value} to boolean.");
				}
				else
					throw new InvalidCastException($"Cannot cast value {value} to boolean.");
			}
			else
				throw new InvalidCastException($"Cannot case value {value} to boolean.");
        }
		#endregion

		#region Helper Functions
		internal List<string> ExtractMembersFromExpression(RqlNode property, int level, Expression expression)
		{
			var results = new List<string>();

			if (expression is ConditionalExpression conditionalExpression)
			{
				if (conditionalExpression.IfTrue != null)
				{
					var expressionCollection = ExtractMembersFromExpression(property, level + 1, conditionalExpression.IfTrue);

					foreach (var memberName in expressionCollection)
						if (!results.Contains(memberName))
							results.Add(memberName);
				}

				if (conditionalExpression.IfFalse != null)
				{
					var expressionCollection = ExtractMembersFromExpression(property, level + 1, conditionalExpression.IfFalse);

					foreach (var memberName in expressionCollection)
						if (!results.Contains(memberName))
							results.Add(memberName);
				}

				if (conditionalExpression.Test != null)
				{
					var expressionCollection = ExtractMembersFromExpression(property, level + 1, conditionalExpression.Test);

					foreach (var memberName in expressionCollection)
						if (!results.Contains(memberName))
							results.Add(memberName);
				}
			}
			else if (expression is MemberInitExpression memberInitExpression)
			{
				foreach (var binding in memberInitExpression.Bindings)
				{
					if (binding is MemberAssignment memberAssignment)
					{
						if (level < property.Count)
						{
							if (string.Equals(property.Value<string>(level + 1), binding.Member.Name, StringComparison.OrdinalIgnoreCase))
							{
								var collection = ExtractMembersFromExpression(property, level + 1, memberAssignment.Expression);

								foreach (var memberName in collection)
									if (!results.Contains(memberName))
										results.Add(memberName);
							}
						}
						else
						{
							var collection = ExtractMembersFromExpression(property, level, memberAssignment.Expression);

							foreach (var memberName in collection)
								if (!results.Contains(memberName))
									results.Add(memberName);
						}
					}
				}
			}
			else if (expression is MemberExpression memberExpression)
			{
				var collection = ExtractMembersFromExpression(property, level, memberExpression.Expression);

				foreach (var memberName in collection)
					if (!results.Contains(memberName))
						results.Add(memberName);

				if (!string.Equals(memberExpression.Member.Name, "value", StringComparison.OrdinalIgnoreCase) &&
					!string.Equals(memberExpression.Member.Name, "hasvalue", StringComparison.OrdinalIgnoreCase))
					if (!results.Contains(memberExpression.Member.Name))
						results.Add(memberExpression.Member.Name);
			}
			else if (expression is NewExpression newExpression)
			{
				results.Add(newExpression.Type.Name);

				foreach (var exp in newExpression.Arguments)
				{
					var expressionCollection = ExtractMembersFromExpression(property, level, exp);

					foreach (var memberName in expressionCollection)
						if (!results.Contains(memberName))
							results.Add(memberName);
				}
			}
			else if (expression is UnaryExpression unaryExpression)
			{
				var expressionCollection = ExtractMembersFromExpression(property, level, unaryExpression.Operand);

				foreach (var memberName in expressionCollection)
					if (!results.Contains(memberName))
						results.Add(memberName);
			}

			else if (expression is MethodCallExpression methodCallExpression)
			{
				try
				{
					foreach (var exp in methodCallExpression.Arguments)
					{
						var expressionCollection = ExtractMembersFromExpression(property, level, exp);

						foreach (var memberName in expressionCollection)
							if (!results.Contains(memberName))
								results.Add(memberName);
					}
				}
				catch (Exception)
				{
				}
			}

			return results;
		}

		/// <summary>
		/// Instantiates an instance of an object of type T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetInstance<T>()
		{
			return (T)Activator.CreateInstance(typeof(T));
		}

		private PropertyInfo? GetResourceProperty<TResource>(RqlNode resourceMember)
		{
			PropertyInfo? resourceProperty = null;
			var resourceType = typeof(TResource);

			foreach (string memberName in resourceMember)
			{
				resourceProperty = FindProperty(resourceType, memberName); ;
				
				if ( resourceProperty != null )
                {
					resourceType = resourceProperty.PropertyType;

					if (resourceType.IsGenericType && resourceType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						resourceType = Nullable.GetUnderlyingType(resourceType);
					}
				}
			}

			return resourceProperty;
		}

		/// <summary>
		/// Given a resource model, returns the associated entity model type
		/// </summary>
		/// <typeparam name="TResource">Type of the resource model</typeparam>
		/// <returns></returns>
		public Type GetEntityType<TResource>()
		{
			var entityAttribute = typeof(TResource).GetCustomAttribute<EntityAttribute>();

			if (entityAttribute == null)
				throw new InvalidCastException($"{typeof(TResource).Name} is not a resource model.");

			return entityAttribute.EntityType;
		}

		/// <summary>
		/// Given a domain model, returns the associated entity model type
		/// </summary>
		/// <param name="resourceType">Type of the domain model</param>
		/// <returns></returns>
		public Type GetEntityType(Type resourceType)
		{
			var entityAttribute = resourceType.GetCustomAttribute<EntityAttribute>();

			if (entityAttribute == null)
				throw new InvalidCastException($"{resourceType.Name} is not a resource model.");

			return entityAttribute.EntityType;
		}

		/// <summary>
		/// Returns the property for the name
		/// </summary>
		/// <param name="propertyType"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public PropertyInfo? FindProperty(Type propertyType, string propertyName)
        {
			var jsonSettings = (JsonSerializerOptions?) _serviceProvider.GetService(typeof(JsonSerializerOptions));
			var caseInSensitiveSearch = (jsonSettings == null) || jsonSettings.PropertyNameCaseInsensitive;
			var properties = propertyType.GetProperties();

			foreach (var property in properties)
			{
				var candidateName = property.Name;

				var jsonName = property.GetCustomAttribute<JsonPropertyNameAttribute>();

				if (jsonName != null)
					candidateName = jsonName.Name;

				if ( caseInSensitiveSearch)
                {
					if (string.Equals(candidateName, propertyName, StringComparison.OrdinalIgnoreCase))
						return property;
                }
				else
                {
					if (string.Equals(candidateName, propertyName, StringComparison.Ordinal))
						return property;
				}
			}

			return null;
        }

        internal string[] GetEntitySubNames(Type resourceType, Type entityType, RqlNode resourceMember)
		{
			var results = new List<string>();

			var resourceMemberName = resourceMember.NonNullValue<string>(0);
			var resourceProperty = FindProperty(resourceType, resourceMemberName);

			if (resourceProperty != null)
			{
				var entityProperty = entityType.GetProperties().FirstOrDefault(e => string.Equals(e.Name, resourceProperty.Name, StringComparison.OrdinalIgnoreCase));

				if (entityProperty != null)
				{
					var resourceSubType = resourceProperty.PropertyType;
					var entitySubType = entityProperty.PropertyType;
				}
			}

			return results.ToArray();
		}

		internal string FindPropertyName(Type typeToSearch, string name)
		{
			var jsonOptions = (JsonSerializerOptions) _serviceProvider.GetService(typeof(JsonSerializerOptions));
			var caseInsensitive = (jsonOptions == null) || jsonOptions.PropertyNameCaseInsensitive;
			var properties = typeToSearch.GetProperties();

			foreach (var property in properties)
			{
				var propertyName = property.Name;
				var jsonName = property.GetCustomAttribute<JsonPropertyNameAttribute>();

				if (jsonName != null)
					propertyName = jsonName.Name;

				if (caseInsensitive)
				{
					if (string.Equals(name, propertyName, StringComparison.OrdinalIgnoreCase))
					{
						return property.Name;
					}
				}
				else
				{
					if (string.Equals(name, propertyName, StringComparison.Ordinal))
					{
						return property.Name;
					}
				}
			}

			return name;
		}
		#endregion
	}
}
