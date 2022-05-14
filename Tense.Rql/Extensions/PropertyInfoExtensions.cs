using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Tense.Rql
{
    /// <summary>
    /// PropertyInfo extension functions
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets the non-nullable type for the property
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static Type GetNonNullableType(this PropertyInfo propertyInfo)
        {
            var propertyType = propertyInfo.PropertyType;

            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyType = Nullable.GetUnderlyingType(propertyType);
            }

            return propertyType;
        }
    }
}
