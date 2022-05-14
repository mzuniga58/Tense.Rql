using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tense.Rql
{
	/// <summary>
	/// Api Swagger Filter
	/// </summary>
	public class RqlSwaggerFilter : IOperationFilter
	{
		/// <summary>
		/// Instantiates an Api Swagger Filter
		/// </summary>
		public RqlSwaggerFilter()
		{
		}

		/// <summary>
		/// Applies the filter
		/// </summary>
		/// <param name="operation">The operation</param>
		/// <param name="context">The context</param>
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			ValidateInput(operation, context);

			var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
				.Union(context.MethodInfo.GetCustomAttributes(true))
				.OfType<AuthorizeAttribute>();

			if (authAttributes.Any())
			{
				if (!operation.Responses.ContainsKey("403"))
					operation.Responses.Add("403", new OpenApiResponse { Description = "Unauthorized: The user is not allowed to access this endpoint." });
			}

			if (IsMethodWithSupportRQLAttribute(context))
			{
				if (!operation.Responses.ContainsKey("400"))
				{
					operation.Responses.Add("400", new OpenApiResponse { Description = "Bad Request: The user has supplied a malformed RQL query." });
				}

				operation.Parameters.Add(new OpenApiParameter
				{
					Name = "RQL",
					In = ParameterLocation.Query,
					Description = "The RQL query",
					Schema = new OpenApiSchema() { Type = "string" },
					Required = false
				});
			}
		}

		private static void ValidateInput(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation == null)
			{
				throw new ArgumentNullException(nameof(operation));
			}

			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (operation.Parameters == null)
			{
				operation.Parameters = new List<OpenApiParameter>();
			}
		}

		private static bool IsMethodWithSupportRQLAttribute(OperationFilterContext context)
		{
			return context.MethodInfo.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(SupportRQLAttribute));
		}
	}
}
