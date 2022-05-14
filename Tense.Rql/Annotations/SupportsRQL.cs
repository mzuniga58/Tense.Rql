using System;

namespace Tense.Rql
{
    /// <summary>
    /// Place this on a controller method to allow the method to accept RQL from the Swagger UI
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
	public class SupportRQLAttribute : Attribute
	{
	}
}
