using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tense.Rql
{
	/// <summary>
	/// Represents a hierarchial set of RQL operations and their parameters
	/// </summary>
	public class RqlNode 
	{
		/// <summary>
		/// Gets or sets the RQL operation for this node
		/// </summary>
		public RqlOperation Operation { get; set; }
		private readonly List<object> _list = new();

		/// <summary>
		/// Instantiates an RQL node with a specific operation
		/// </summary>
		/// <param name="op">The <see cref="RqlOperation"/> for this node</param>
		public RqlNode(RqlOperation op)
		{
			Operation = op;
		}

		/// <summary>
		/// Converts the string representation of an RQL Query to its <see cref="RqlNode"/>
		/// equivalent by using culture-specific format information
		/// </summary>
		/// <param name="inQueryString">A string that contains an RQL Query to convert.</param>
		/// <returns>An <see cref="RqlNode"/> object that is equivalent to the RQL Query contained in <paramref name="inQueryString"/>, or <see langword="null"/> if <paramref name="inQueryString"/> is an empty string.</returns>
		/// <exception cref="RqlFormatException">Thrown when s does not contain a valid string representation of a valid RQL query.</exception>
		public static RqlNode Parse(string? inQueryString)
		{
			if (string.IsNullOrWhiteSpace(inQueryString))
				return new RqlNode(RqlOperation.NOOP);

			return RqlParser.Parse(inQueryString);
		}
		
		/// <summary>
		/// Validate the members in the RQL Statement
		/// </summary>
		/// <typeparam name="T">The type of member to check against.</typeparam>
		/// <param name="serviceProvider">The <see cref="IServiceProvider"/> interface</param>
		/// <param name="mapper">The <see cref="IMapper"/> interface.</param>
		/// <param name="errors">The returned errors, if any</param>
		/// <returns></returns>
		public bool ValidateMembers<T>(IServiceProvider serviceProvider, IMapper mapper, ModelStateDictionary errors)
		{
			var translator = new Translator(serviceProvider, mapper);

			if (!translator.CheckMembers<T>(this, out List<string> badMembers))
			{
				foreach (var badMember in badMembers)
				{
					errors.AddModelError(badMember, $"Not a member of {typeof(T).Name}");
				}

				return false;
			}

			return true;
		}


		/// <summary>
		/// Gets or sets the element at the specified index. The child element will either be an <see cref="RqlNode"/> or a value,
		/// depending upon the <see cref="RqlNode"/> being queried.
		/// </summary>
		/// <param name="index">The index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">when index is out of range.</exception>
		public object this[int index]
		{
			get 
			{
				if (index >= 0 && index < _list.Count)
					return _list[index];

				throw new ArgumentOutOfRangeException(nameof(index));
			}
			set 
			{
				if (index >= 0 && index < _list.Count)
					_list[index] = value;

				throw new ArgumentOutOfRangeException(nameof(index));
			}
		}

		/// <summary>
		/// Extracts the value of a node parameter at a given <paramref name="index"/>. The child element will either be an <see cref="RqlNode"/> or a value,
		/// depending upon the <see cref="RqlNode"/> being queried.
		/// </summary>
		/// <typeparam name="T">The value will be cast to this type upon extraction.</typeparam>
		/// <param name="index">The index of the paramter.</param>
		/// <returns>The parameter value of the <see cref="RqlNode"/> at the given <paramref name="index"/>, cast to type <typeparamref name="T"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public T? Value<T>(int index) where T : class
		{
			if (index >= 0 && index < _list.Count)
			{
				if (_list[index] == null)
					return default;

				if (_list[index].GetType() == typeof(T))
					return (T)_list[index];

				if (typeof(T) == typeof(object))
					return (T)_list[index];

				return (T)Convert.ChangeType(_list[index], typeof(T));
			}

			throw new ArgumentOutOfRangeException(nameof(index));
		}

		/// <summary>
		/// Extracts the non-null value of a node parameter at a given <paramref name="index"/>. The child element will either be an <see cref="RqlNode"/> or a value,
		/// depending upon the <see cref="RqlNode"/> being queried.
		/// </summary>
		/// <typeparam name="T">The value will be cast to this type upon extraction.</typeparam>
		/// <param name="index">The index of the paramter.</param>
		/// <returns>The parameter value of the <see cref="RqlNode"/> at the given <paramref name="index"/>, cast to type <typeparamref name="T"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when you ask for an index that does not exist</exception>
		/// <exception cref="NullReferenceException">Thrown when the value is null</exception>
		public T NonNullValue<T>(int index)
		{
			if (index >= 0 && index < _list.Count)
			{
				if (_list[index] == null)
					throw new NullReferenceException("RqlNode value is null.");

				if (_list[index].GetType() == typeof(T))
					return (T)_list[index];

				if (typeof(T) == typeof(object))
					return (T)_list[index];

				return (T)Convert.ChangeType(_list[index], typeof(T));
			}

			throw new ArgumentOutOfRangeException(nameof(index));
		}


		/// <summary>
		/// Adds a parameter to the list of parameters in an <see cref="RqlNode"/>.
		/// </summary>
		/// <param name="item">The parameter item to add.</param>
		public void Add(object item)
		{
			_list.Add(item);
		}

		/// <summary>
		/// Adds an <see cref="RqlNode"/> parameter to the list of parameters in an <see cref="RqlNode"/>. This function will
		/// throw an RqlFormatException if adding a node at this point would violate RQL syntax.
		/// </summary>
		/// <param name="node">The <see cref="RqlNode"/> parameter item to add.</param>
		/// <exception cref="RqlFormatException"></exception>
		public void Add(RqlNode node)
        {
			if (Operation == RqlOperation.SELECT)
			{
				if (node.Operation == RqlOperation.PROPERTY)
				{
					_list.Add(node);
				}
				else
					throw new RqlFormatException("A SELECT node contains only PROPERTY node children. Adding this node would create a syntax violation.");
			}
			else if (Operation == RqlOperation.SORT)
			{
				if (node.Operation == RqlOperation.SORTPROPERTY)
				{
					_list.Add(node);
				}
				else
					throw new RqlFormatException("A SORT node contains only SORTPROPERTY node children. Adding this node would create a syntax violation.");
			}
			else if (Operation == RqlOperation.SORTPROPERTY)
			{
				if (node.Operation == RqlOperation.PROPERTY && _list.Count == 1)
					_list.Add(node);
				else
					throw new RqlFormatException("Adding a child node to this parent node at this index would create a syntax violation.");
			}
			else if (Operation == RqlOperation.AND ||
					 Operation == RqlOperation.OR)
			{
				_list.Add(node);
			}
			else if (Operation == RqlOperation.EQ ||
					  Operation == RqlOperation.NE ||
					  Operation == RqlOperation.GT ||
					  Operation == RqlOperation.GE ||
					  Operation == RqlOperation.LT ||
					  Operation == RqlOperation.LE ||
					  Operation == RqlOperation.IN ||
					  Operation == RqlOperation.OUT ||
					  Operation == RqlOperation.CONTAINS ||
					  Operation == RqlOperation.EXCLUDES ||
					  Operation == RqlOperation.LIKE ||
					  Operation == RqlOperation.VALUES ||
					  Operation == RqlOperation.MIN ||
					  Operation == RqlOperation.MAX ||
					  Operation == RqlOperation.MEAN ||
					  Operation == RqlOperation.SUM)
			{
				if (_list.Count == 0)
				{
					if (node.Operation == RqlOperation.PROPERTY)
					{
						_list.Add(node);
					}
					else
						throw new RqlFormatException("This node requires a PROPERTY node at the 0th location. Adding this node would create a syntax violation.");
				}
				else
					throw new RqlFormatException("Adding a child node to this parent node at this index would create a syntax violation.");
			}
			else if (Operation == RqlOperation.AGGREGATE)
			{
				if (_list.Count == 0)
				{
					if (node.Operation == RqlOperation.PROPERTY)
					{
						_list.Add(node);
					}
					else if (node.Operation == RqlOperation.SUM ||
							  node.Operation == RqlOperation.MAX ||
							  node.Operation == RqlOperation.MIN ||
							  node.Operation == RqlOperation.MEAN ||
							  node.Operation == RqlOperation.COUNT)
					{
						throw new RqlFormatException("An AGGREGATE operation must contain at least one property, and all properties must preceed any SUM, MAX, MIN, MEAN and/or COUNT operations.");
					}
					else
						throw new RqlFormatException("An AGGREGATE operation can contain only properties and SUM, MAX, MIN, MEAN and/or COUNT operations.");
				}
				else if (((RqlNode)_list[^1]).Operation == RqlOperation.PROPERTY)
				{
					if (node.Operation == RqlOperation.PROPERTY)
					{
						_list.Add(node);
					}
					else if (node.Operation == RqlOperation.SUM ||
							  node.Operation == RqlOperation.MAX ||
							  node.Operation == RqlOperation.MIN ||
							  node.Operation == RqlOperation.MEAN ||
							  node.Operation == RqlOperation.COUNT)
					{
						_list.Add(node);
					}
					else
						throw new RqlFormatException("An AGGREGATE operation can contain only properties and SUM, MAX, MIN, MEAN and/or COUNT operations.");
				}
				else
				{
					if (node.Operation == RqlOperation.PROPERTY)
					{
						throw new RqlFormatException("In an AGGREGATE operation, all properties must preceed any SUM, MAX, MIN, MEAN and/or COUNT operations.");
					}
					else if (node.Operation == RqlOperation.SUM ||
							  node.Operation == RqlOperation.MAX ||
							  node.Operation == RqlOperation.MIN ||
							  node.Operation == RqlOperation.MEAN ||
							  node.Operation == RqlOperation.COUNT)
					{
						_list.Add(node);
					}
					else
						throw new RqlFormatException("An AGGREGATE operation can contain only properties and SUM, MAX, MIN, MEAN and/or COUNT operations.");
				}
			}
			else if (Operation == RqlOperation.COUNT)
			{
				if (_list.Count == 0)
				{
					if (node.Operation == RqlOperation.PROPERTY)
					{
						_list.Add(node);
					}
					else
						throw new RqlFormatException("A COUNT operation can only reference a property.");
				}
				else
					throw new RqlFormatException("A COUNT operation cannot reference more than one property.");
			}
			else if (Operation == RqlOperation.NOOP)
			{
				_list.Add(node);
			}
			else
				throw new RqlFormatException("Adding a child node to this parent node at this index would create a syntax violation.");
        }

		/// <summary>
		/// Returns the number of parameters contained within the <see cref="RqlNode"/>.
		/// </summary>
		/// <returns>The number of elements in the <see cref="RqlNode"/>.</returns>
		public int Count
		{
			get
			{
				return _list.Count;
			}
		}

		/// <summary>
		/// Returns the number of <see cref="RqlOperation"/> <see cref="RqlNode"/>s contained within the current <see cref="RqlNode"/>.
		/// </summary>
		///<param name="operation">The <see cref="RqlOperation"/> <see cref="RqlNode"/>s to count.</param> 
		///<returns>The number of <see cref="RqlOperation"/> <see cref="RqlNode"/>s in the current <see cref="RqlNode"/>.</returns>
		public int CountOf(RqlOperation operation)
		{
			int answer = 0;

			if (Operation == operation)
			{
				answer++;
			}

			foreach (var child in _list)
			{
				if (child != null && child.GetType() == typeof(RqlNode))
				{
                    if (child is RqlNode childnode)
                        answer += childnode.CountOf(operation);
                }
			}

			return answer;
		}

		/// <summary>
		/// Returns a string that represents the <see cref="RqlNode"/> in specified format
		/// </summary>
		/// <param name="format">The <see cref="RqlFormat"/> that specifies the format of the string to return.</param>
		/// <returns>A string that represents the current object.</returns>
		/// <exception cref="FormatException">when format is an unrecognized format.</exception>
		public string ToString(RqlFormat format)
		{
			switch (format)
			{
				case RqlFormat.Standard:
					return ToString();

				case RqlFormat.Normalized:
					{
						StringBuilder theString = new();

						switch (Operation)
						{
							case RqlOperation.SORTPROPERTY:
								var property = NonNullValue<RqlNode>(1);

								if (NonNullValue<RqlSortOrder>(0) == RqlSortOrder.Ascending)
								{
									theString.Append($"+{ConvertValueToString(property)}");
								}
								else
								{
									theString.Append($"-{ConvertValueToString(property)}");
								}
								break;

							case RqlOperation.PROPERTY:
								{
									if (_list.Count > 1)
										theString.Append('(');
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										theString.Append(item);
									}
									if (_list.Count > 1)
										theString.Append(')');
								}
								break;

							case RqlOperation.LIMIT:
								{
									theString.Append(Operation.ToString().ToLower());
									theString.Append('(');
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										if ((ulong)item <= int.MaxValue)
											theString.Append($"{item}");
										else if ((ulong)item <= long.MaxValue)
											theString.Append($"{item}L");
										else
											theString.Append($"{item}UL");
									}
									theString.Append(')');
								}
								break;

							case RqlOperation.SORT:
								{
									theString.Append(Operation.ToString().ToLower());
									theString.Append('(');
									bool First = true;

									foreach (RqlNode sortProperty in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										var prop = sortProperty.NonNullValue<RqlNode>(1);
										if (sortProperty.NonNullValue<RqlSortOrder>(0) == RqlSortOrder.Ascending)
										{
											theString.Append(ConvertValueToString(prop));
										}
										else
										{
											theString.Append($"-{ConvertValueToString(prop)}");
										}
									}
									theString.Append(')');
								}
								break;

							case RqlOperation.SELECT:
							case RqlOperation.CONTAINS:
							case RqlOperation.LIKE:
							case RqlOperation.EXCLUDES:
								{
									theString.Append(Operation.ToString().ToLower());
									theString.Append('(');
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										theString.Append(ConvertValueToString(item));
									}
									theString.Append(')');
								}
								break;

							case RqlOperation.IN:
							case RqlOperation.OUT:
								{
									theString.Append(Operation.ToString().ToLower());
									theString.Append('(');
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										if (item.GetType() == typeof(RqlNode))
											theString.Append(((RqlNode)item).ToString(RqlFormat.Normalized));
										else
											theString.Append(ConvertValueToString(item));
									}
									theString.Append(')');
								}
								break;

							case RqlOperation.DISTINCT:
								theString.Append("distinct()");
								break;

							case RqlOperation.EQ:
								theString.Append("eq(");
								theString.Append(_list[0].ToString());
								theString.Append(',');
								theString.Append(ConvertValueToString(_list[1]));
								theString.Append(')');
								break;

							case RqlOperation.NE:
								theString.Append("ne(");
								theString.Append(_list[0].ToString());
								theString.Append(',');
								theString.Append(ConvertValueToString(_list[1]));
								theString.Append(')');
								break;

							case RqlOperation.LE:
								theString.Append("le(");
								theString.Append(_list[0].ToString());
								theString.Append(',');
								theString.Append(ConvertValueToString(_list[1]));
								theString.Append(')');
								break;

							case RqlOperation.LT:
								theString.Append("lt(");
								theString.Append(_list[0].ToString());
								theString.Append(',');
								theString.Append(ConvertValueToString(_list[1]));
								theString.Append(')');
								break;

							case RqlOperation.GE:
								theString.Append("ge(");
								theString.Append(_list[0].ToString());
								theString.Append(',');
								theString.Append(ConvertValueToString(_list[1]));
								theString.Append(')');
								break;

							case RqlOperation.GT:
								theString.Append("gt(");
								theString.Append(_list[0].ToString());
								theString.Append(',');
								theString.Append(ConvertValueToString(_list[1]));
								theString.Append(')');
								break;

							case RqlOperation.AND:
								{
									theString.Append("and(");
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										theString.Append(((RqlNode)item).ToString(RqlFormat.Normalized));
									}
									theString.Append(')');
								}
								break;

							case RqlOperation.OR:
								{
									theString.Append("or(");
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										theString.Append(((RqlNode)item).ToString(RqlFormat.Normalized));
									}
									theString.Append(')');
								}
								break;

							case RqlOperation.ONE:
								theString.Append("one()");
								break;

							case RqlOperation.FIRST:
								theString.Append("first()");
								break;

							case RqlOperation.AGGREGATE:
								{
									theString.Append(Operation.ToString().ToLower());
									theString.Append('(');
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										theString.Append(((RqlNode)item).ToString(RqlFormat.Normalized));
									}
									theString.Append(')');
								}
								break;

							case RqlOperation.COUNT:
							case RqlOperation.SUM:
							case RqlOperation.MAX:
							case RqlOperation.MIN:
							case RqlOperation.MEAN:
								{
									theString.Append(Operation.ToString().ToLower());
									theString.Append('(');
									bool First = true;

									foreach (var item in _list)
									{
										if (First)
											First = false;
										else
											theString.Append(',');

										theString.Append(((RqlNode)item).ToString(RqlFormat.Normalized));
									}
									theString.Append(')');
								}
								break;

							default:
								theString.Append("Unknown");
								break;
						}

						return theString.ToString();
					}

				default:
					throw new FormatException("Unknown or invalid format");
			}
		}

		/// <summary>
		/// Returns a string that represents the <see cref="RqlNode"/> in standard format
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			StringBuilder theString = new();

			switch (Operation)
			{
				case RqlOperation.SORTPROPERTY:
					var property = NonNullValue<RqlNode>(1);

					if (NonNullValue<RqlSortOrder>(0) == RqlSortOrder.Ascending)
					{
						theString.Append($"+{ConvertValueToString(property)}");
					}
					else
					{
						theString.Append($"-{ConvertValueToString(property)}");
					}
					break;

				case RqlOperation.PROPERTY:
					{
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append('/');

							theString.Append(item);
						}
					}
					break;

				case RqlOperation.LIMIT:
					{
						theString.Append(Operation.ToString().ToLower());
						theString.Append('(');
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append(',');

							if ((ulong)item <= int.MaxValue)
								theString.Append($"{item}");
							else if ((ulong)item <= long.MaxValue)
								theString.Append($"{item}L");
							else
								theString.Append($"{item}UL");
						}
						theString.Append(')');
					}
					break;

				case RqlOperation.SORT:
					{
						theString.Append(Operation.ToString().ToLower());
						theString.Append('(');
						bool First = true;

						foreach (RqlNode sortProperty in _list)
						{
							if (First)
								First = false;
							else
								theString.Append(',');

							var prop = sortProperty.NonNullValue<RqlNode>(1);

							if (sortProperty.NonNullValue<RqlSortOrder>(0) == RqlSortOrder.Ascending)
							{
								theString.Append(ConvertValueToString(prop));
							}
							else
							{
								theString.Append($"-{ConvertValueToString(prop)}");
							}

						}
						theString.Append(')');
					}
					break;

				case RqlOperation.SELECT:
				case RqlOperation.CONTAINS:
				case RqlOperation.LIKE:
				case RqlOperation.EXCLUDES:
					{
						theString.Append(Operation.ToString().ToLower());
						theString.Append('(');
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append(',');

							theString.Append(ConvertValueToString(item));
						}
						theString.Append(')');
					}
					break;

				case RqlOperation.IN:
				case RqlOperation.OUT:
					{
						theString.Append(Operation.ToString().ToLower());
						theString.Append('(');
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append(',');

							if (item.GetType() == typeof(RqlNode))
								theString.Append(item.ToString());
							else
								theString.Append(ConvertValueToString(item));
						}
						theString.Append(')');
					}
					break;

				case RqlOperation.DISTINCT:
					theString.Append("distinct()");
					break;

				case RqlOperation.ONE:
					theString.Append("one()");
					break;

				case RqlOperation.FIRST:
					theString.Append("first()");
					break;

				case RqlOperation.EQ:
					theString.Append(_list[0].ToString());
					theString.Append('=');
					theString.Append(ConvertValueToString(_list[1]));
					break;

				case RqlOperation.NE:
					theString.Append(_list[0].ToString());
					theString.Append("!=");
					theString.Append(ConvertValueToString(_list[1]));
					break;

				case RqlOperation.LE:
					theString.Append(_list[0].ToString());
					theString.Append("<=");
					theString.Append(ConvertValueToString(_list[1]));
					break;

				case RqlOperation.LT:
					theString.Append(_list[0].ToString());
					theString.Append('<');
					theString.Append(ConvertValueToString(_list[1]));
					break;

				case RqlOperation.GE:
					theString.Append(_list[0].ToString());
					theString.Append(">=");
					theString.Append(ConvertValueToString(_list[1]));
					break;

				case RqlOperation.GT:
					theString.Append(_list[0].ToString());
					theString.Append('>');
					theString.Append(ConvertValueToString(_list[1]));
					break;

				case RqlOperation.NOOP:
					theString.Append("NOOP");
					break;

				case RqlOperation.AND:
					{
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append('&');

							if (item == null )
                            {
								theString.Append("null");
                            }
							else if ( item is RqlNode rqlItem )
							{
								if ( rqlItem.Operation == RqlOperation.AND ||
									 rqlItem.Operation == RqlOperation.OR )
								{
									theString.Append('(');
									theString.Append(rqlItem.ToString());
									theString.Append(')');
								}
								else
									theString.Append(rqlItem.ToString());
							}
							else
								theString.Append(item.ToString());
						}
					}
					break;

				case RqlOperation.OR:
					{
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append('|');

							if (item is RqlNode rqlItem)
							{
								if (rqlItem.Operation == RqlOperation.AND ||
									rqlItem.Operation == RqlOperation.OR)
								{
									theString.Append('(');
									theString.Append(rqlItem.ToString());
									theString.Append(')');
								}
								else
									theString.Append(rqlItem.ToString());
							}
							else
								theString.Append(item.ToString());
						}
					}
					break;

				case RqlOperation.AGGREGATE:
					{
						theString.Append(Operation.ToString().ToLower());
						theString.Append('(');
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append(',');

							theString.Append(item.ToString());
						}
						theString.Append(')');
					}
					break;

				case RqlOperation.SUM:
				case RqlOperation.MAX:
				case RqlOperation.MIN:
				case RqlOperation.MEAN:
				case RqlOperation.COUNT:
					{
						theString.Append(Operation.ToString().ToLower());
						theString.Append('(');
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append(',');

							theString.Append(item.ToString());
						}
						theString.Append(')');
					}
					break;

				case RqlOperation.VALUES:
					{
						theString.Append(Operation.ToString().ToLower());
						theString.Append('(');
						bool First = true;

						foreach (var item in _list)
						{
							if (First)
								First = false;
							else
								theString.Append(',');

							theString.Append(item.ToString());
						}
						theString.Append(')');
					}
					break;

				default:
					theString.Append("Unknown");
					break;
			}

			return theString.ToString();
		}

		/// <summary>
		/// Determines if the current <see cref="RqlNode"/> is of, or if any child <see cref="RqlNode"/>s are of the specified operation.
		/// </summary>
		/// <param name="op">The operation to locate within the current <see cref="RqlNode"/></param>
		/// <returns><see langword="true"/> if the current node, or any of it's children, are of the specified operation; <see langword="false"/> otherwise.</returns>
		public bool Contains(RqlOperation op)
		{
			bool answer = false;

			if (Operation == op)
			{
				answer = true;
			}
			else
			{
				foreach (var child in _list)
				{
					if (child != null && child.GetType() == typeof(RqlNode))
					{
						if ( child is RqlNode childnode)
							answer |= childnode.Contains(op);
					}
				}
			}

			return answer;
		}

		/// <summary>
		/// Determines if the current <see cref="RqlNode"/> references, or contains any child <see cref="RqlNode"/>s that reference the specified member.
		/// </summary>
		/// <param name="memberName">The member to locate</param>
		/// <returns><see langword="true"/> if the current <see cref="RqlNode"/> or any of it's children reference the member; <see langword="false"/> otherwise.</returns>
		public bool Contains(RqlNode memberName)
		{
			bool answer = false;

			if (memberName.Operation != RqlOperation.PROPERTY)
				return false;

			switch (Operation)
			{
				case RqlOperation.SORT:
				case RqlOperation.SELECT:
				case RqlOperation.VALUES:
				case RqlOperation.AND:
				case RqlOperation.OR:
				case RqlOperation.AGGREGATE:
					foreach (RqlNode child in _list)
					{
						answer |= child.Contains(memberName);
					}
					break;

				case RqlOperation.EQ:
				case RqlOperation.LE:
				case RqlOperation.GE:
				case RqlOperation.LT:
				case RqlOperation.GT:
				case RqlOperation.NE:
				case RqlOperation.LIKE:
				case RqlOperation.IN:
				case RqlOperation.OUT:
				case RqlOperation.CONTAINS:
				case RqlOperation.EXCLUDES:
				case RqlOperation.COUNT:
				case RqlOperation.SUM:
				case RqlOperation.MIN:
				case RqlOperation.MAX:
				case RqlOperation.MEAN:
					if (_list.Count > 0)
					{
						if (_list[0] is RqlNode child)
							return child.Equals(memberName);
						else
							throw new RqlFormatException("Invalid RQL Syntax");
					}
					break;

				case RqlOperation.SORTPROPERTY:
					if (_list.Count > 1 )
					{
						if ( _list[1] is RqlNode property)
							return property.Equals(memberName);
						else
							throw new RqlFormatException("Invalid RQL Syntax");
					}
					break;

				case RqlOperation.PROPERTY:
					return Equals(memberName);
			}

			return answer;
		}

		/// <summary>
		/// Determines if the <see cref="RqlNode"/> contains a node with the same operation and member
		/// </summary>
		/// <param name="op">The <see cref="RqlOperation"/> to search for</param>
		/// <param name="member">The member to search for. This <see cref="RqlNode"/> must be a PROPERTY node.</param>
		/// <returns><see langword="true"/> if a matching node is found; otherwise <see langword="false"/></returns>
		public bool Contains(RqlOperation op, RqlNode member)
		{
			bool answer = false;

			if (member.Operation != RqlOperation.PROPERTY)
				throw new InvalidOperationException("The member must be a PROPERTY node");

			if (op == RqlOperation.AND || op == RqlOperation.OR || op == RqlOperation.PROPERTY ||
				 op == RqlOperation.SORTPROPERTY || op == RqlOperation.DISTINCT ||
				 op == RqlOperation.FIRST || op == RqlOperation.LIMIT || op == RqlOperation.NOOP ||
				 op == RqlOperation.ONE || op == RqlOperation.SELECT || op == RqlOperation.SORT)
			{
				throw new InvalidOperationException("The operations searched for is invalid.");
			} 

			switch (Operation)
			{
				case RqlOperation.AND:
				case RqlOperation.OR:
					foreach (RqlNode child in _list)
					{
						try
						{
							answer |= child.Contains(op, member);
						}
						catch (InvalidOperationException)
						{
						}
					}
					break;

				case RqlOperation.AGGREGATE:
					if (op == RqlOperation.AGGREGATE)
					{
						foreach ( RqlNode child in _list )
						{
							if (child.Operation == RqlOperation.PROPERTY)
							{
								answer |= child.Equals(member);
                           }
							else
							{
								var cnode = child.Value<RqlNode>(0);
								if ( cnode != null )
									answer |= cnode.Equals(member);
							}
						}
					}
					else
					{
						foreach (RqlNode child in _list)
						{
							try
							{
								answer |= child.Contains(op, member);
							}
							catch (InvalidOperationException)
							{
							}
						}
					}
					break;

				case RqlOperation.VALUES:
				case RqlOperation.EQ:
				case RqlOperation.LE:
				case RqlOperation.GE:
				case RqlOperation.LT:
				case RqlOperation.GT:
				case RqlOperation.NE:
				case RqlOperation.LIKE:
				case RqlOperation.IN:
				case RqlOperation.OUT:
				case RqlOperation.CONTAINS:
				case RqlOperation.EXCLUDES:
				case RqlOperation.COUNT:
				case RqlOperation.SUM:
				case RqlOperation.MIN:
				case RqlOperation.MAX:
				case RqlOperation.MEAN:
					if (_list.Count > 0)
					{
						if (Operation == op)
						{
                            return _list[0] is RqlNode child && child.Equals(member);
                        }
						else
							return false;
					}
					break;
			}

			return answer;
		}

		/// <summary>
		/// Finds the First <see cref="RqlNode"/> of the specified operation
		/// </summary>
		/// <param name="op">The operation to search for</param>
		/// <returns></returns>
		public RqlNode? Find(RqlOperation op)
		{
			if (Operation == op)
				return this;

			foreach (var child in _list)
			{
				if (child != null && child.GetType() == typeof(RqlNode))
				{
                    if (child is RqlNode childnode)
                    {
                        if (childnode.Operation == op)
                            return child as RqlNode;

                        var result = childnode.Find(op);

                        if (result != null)
                            return result;
                    }
                }
			}

			return null;
		}

		/// <summary>
		/// Finds the first <see cref="RqlNode"/> property node
		/// </summary>
		/// <param name="member">The property node to find</param>
		/// <returns>The property node if found; otherwise, <see langword="null"/></returns>
		public RqlNode? Find(RqlNode member)
        {
			if (IsEqualProperty(member))
				return this;

            foreach (var child in _list)
            {
                if (child != null && child.GetType() == typeof(RqlNode))
                {
					if (child is RqlNode childnode)
					{
						var result = childnode.Find(member);

						if (result != null)
							return result;
					}
                }
            }

            return null;
        }

		/// <summary>
		/// Finds the first <see cref="RqlNode"/> of a certain operation that references a certian member
		/// </summary>
		/// <param name="op">The operation to search for</param>
		/// <param name="member">The member to be referenced</param>
		/// <returns>The property node if found; otherwise, <see langword="null"/></returns>
		public RqlNode? Find(RqlOperation op, RqlNode member)
        {
			var firstNode = Value<RqlNode>(0);

			if (firstNode != null)
			{
				if (this.Operation == op && firstNode.IsEqualProperty(member))
				{
					return this;
				}
			}

			foreach (var child in _list)
			{
				if (child is RqlNode childnode)
				{
					var result = childnode.Find(op, member);

					if (result != null)
						return result;
				}
			}

			return null;
		}

		/// <summary>
		/// Returns a collection of RQL Nodes of a specified type
		/// </summary>
		/// <param name="op">The <see cref="RqlOperation"/> of the node list</param>
		/// <returns></returns>
		public List<RqlNode> FindAll(RqlOperation op)
		{
			var theList = new List<RqlNode>();

			if (Operation == op)
			{
				theList.Add(this);
				return theList;
			}

			foreach (var child in _list)
			{
				if ( child is RqlNode childnode)
				{ 
					theList.AddRange(childnode.FindAll(op));
				}
			}

			return theList;
		}

		/// <summary>
		/// Removes all nodes of a specified type from the <see cref="RqlNode"/>.
		/// </summary>
		/// <param name="op">The <see cref="RqlOperation"/> of the nodes to be removed</param>
		/// <returns></returns>
		public void RemoveAll(RqlOperation op)
		{
			if (Operation == op)
			{
				_list.Clear();
				Operation = RqlOperation.NOOP;
			}
			else
			{
				for (int i = 0; i < _list.Count; i++)
				{
					if ( _list[i] is RqlNode childnode)
					{
						if (childnode.Operation == op)
						{
							for (int j = i + 1; j < _list.Count; j++)
								_list[j - 1] = _list[j];

							_list.RemoveAt(_list.Count - 1);
							i--;
						}
						else
						{
							childnode.RemoveAll(op);
						}
					}
				}
			}

			if (Operation == RqlOperation.AND || Operation == RqlOperation.OR)
			{
				if (_list.Count == 0)
				{
					Operation = RqlOperation.NOOP;
				}
				else if (_list.Count == 1)
				{
					var node = (RqlNode)_list[0];

					_list.Clear();
					Operation = node.Operation;
					foreach (var childNode in node)
						_list.Add(childNode);
				}
			}
		}

		/// <summary>
		/// Removes all elements from the <see cref="RqlNode"/>
		/// </summary>
		public void Clear()
		{
			_list.Clear();
		}

		/// <summary>
		/// Copies the values from the source <see cref="RqlNode"/> into the current <see cref="RqlNode"/>
		/// </summary>
		/// <param name="source">The source <see cref="RqlNode"/> to copy values from</param>
		/// <exception cref="InvalidOperationException">An <see cref="InvalidOperationException"/> is thrown if the source node is not the same type as the destination node.</exception>
		public void CopyFrom(RqlNode source)
		{
			if (Operation == source.Operation)
			{
				_list.Clear();

				switch (Operation)
				{
					case RqlOperation.PROPERTY:
						foreach (string member in source)
							Add(member);
						break;

					case RqlOperation.SORTPROPERTY:
						Add(source.NonNullValue<RqlSortOrder>(0));
						Add(Copy(source.NonNullValue<RqlNode>(1)));
						break;

					case RqlOperation.AND:
					case RqlOperation.OR:
					case RqlOperation.SELECT:
					case RqlOperation.SORT:
						foreach (RqlNode child in source)
							Add(Copy(child));
						break;

					case RqlOperation.LIMIT:
						foreach (var val in source)
							Add(val);
						break;

					case RqlOperation.EQ:
					case RqlOperation.LE:
					case RqlOperation.LT:
					case RqlOperation.GE:
					case RqlOperation.GT:
					case RqlOperation.NE:
						Add(Copy(source.NonNullValue<RqlNode>(0)));
						if (source.NonNullValue<object>(1) is string strValue)
							Add(strValue);
						else
							Add(source.NonNullValue<object>(1));
						break;

					case RqlOperation.LIKE:
					case RqlOperation.CONTAINS:
					case RqlOperation.EXCLUDES:
					case RqlOperation.IN:
					case RqlOperation.OUT:
						Add(Copy(source.NonNullValue<RqlNode>(0)));

						for (int i = 1; i < source.Count; i++)
						{
							if (source.Value<object>(i) is string strCompValue)
								Add(strCompValue);
							else
								Add(source.NonNullValue<object>(i));
						}
						break;

					case RqlOperation.DISTINCT:
					case RqlOperation.ONE:
					case RqlOperation.FIRST:
					case RqlOperation.COUNT:
						break;

					default:
						throw new FormatException("Unknown or invalid format");
				}
			}
			else
				throw new InvalidOperationException("RQL Node types must be the same.");
		}

		/// <summary>
		/// Creates a new instance of <see cref="RqlNode"/> with the same value as a specified <see cref="RqlNode"/>.
		/// </summary>
		/// <param name="node">The <see cref="RqlNode"/> to copy</param>
		/// <returns>A new <see cref="RqlNode"/> with the same value as node.</returns>
		/// <exception cref="ArgumentNullException">Throws a <see cref="ArgumentNullException"/> if node is null.</exception>
		/// <exception cref="FormatException">Throws a <see cref="FormatException"/> if the node is improperly formed.</exception>
		public static RqlNode Copy(RqlNode node)
		{
			if (node == null)
				throw new ArgumentNullException(nameof(node));

			var newNode = new RqlNode(node.Operation);

			switch (node.Operation)
			{
				case RqlOperation.PROPERTY:
					foreach (string member in node)
						newNode.Add(member);
					break;

				case RqlOperation.SORTPROPERTY:
					newNode.Add(node.NonNullValue<RqlSortOrder>(0));
					newNode.Add(Copy(node.NonNullValue<RqlNode>(1)));
					break;

				case RqlOperation.EQ:
				case RqlOperation.LE:
				case RqlOperation.LT:
				case RqlOperation.GE:
				case RqlOperation.GT:
				case RqlOperation.NE:
					newNode.Add(Copy(node.NonNullValue<RqlNode>(0)));

					if (node.NonNullValue<object>(1).GetType() == typeof(string))
						newNode.Add(node.NonNullValue<string>(1));
					else
						newNode.Add(node.NonNullValue<object>(1));
					break;

				case RqlOperation.LIMIT:
					foreach (var val in node)
					{
						newNode.Add(val);
					}
					break;

				case RqlOperation.SELECT:
				case RqlOperation.SORT:
				case RqlOperation.SUM:
				case RqlOperation.MAX:
				case RqlOperation.MIN:
				case RqlOperation.MEAN:
				case RqlOperation.AGGREGATE:
				case RqlOperation.AND:
				case RqlOperation.OR:
				case RqlOperation.VALUES:
					foreach (RqlNode child in node)
					{
						newNode.Add(Copy(child));
					}
					break;

				case RqlOperation.LIKE:
				case RqlOperation.CONTAINS:
				case RqlOperation.EXCLUDES:
				case RqlOperation.IN:
				case RqlOperation.OUT:
					newNode.Add(Copy(node.NonNullValue<RqlNode>(0)));

					for (int i = 1; i < node.Count; i++)
					{
						if (node.NonNullValue<object>(i).GetType() == typeof(string))
							newNode.Add(node.NonNullValue<string>(i));
						else
							newNode.Add(node.NonNullValue<object>(i));
					}
					break;

				case RqlOperation.DISTINCT:
				case RqlOperation.ONE:
				case RqlOperation.FIRST:
				case RqlOperation.COUNT:
					break;

				case RqlOperation.NOOP:
					break;

				default:
					throw new FormatException("Unknown or invalid format");
			}

			return newNode;
		}

		/// <summary>
		/// Merges an <see cref="RqlNode"/> into the current <see cref="RqlNode"/>. Certain nodes, such as SELECT and SORT,
		/// will be consolidated into a single node during the merge.
		/// </summary>
		/// <param name="source">The source <see cref="RqlNode"/> to merge.</param>
		public RqlNode Merge(RqlNode source)
		{
			if (source.Operation == RqlOperation.NOOP)
				return this;

			if (this.Operation == RqlOperation.NOOP)
				return source;

            RqlNode? nonConditionals = ExtractNonConditional();

            if (nonConditionals != null)
			{
				var s1 = source.ExtractNonConditional();

				if ( s1 != null )
					nonConditionals.MergeNonConditionals(s1);
			}
			else
			{
				nonConditionals = source.ExtractNonConditional();
			}

			if (source != null)
			{
				switch (source.Operation)
				{
					case RqlOperation.AND:
						if (Operation == RqlOperation.AND)
						{
							foreach (RqlNode child in source)
							{
								if (child.Operation != RqlOperation.NOOP)
									Merge(child);
							}
						}
						else
						{
							var childNode = Copy(this);
							_list.Clear();

							Operation = RqlOperation.AND;

							if (childNode.Operation != RqlOperation.NOOP)
								Add(childNode);

							foreach (RqlNode child in source)
							{
								if (child.Operation != RqlOperation.NOOP)
									Merge(child);
							}
						}
						break;

					case RqlOperation.OR:
						if (Operation == RqlOperation.OR)
						{
							foreach (RqlNode child in source)
							{
								if (child.Operation != RqlOperation.NOOP)
									Merge(child);
							}
						}
						else
						{
							var childNode = Copy(this);
							_list.Clear();

							Operation = RqlOperation.OR;

							if (childNode.Operation != RqlOperation.NOOP)
								Add(childNode);

							foreach (RqlNode child in source)
							{
								if (child.Operation != RqlOperation.NOOP)
									Merge(child);
							}
						}
						break;

					default:
						{
							if (Operation == RqlOperation.AND)
							{
								if (source.Operation != RqlOperation.NOOP)
									Add(Copy(source));
							}
							else
							{
								var childNode = Copy(this);
								_list.Clear();

								Operation = RqlOperation.AND;

								if ( childNode.Operation != RqlOperation.NOOP )
									Add(childNode);

								if (source.Operation != RqlOperation.NOOP)
									Add(Copy(source));
							}
						}
						break;
				}
			}

			if (nonConditionals != null)
			{
				if (Operation != RqlOperation.AND)
				{
					var childNode = Copy(this);
					_list.Clear();

					Operation = RqlOperation.AND;
					Add(childNode);
				}

				if (nonConditionals.Operation == RqlOperation.AND)
				{
					foreach (RqlNode child in nonConditionals)
						Add(Copy(child));
				}
				else if (nonConditionals.Operation == RqlOperation.OR)
				{
					foreach (RqlNode child in nonConditionals)
						Add(Copy(child));
				}
				else
				{
					Add(nonConditionals);
				}
			}

			return this;
		}

		/// <summary>
		/// Returns <see langword="true"/> if the select clause contains the property; <see langword="false"/> otherwise.
		/// </summary>
		/// <param name="propertyName">The name of the property to check</param>
		/// <returns><see langword="true"/> if the select clause contains the property; <see langword="false"/> otherwise.</returns>
		/// <exception cref="InvalidOperationException">Thrown when the node is not a SELECT node.</exception>
		/// <exception cref="ArgumentNullException">Thrown when property is null.</exception>
		public bool SelectContains(string propertyName)
        {
			if (Operation != RqlOperation.SELECT)
				throw new InvalidOperationException("ExtractPropertiesFromSelect can only be performed on a SELECT node.");

			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			foreach (RqlNode childProperty in _list)
			{
				var parts = propertyName.Split('/');
				if (childProperty.Count >= parts.Length)
				{
					bool referencedNode = true;

					for (int i = 0; i < parts.Length && referencedNode; i++)
                    {
						referencedNode &= (string.Equals(parts[i], childProperty.Value<string>(i), StringComparison.OrdinalIgnoreCase));
					}

					if (referencedNode)
						return true;
				}
			}

			return false;
        }

		/// <summary>
		/// Determines if a property is referenced in a node
		/// </summary>
		/// <param name="property">The property node to check</param>
		/// <param name="caseInsensitive">If <see langword="true"/> do not take case into consideration; <see langword="false"/> otherwise.</param>
		/// <returns><see langword="true"/> if the property is referenced; <see langword="false"/> otherwise</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public bool References(RqlNode property, bool caseInsensitive = true)
		{
			if (property == null)
				throw new ArgumentNullException(nameof(property));

			if (property.Operation != RqlOperation.PROPERTY)
				throw new ArgumentException("Property node must be a property node.");

			bool answer = false;

			switch (Operation)
			{
				case RqlOperation.AND:
					foreach (RqlNode child in _list)
					{
						answer |= child.References(property, caseInsensitive);
					}
					break;

				case RqlOperation.OR:
					foreach (RqlNode child in _list)
					{
						answer |= child.References(property, caseInsensitive);
					}
					break;

				case RqlOperation.EQ:
				case RqlOperation.LE:
				case RqlOperation.LT:
				case RqlOperation.GT:
				case RqlOperation.GE:
				case RqlOperation.NE:
				case RqlOperation.LIKE:
				case RqlOperation.CONTAINS:
				case RqlOperation.IN:
				case RqlOperation.OUT:
				case RqlOperation.VALUES:
				case RqlOperation.SUM:
				case RqlOperation.MIN:
				case RqlOperation.MAX:
				case RqlOperation.MEAN:
				case RqlOperation.EXCLUDES:
					{
						RqlNode childProperty = NonNullValue<RqlNode>(0);
						var result = true;

						if (childProperty.Count >= property.Count)
						{
							for (int i = 0; i < property.Count && result; i++)
							{
								if ( caseInsensitive)
									result &= string.Equals(property.Value<string>(i), childProperty.Value<string>(i), StringComparison.OrdinalIgnoreCase);
								else
									result &= string.Equals(property.Value<string>(i), childProperty.Value<string>(i), StringComparison.Ordinal);
							}

							if (result)
								return true;
						}

						return false;
					}

				case RqlOperation.AGGREGATE:
					foreach (RqlNode child in _list)
					{
						if (child.Operation == RqlOperation.PROPERTY)
						{
							var childProperty = NonNullValue<RqlNode>(0);
							var result = true;

							if (childProperty.Count >= property.Count)
							{
								for (int i = 0; i < property.Count && result; i++)
								{
									if (caseInsensitive)
										result &= string.Equals(property.Value<string>(i), childProperty.Value<string>(i), StringComparison.OrdinalIgnoreCase);
									else
										result &= string.Equals(property.Value<string>(i), childProperty.Value<string>(i), StringComparison.Ordinal);
								}

								if (result)
									return true;
							}

							return false;
						}
						else
						{
							answer |= child.References(property, caseInsensitive);
						}
					}
					break;

				case RqlOperation.SELECT:
					{
						foreach (RqlNode childProperty in _list)
						{
							var result = true;

							if ( childProperty.Count >= property.Count)
                            {
								for (int i = 0; i < property.Count && result; i++)
								{
									if (caseInsensitive)
										result &= string.Equals(property.Value<string>(i), childProperty.Value<string>(i), StringComparison.OrdinalIgnoreCase);
									else
										result &= string.Equals(property.Value<string>(i), childProperty.Value<string>(i), StringComparison.Ordinal);
								}

								if (result)
									return true;
							}
						}

						return false;
					}
			}

			return answer;
		}


		/// <summary>
		/// Consolidates all child <see cref="RqlNode"/>s of a specified <see cref="RqlOperation"/> into a single
		/// child <see cref="RqlNode"/>.
		/// </summary>
		/// <param name="op">The <see cref="RqlOperation"/> to consolidate.</param>
		public void Consolidate(RqlOperation op)
		{
			var nodes = FindAll(op);

			if (nodes.Count < 2)
				return;

			RemoveAll(op);

			var targetNode = nodes[0];

			for (int i = 1; i < nodes.Count; i++)
			{
				foreach (var child in nodes[i])
					targetNode.Add(child);
			}

			if (Operation == RqlOperation.NOOP)
			{
				Operation = op;
				_list.Clear();
				foreach (var child in targetNode)
					_list.Add(child);
			}
			else if (Operation == RqlOperation.AND || Operation == RqlOperation.OR)
			{
				_list.Add(targetNode);
			}
			else
			{
				var originalNode = new RqlNode(this.Operation);

				foreach (var child in _list)
					originalNode.Add(child);

				Operation = RqlOperation.AND;
				_list.Clear();
				_list.Add(originalNode);
				_list.Add(targetNode);
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through the list of parameters of the <see cref="RqlNode"/>.
		/// </summary>
		/// <returns>A <see cref="List{Object}.Enumerator"/> for the <see cref="RqlNode"/></returns>
		public List<Object>.Enumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		/// <summary>
		/// Extracts the list of properties used by any aggregate functions
		/// </summary>
		/// <returns></returns>
		public RqlNode ExtractValueField()
		{
			return ExtractValueField(this);
		}

		/// <summary>
		/// Removes the First node that is of <see cref="RqlOperation"/> from the current <see cref="RqlNode"/>.
		/// </summary>
		/// <param name="op">The <see cref="RqlOperation"/> of the node to remove</param>
		/// <returns>The removed <see cref="RqlNode"/>, or <see langword="null"/> if none was found.</returns>
		public RqlNode? Remove(RqlOperation op)
		{
			RqlNode? removedNode = null;

			if (Operation == RqlOperation.AND)
			{
				var newList = new List<object>();

				foreach (RqlNode childNode in _list)
				{
					var xnode = childNode.Remove(op);

					if (xnode == null)
						newList.Add(childNode);
					else
						removedNode = xnode;
				}

				_list.Clear();

				if (newList.Count == 0)
				{
					Operation = RqlOperation.NOOP;
				}
				else if (newList.Count == 1)
				{
					var child = (RqlNode)newList[0];

					Operation = child.Operation;
					_list.AddRange(child._list);
				}
				else
				{
					_list.AddRange(newList);
				}
			}
			else if (Operation == RqlOperation.OR)
			{
				var newList = new List<object>();

				foreach (RqlNode childNode in _list)
				{
					var xnode = childNode.Remove(op);

					if (xnode == null)
						newList.Add(childNode);
					else
						removedNode = xnode;
				}

				_list.Clear();

				if (newList.Count == 0)
				{
					Operation = RqlOperation.NOOP;
				}
				else if (newList.Count == 1)
				{
					var child = (RqlNode)newList[0];

					Operation = child.Operation;
					_list.AddRange(child._list);
				}
				else
				{
					_list.AddRange(newList);
				}
			}
			else if (Operation == op)
			{
				removedNode = Copy(this);
				this.Operation = RqlOperation.NOOP;
				this._list.Clear();
			}

			return removedNode;
		}

		/// <summary>
		/// Determines whether the specified object is equal ot the current object
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><see langword="true"/> if the specified object is equal to the current object; <see langword="false"/> otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (obj.GetType() != typeof(RqlNode))
				return false;

			var candidate = (RqlNode)obj;

			if (Operation == candidate.Operation)
			{
				if (Count == candidate.Count)
				{
					var answer = true;

					for (int i = 0; i < Count; i++)
					{
						if (_list[i].GetType() == typeof(string) && candidate[i].GetType() == typeof(string))
						{
							answer &= string.Equals(_list[i].ToString(), candidate[i].ToString(), StringComparison.OrdinalIgnoreCase);
						}
						else
						{
							answer &= _list[i].Equals(candidate[i]);
						}
					}

					return answer;
				}
			}

			return false;
		}

		/// <summary>
		/// Returns the hash code for this RqlNode
		/// </summary>
		/// <returns>A 32-bit signed interger hash code.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Turn a property name string into a property node
		/// </summary>
		/// <param name="propertyName">The name of the property</param>
		/// <returns>A <see cref="RqlNode"/> that represents the property</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static RqlNode ToProperty(string propertyName)
		{
			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			var result = new RqlNode(RqlOperation.PROPERTY);

			var thePath = propertyName.Replace('\\', '/');
			var parts = thePath.Split('/');
			foreach (var part in parts)
				result.Add(part);

			return result;
		}

		/// <summary>
		/// Returns the property name from a property node.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when requesting the property name from a node that is not a property node.</exception>
		public string PropertyName
        {
            get
            {
				if ( this.Operation == RqlOperation.PROPERTY)
                {
					StringBuilder propertyName = new();
					bool first = true;
					foreach ( var val in _list)
                    {
						if (first)
							first = false;
						else
							propertyName.Append('/');

						propertyName.Append(val.ToString());
                    }

					return propertyName.ToString();
                }

				throw new InvalidOperationException("Property names can only be extracted from property nodes.");
            }
        }

		/// <summary>
		/// Extracts the select clause from the node
		/// </summary>
		/// <returns>An <see cref="RqlNode"/> that contains only the select clause, or null if no select clause was present</returns>
		public RqlNode? ExtractSelectClause()
		{
			RqlNode? selectNode = null;

			if (Contains(RqlOperation.VALUES))
			{
				selectNode = ExtractValueField();
			}
			else if (selectNode == null && HasAggregates())
			{
				selectNode = ExtractAggregateFields();
			}
			else
			{
				selectNode = Find(RqlOperation.SELECT);
			}

			return selectNode;
		}

		/// <summary>
		/// Extracts the limit clause from the node
		/// </summary>
		/// <returns>An <see cref="RqlNode"/> that contains only the select clause, or null if no select clause was present</returns>
		public RqlNode? ExtractLimitClause()
		{
			return Find(RqlOperation.LIMIT);
		}

		/// <summary>
		/// Used to determine of the <see cref="RqlNode"/> contains aggregate functions
		/// </summary>
		/// <returns><see langword="true"/> if the <see cref="RqlNode"/> contains aggregate functions; <see langword="false"/> otherwise.</returns>
		public bool HasAggregates()
		{
			return Find(RqlOperation.MAX) != null ||
				   Find(RqlOperation.MIN) != null ||
				   Find(RqlOperation.MAX) != null ||
				   Find(RqlOperation.SUM) != null ||
				   Find(RqlOperation.MEAN) != null ||
				   Find(RqlOperation.AGGREGATE) != null;
		}

		/// <summary>
		/// Extracts the list of properties used by any aggregate functions
		/// </summary>
		/// <returns></returns>
		public RqlNode ExtractAggregateFields()
		{
			return ExtractAggregateFields(this);
		}

        #region Internal helper functions
		/// <summary>
		/// Extracts the list of properties used by any aggregate functions
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private RqlNode ExtractAggregateFields(RqlNode node)
		{
			var resultNode = new RqlNode(RqlOperation.SELECT);

			switch (node.Operation)
			{
				case RqlOperation.AND:
				case RqlOperation.OR:
					foreach (RqlNode childnode in node)
					{
						var agg = ExtractAggregateFields(childnode);

						foreach (var child in agg)
							resultNode.Add(child);
					}
					break;

				case RqlOperation.MAX:
				case RqlOperation.MIN:
				case RqlOperation.SUM:
				case RqlOperation.MEAN:
					resultNode.Add((RqlNode)node[0]);
					break;

				case RqlOperation.COUNT:
					if (node.Count > 0)
						resultNode.Add((RqlNode)node[0]);
					break;

				case RqlOperation.AGGREGATE:
					foreach (RqlNode childNode in node)
					{
						if (childNode.Operation == RqlOperation.PROPERTY)
						{
							resultNode.Add(childNode);
						}
						else
						{
							resultNode.Add((RqlNode)childNode[0]);
						}
					}
					break;
			}

			return resultNode;
		}

        /// <summary>
        /// Converts the value of a node paramter to a string, used in ToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ConvertValueToString(object value)
		{
			if (value == null)
			{
				return "null";
			}
			if (value.GetType() == typeof(long))
			{
				var str = value.ToString();

				if ((long)value >= int.MinValue && (long)value <= int.MaxValue)
					return str;
				else
					return $"{str}L";
			}
			else if (value.GetType() == typeof(ulong))
			{
				var str = value.ToString();

				if ((ulong)value >= (ulong)uint.MinValue && (ulong)value <= (ulong)uint.MaxValue)
					return $"{str}U";
				else
					return $"{str}UL";
			}
			else if (value.GetType() == typeof(uint))
			{
				var str = value.ToString();
				return $"{str}U";
			}
			else if (value.GetType() == typeof(decimal))
			{
				var str = value.ToString();
				return $"{str}M";
			}
			else if (value.GetType() == typeof(float))
			{
				var str = value.ToString();
				return $"{str}F";
			}
			else if (value.GetType() == typeof(string))
			{
				return $"\"{value}\"";
			}
			else if (value.GetType() == typeof(DateTime))
			{
				return ((DateTime)value).ToString("o");
			}
			else if (value.GetType() == typeof(char))
			{
				return $"'{value}'";
			}

			return value.ToString();
		}

		private bool IsEqualProperty(RqlNode member)
		{
			bool areEqual = true;

			if (Operation == RqlOperation.PROPERTY)
			{
				if (Count == member.Count)
				{
					for (int i = 0; i < Count; i++)
					{
						if (!string.Equals(Value<string>(i), member.Value<string>(i), StringComparison.OrdinalIgnoreCase))
						{
							areEqual = false;
						}
					}
				}
				else
					areEqual = false;
			}
			else
				areEqual = false;

			return areEqual;
		}

		private RqlNode? ExtractNonConditional()
		{
			RqlNode? staticNode = null;

			switch (Operation)
			{
				case RqlOperation.AND:
					{
						var newList = new List<object>();

						foreach (RqlNode child in _list)
						{
							var s1 = child.ExtractNonConditional();

							if (child.Operation != RqlOperation.NOOP)
								newList.Add(child);

							if (s1 != null)
							{
								if (staticNode == null)
									staticNode = s1;
								else if (staticNode.Operation == RqlOperation.AND)
								{
									staticNode.Add(s1);
								}
								else
								{
									var s2 = Copy(staticNode);
									staticNode = new RqlNode(RqlOperation.AND);
									staticNode.Add(s2);
									staticNode.Add(s1);
								}
							}
						}

						_list.Clear();
						_list.AddRange(newList);

						if (_list.Count == 0)
						{
							Operation = RqlOperation.NOOP;
						}
						else if (_list.Count == 1 && NonNullValue<RqlNode>(0).Operation == RqlOperation.NOOP)
						{
							Operation = RqlOperation.NOOP;
							_list.Clear();
						}
						else if (_list.Count == 1 && NonNullValue<RqlNode>(0).Operation != RqlOperation.NOOP)
						{
							var v = Copy(NonNullValue<RqlNode>(0));
							Operation = v.Operation;
							_list.Clear();
							_list.AddRange(v._list);
						}
					}
					break;

				case RqlOperation.OR:
					{
						var newList = new List<object>();

						foreach (RqlNode child in _list)
						{
							var s1 = child.ExtractNonConditional();

							if (child.Operation != RqlOperation.NOOP)
								newList.Add(child);

							if (s1 != null)
							{
								if (staticNode == null)
									staticNode = s1;
								else if (staticNode.Operation == RqlOperation.AND)
								{
									staticNode.Add(s1);
								}
								else
								{
									var s2 = Copy(staticNode);
									staticNode = new RqlNode(RqlOperation.AND);
									staticNode.Add(s2);
									staticNode.Add(s1);
								}
							}
						}

						_list.Clear();
						_list.AddRange(newList);

						if (_list.Count == 0)
						{
							Operation = RqlOperation.NOOP;
						}
						else if (_list.Count == 1 && NonNullValue<RqlNode>(0).Operation == RqlOperation.NOOP)
						{
							Operation = RqlOperation.NOOP;
							_list.Clear();
						}
						else if (_list.Count == 1 && NonNullValue<RqlNode>(0).Operation != RqlOperation.NOOP)
						{
							var v = Copy(NonNullValue<RqlNode>(0));
							Operation = v.Operation;
							_list.Clear();
							_list.AddRange(v._list);
						}
					}
					break;

				case RqlOperation.SELECT:
				case RqlOperation.LIMIT:
				case RqlOperation.SORT:
				case RqlOperation.VALUES:
				case RqlOperation.DISTINCT:
				case RqlOperation.FIRST:
				case RqlOperation.ONE:
				case RqlOperation.COUNT:
				case RqlOperation.MAX:
				case RqlOperation.MIN:
				case RqlOperation.MEAN:
				case RqlOperation.SUM:
				case RqlOperation.AGGREGATE:
					staticNode = Copy(this);
					Operation = RqlOperation.NOOP;
					_list.Clear();
					break;

				case RqlOperation.NOOP:
					return null;

				default:
					return null;
			}

			return staticNode;
		}
		private void MergeNonConditionals(RqlNode source)
		{
			if (source == null)
				return;

			if (source.Operation == RqlOperation.NOOP)
				return;

			if (source != null)
			{
				switch (source.Operation)
				{
					case RqlOperation.AND:
						if (Operation == RqlOperation.AND)
						{
							foreach (RqlNode child in source)
							{
								MergeNonConditionals(Copy(child));
							}
						}
						else
						{
							var childNode = Copy(this);
							Operation = RqlOperation.AND;
							_list.Clear();
							Add(childNode);

							foreach (RqlNode child in source)
							{
								MergeNonConditionals(Copy(child));
							}
						}
						break;

					case RqlOperation.OR:
						if (Operation == RqlOperation.OR)
						{
							foreach (RqlNode child in source)
							{
								MergeNonConditionals(Copy(child));
							}
						}
						else
						{
							var childNode = Copy(this);
							Operation = RqlOperation.OR;
							_list.Clear();
							Add(childNode);

							foreach (RqlNode child in source)
							{
								MergeNonConditionals(Copy(child));
							}
						}
						break;

					case RqlOperation.LIMIT:
						if (Find(RqlOperation.LIMIT) != null)
						{
							throw new RqlFormatException("Conflict found. Duplicate limit clause.");
						}
						else if (Find(RqlOperation.MIN) != null)
						{
							throw new RqlFormatException("An RQL Query that contains a MIN clause outside of an AGGREGATE cannot contain a LIMIT clause.");
						}
						else if (Find(RqlOperation.MAX) != null)
						{
							throw new RqlFormatException("An RQL Query that contains a MAX clause outside of an AGGREGATE cannot contain a LIMIT clause.");
						}
						else if (Find(RqlOperation.COUNT) != null)
						{
							throw new RqlFormatException("An RQL Query that contains a COUNT clause outside of an AGGREGATE cannot contain a LIMIT clause.");
						}
						else if (Find(RqlOperation.MEAN) != null)
						{
							throw new RqlFormatException("An RQL Query that contains a MEAN clause outside of an AGGREGATE cannot contain a LIMIT clause.");
						}
						else if (Find(RqlOperation.SUM) != null)
						{
							throw new RqlFormatException("An RQL Query that contains a SUM clause outside of an AGGREGATE cannot contain a LIMIT clause.");
						}
						else if (Find(RqlOperation.AGGREGATE) != null)
						{
							throw new RqlFormatException("An RQL Query that contains an AGGREGATE clause cannot contain a LIMIT clause.");
						}
						else
						{
							Append(source);
						}
						break;

					case RqlOperation.FIRST:
						if (Find(RqlOperation.FIRST) != null)
						{
							throw new RqlFormatException("Conflict found. Duplicate first clause.");
						}
						else
						{
							Append(source);
						}
						break;

					case RqlOperation.DISTINCT:
						if (Find(RqlOperation.DISTINCT) != null)
						{
							throw new RqlFormatException("Conflict found. Duplicate distinct clause.");
						}
						else
						{
							Append(source);
						}
						break;

					case RqlOperation.ONE:
						if (Find(RqlOperation.ONE) != null)
						{
							throw new RqlFormatException("Conflict found. Duplicate one clause.");
						}
						else
						{
							Append(source);
						}
						break;

					case RqlOperation.MIN:
						{
							if (Find(RqlOperation.AGGREGATE) != null)
							{
								throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a MIN clause outside of that AGGREGATE clause.");
							}
							else if (Find(RqlOperation.LIMIT) != null)
							{
								throw new RqlFormatException("An RQL Query that contains a MIN clause outside of an AGGREGATE cannot contain a LIMIT clause.");
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.MAX:
						{
							if (Find(RqlOperation.AGGREGATE) != null)
							{
								throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a MAX clause outside of that AGGREGATE clause.");
							}
							else if (Find(RqlOperation.LIMIT) != null)
							{
								throw new RqlFormatException("An RQL Query that contains a MAX clause outside of an AGGREGATE cannot contain a LIMIT clause.");
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.MEAN:
						{
							if (Find(RqlOperation.AGGREGATE) != null)
							{
								throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a MEAN clause outside of that AGGREGATE clause.");
							}
							else if (Find(RqlOperation.LIMIT) != null)
							{
								throw new RqlFormatException("An RQL Query that contains a MEAN clause outside of an AGGREGATE cannot contain a LIMIT clause.");
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.COUNT:
						{
							if (Find(RqlOperation.AGGREGATE) != null)
							{
								throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a COUNT clause outside of that AGGREGATE clause.");
							}
							else if (Find(RqlOperation.LIMIT) != null)
							{
								throw new RqlFormatException("An RQL Query that contains a COUNT clause outside of an AGGREGATE cannot contain a LIMIT clause.");
							}
							else if (Find(RqlOperation.COUNT) != null)
							{
								throw new RqlFormatException("An RQL Query cannot contain more than one COUNT clause.");
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.SUM:
						{
							if (Find(RqlOperation.AGGREGATE) != null)
							{
								throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a SUM clause outside of that AGGREGATE clause.");
							}
							else if (Find(RqlOperation.LIMIT) != null)
							{
								throw new RqlFormatException("An RQL Query that contains a SUM clause outside of an AGGREGATE cannot contain a LIMIT clause.");
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.AGGREGATE:
						if (Find(RqlOperation.AGGREGATE) != null)
						{
							throw new RqlFormatException("Conflict found. Duplicate aggregate clause.");
						}
						else if (Find(RqlOperation.SUM) != null)
						{
							throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a SUM clause outside of that AGGREGATE clause.");
						}
						else if (Find(RqlOperation.MEAN) != null)
						{
							throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a MEAN clause outside of that AGGREGATE clause.");
						}
						else if (Find(RqlOperation.COUNT) != null)
						{
							throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a COUNT clause outside of that AGGREGATE clause.");
						}
						else if (Find(RqlOperation.MAX) != null)
						{
							throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a MAX clause outside of that AGGREGATE clause.");
						}
						else if (Find(RqlOperation.MIN) != null)
						{
							throw new RqlFormatException("An RQL query that contains an AGGREGATE clause cannot contain a MIN clause outside of that AGGREGATE clause.");
						}
						else if (Find(RqlOperation.LIMIT) != null)
						{
							throw new RqlFormatException("An RQL Query that contains an AGGREGATE clause cannot contain a LIMIT clause.");
						}
						else
						{
							Append(source);
						}
						break;

					case RqlOperation.SELECT:
						{
							var selectNode = Find(RqlOperation.SELECT);

							if (selectNode != null)
							{
								for (int i = 0; i < source.Count; i++)
								{
									bool foundIt = false;

									for (int j = 0; j < selectNode.Count; j++)
									{
										if (selectNode.NonNullValue<RqlNode>(j).IsEqualProperty(source.NonNullValue<RqlNode>(i)))
										{
											foundIt = true;
											break;
										}
									}

									if (!foundIt)
										selectNode.Add(source.NonNullValue<RqlNode>(i));
								}
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.SORT:
						{
							var sortNode = Find(RqlOperation.SORT);

							if (sortNode != null)
							{
								for (int i = 0; i < source.Count; i++)
									sortNode.Add(source.NonNullValue<RqlNode>(i));
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.IN:
						{
							var inNode = Find(RqlOperation.IN, NonNullValue<RqlNode>(0));

							if (inNode != null)
							{
								for (int i = 1; i < source.Count; i++)
								{
									bool foundIt = false;
									for (int j = 1; j < inNode.Count; j++)
									{
										if (string.Equals(inNode.Value<string>(j), source.Value<string>(i)))
										{
											foundIt = true;
											break;
										}
									}

									if (!foundIt)
										inNode.Add(source.NonNullValue<string>(i));
								}
							}
							else
							{
								Append(source);
							}
						}
						break;

					case RqlOperation.OUT:
						{
							var outNode = Find(RqlOperation.OUT, NonNullValue<RqlNode>(0));

							if (outNode != null)
							{
								for (int i = 1; i < source.Count; i++)
								{
									bool foundIt = false;
									for (int j = 1; j < outNode.Count; j++)
									{
										if (string.Equals(outNode.Value<string>(j), source.Value<string>(i)))
										{
											foundIt = true;
											break;
										}
									}

									if (!foundIt)
										outNode.Add(source.NonNullValue<string>(i));
								}
							}
							else
							{
								Append(source);
							}
						}
						break;

					default:
						Append(source);
						break;
				}
			}
		}

		private void Append(RqlNode source)
		{
			if (Operation == RqlOperation.AND)
			{
				Add(Copy(source));
			}
			else
			{
				var s1 = Copy(this);
				Operation = RqlOperation.AND;
				_list.Clear();
				Add(s1);
				Add(Copy(source));
			}
		}

		/// <summary>
		/// Purge the source <see cref="RqlNode"/> of any elements referenced by this node
		/// </summary>
		/// <param name="source">The source <see cref="RqlNode"/> to purge</param>
		/// <param name="caseInsensitive">If <see langword="true"/> then searchs for properties do not take case into consideration; if <see langword="false"/> they do.</param>
		/// <returns>The source <see cref="RqlNode"/> purged of conflicting elements</returns>
		private RqlNode? Purge(RqlNode source, bool caseInsensitive)
		{
			RqlNode? result = null;

			switch (source.Operation)
			{
				case RqlOperation.AND:
					{
						result = new RqlNode(RqlOperation.AND);

						foreach (RqlNode node in source)
						{
							var childNode = Purge(node, caseInsensitive);

							if (childNode != null)
								result.Add(childNode);
						}

						if (result.Count == 0)
							return null;
						else if (result.Count == 1)
							return result.Value<RqlNode>(0);
					}
					break;

				case RqlOperation.OR:
					{
						result = new RqlNode(RqlOperation.OR);

						foreach (RqlNode node in source)
						{
							var childNode = Purge(node, caseInsensitive);

							if (childNode != null)
								result.Add(childNode);
						}

						if (result.Count == 0)
							return null;
						else if (result.Count == 1)
							return result.Value<RqlNode>(0);
					}
					break;

				case RqlOperation.SELECT:
					if (!Contains(RqlOperation.SELECT))
					{
						return Copy(source);
					}
					break;

				case RqlOperation.LIMIT:
					if (!Contains(RqlOperation.LIMIT))
					{
						return Copy(source);
					}
					break;

				case RqlOperation.SORT:
					if (!Contains(RqlOperation.SORT))
					{
						return Copy(source);
					}
					break;

				case RqlOperation.EQ:
				case RqlOperation.LE:
				case RqlOperation.LT:
				case RqlOperation.GT:
				case RqlOperation.GE:
				case RqlOperation.NE:
				case RqlOperation.LIKE:
				case RqlOperation.CONTAINS:
				case RqlOperation.EXCLUDES:
				case RqlOperation.IN:
				case RqlOperation.OUT:
				case RqlOperation.VALUES:
				case RqlOperation.AGGREGATE:
				case RqlOperation.MAX:
				case RqlOperation.MIN:
				case RqlOperation.MEAN:
				case RqlOperation.SUM:
					if (!References(source.NonNullValue<RqlNode>(0), caseInsensitive))
					{
						return Copy(source);
					}
					break;

				case RqlOperation.DISTINCT:
					if (!Contains(RqlOperation.DISTINCT))
					{
						return Copy(source);
					}
					break;

				case RqlOperation.ONE:
					if (!Contains(RqlOperation.ONE))
					{
						return Copy(source);
					}
					break;

				case RqlOperation.FIRST:
					if (!Contains(RqlOperation.FIRST))
					{
						return Copy(source);
					}
					break;
			}

			return result;
		}

		internal RqlNode ExtractValueField(RqlNode node)
		{
			var resultNode = new RqlNode(RqlOperation.SELECT);

			switch (node.Operation)
			{
				case RqlOperation.AND:
				case RqlOperation.OR:
					foreach (RqlNode childnode in node)
					{
						var selectNode = ExtractValueField(childnode);
						if (selectNode != null)
							return selectNode;
					}
					break;

				case RqlOperation.VALUES:
					{
						resultNode.Add(node.NonNullValue<RqlNode>(0));
					}
					break;
			}

			return resultNode;
		}
		#endregion
	}
}
