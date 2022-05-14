using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Tense.Rql
{
    /// <summary>
    /// Hateoas Result Filter
    /// </summary>
    public class RqlFilter : IResultFilter
    {
        /// <summary>
        /// Instantiates the filter
        /// </summary>
        public RqlFilter()
        {
        }

        /// <summary>
        /// OnResultExecuting
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is not ObjectResult objectResult) return;

            if (objectResult.StatusCode != null && objectResult.StatusCode.Value >= 300)
                return;

            SetResponseContentType(context);

            var node = RqlNode.Parse(context.HttpContext.Request.QueryString.Value);
            var selectNode = node.ExtractSelectClause();
            objectResult.Value = GenerateRqlResponse(objectResult.Value, selectNode);
        }

        private void SetResponseContentType(ResultExecutingContext context)
        {
            var acceptHeader = context.HttpContext.Request.Headers.FirstOrDefault(h => h.Key.Equals("Accept", StringComparison.OrdinalIgnoreCase));

            if (acceptHeader.Key != null)
            {
                string style = string.Empty;
                string version = string.Empty;
                string media = string.Empty;

                foreach (var headerValue in acceptHeader.Value)
                {
                    var match = Regex.Match(headerValue, "application\\/(?<style>[a-z-A-Z0-9]+)\\.v(?<version>[0-9]+)\\+(?<media>.*)", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        style = match.Groups["style"].Value;
                        version = match.Groups["version"].Value;
                        media = match.Groups["media"].Value;
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(style) &&
                    !string.IsNullOrWhiteSpace(version) &&
                    !string.IsNullOrWhiteSpace(media))
                {
                    context.HttpContext.Response.ContentType = $"application/{style}.v{version}+{media}";
                }
            }
        }

        private object? GenerateRqlResponse(object? value, RqlNode? selectNode)
        {
            if (value == null)
            {
                return null;
            }

            IDictionary<string, object?> result = AddPropertiesAndEmbedded(value, selectNode);

            return result;

        }

        private IDictionary<string, object?> AddPropertiesAndEmbedded(object obj, RqlNode? selectNode)
        {
            IDictionary<string, object?> properties = new ExpandoObject();

            if (obj.GetType().IsGenericType)
            {
                var genericType = obj.GetType().GetGenericTypeDefinition();
                var genArgs = obj.GetType().GetGenericArguments();
                var typedVariant = genericType.MakeGenericType(genArgs);
                FieldInfo[] thisFieldInfo = typedVariant.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var field in thisFieldInfo)
                {
                    var propertyValue = field.GetValue(obj);
                    var propertyName = $"{field.Name[..1].ToLower()}{field.Name[1..]}";

                    if (propertyValue is null)
                    {
                        continue;
                    }

                    if ( propertyName.Equals("_links", StringComparison.OrdinalIgnoreCase))
                    {
                        properties.Add(propertyName, propertyValue);
                        continue;
                    }

                    if (propertyValue is not IEnumerable<object>)
                    {
                        properties.Add(propertyName, propertyValue);
                        continue;
                    }

                    var content = propertyValue is IEnumerable<object> list
                        ? list.ToList().Select(x => GenerateRqlResponse(x, selectNode)).ToList()
                        : GenerateRqlResponse(propertyValue, selectNode);

                    properties.Add(propertyName, content);
                }
            }
            if (obj.GetType() == typeof(ExpandoObject))
            {
                foreach (var property in (IDictionary<string, Object>)obj)
                {
                    var propertyValue = property.Value;
                    var propertyName = $"{property.Key[..1].ToLower()}{property.Key[1..]}";

                    if (propertyValue is null)
                    {
                        continue;
                    }

                    if (propertyName.Equals("_links", StringComparison.OrdinalIgnoreCase) )
                    {
                        properties.Add(propertyName, propertyValue);
                        continue;
                    }

                    if (propertyValue is not IEnumerable<object>)
                    {
                        if (selectNode == null)
                        {
                            properties.Add(propertyName, propertyValue);
                        }
                        else
                        {
                            if ( propertyValue.GetType() == typeof(ExpandoObject))
                            {
                                var exvalue = propertyValue is IEnumerable<object> xlist
                                    ? xlist.ToList().Select(x => GenerateRqlResponse(x, selectNode)).ToList()
                                    : GenerateRqlResponse(propertyValue, selectNode);

                                properties.Add(propertyName, exvalue);
                            }
                            else if (propertyName.Equals("totalRecordsInSet", StringComparison.OrdinalIgnoreCase))
                                properties.Add(propertyName, propertyValue);
                            else if (propertyName.Equals("start", StringComparison.OrdinalIgnoreCase))
                                properties.Add(propertyName, propertyValue);
                            else if (propertyName.Equals("pageSize", StringComparison.OrdinalIgnoreCase))
                                properties.Add(propertyName, propertyValue);
                            else if (selectNode.SelectContains(propertyName))
                                properties.Add(propertyName, propertyValue);
                        }
                        continue;
                    }

                    var content = propertyValue is IEnumerable<object> list
                        ? list.ToList().Select(x => GenerateRqlResponse(x, selectNode)).ToList()
                        : GenerateRqlResponse(propertyValue, selectNode);

                    properties.Add(propertyName, content);
                }
            }
            else
            {
                foreach (PropertyInfo property in obj.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(obj);
                    var propertyName = $"{property.Name[..1].ToLower()}{property.Name[1..]}";
                    var propertyType = property.PropertyType;

                    if (propertyValue is null)
                    {
                        continue;
                    }

                    if (propertyName.Equals("_links", StringComparison.OrdinalIgnoreCase))
                    {
                        properties.Add(propertyName, propertyValue);
                        continue;
                    }

                    if (propertyValue is not IEnumerable<object>)
                    {
                        if (selectNode == null)
                        {
                            properties.Add(propertyName, propertyValue);
                        }
                        else
                        {
                            if (selectNode.SelectContains(propertyName))
                                properties.Add(propertyName, propertyValue);
                        }
                        continue;
                    }

                    var content = propertyValue is IEnumerable<object> list
                        ? list.ToList().Select(x => GenerateRqlResponse(x, selectNode)).ToList()
                        : GenerateRqlResponse(propertyValue, selectNode);

                    properties.Add(propertyName, content);
                }
            }

            return properties;
        }

        /// <summary>
        /// OnResultExecuted
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
