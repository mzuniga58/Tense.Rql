using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Tense.Rql
{
    /// <summary>
    /// RQL Handler Middleware
    /// </summary>
	public class RqlMiddleware
    {
        private readonly RequestDelegate _Next;

        /// <summary>
        /// Instantiates an RQL HAndler middleware component
        /// </summary>
        /// <param name="Next"></param>
        public RqlMiddleware(RequestDelegate Next)
        {
            _Next = Next;
        }

        /// <summary>
        /// Invoked on every call
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString.ToString()))
            {
                var queryString = context.Request.QueryString.ToString();

                if (queryString.StartsWith("?RQL="))
                    queryString = $"?{queryString[5..]}";

                context.Request.QueryString = new QueryString(queryString);
            }

            await _Next(context);
        }
    }
}
