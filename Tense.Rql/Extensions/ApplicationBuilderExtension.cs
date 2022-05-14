using Microsoft.AspNetCore.Builder;

namespace Tense.Rql
{
    /// <summary>
    /// RQL Handler Extension
    /// </summary>
	public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Use RQL Handler
        /// </summary>
        /// <param name="builder">The applicationBuilder</param>
        /// <returns></returns>
        public static IApplicationBuilder UseRql(
                    this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RqlMiddleware>();
        }
    }
}
