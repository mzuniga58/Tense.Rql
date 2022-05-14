using System;
using System.Collections.Generic;
using System.Text;

namespace Tense.Rql
{
	/// <summary>
	/// RQL Sort Property
	/// </summary>
	public class RqlSortProperty
	{
		/// <summary>
		/// True if ascending; false otherwise
		/// </summary>
		public bool Ascending { get; set; }

		/// <summary>
		/// The list of property names
		/// </summary>
		public RqlNode PropertyNames { get; set; }

		/// <summary>
		/// Instantiates an RQL Sort Property
		/// </summary>
		/// <param name="ascending"></param>
		public RqlSortProperty(bool ascending)
		{
			Ascending = ascending;
			PropertyNames = new RqlNode(RqlOperation.PROPERTY);
		}

		/// <summary>
		/// Returns a string that represents the object
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var result = new StringBuilder();

			if (!Ascending)
				result.Append("-");

			bool First = true;

			foreach (var v in PropertyNames)
			{
				if (First)
					First = false;
				else
					result.Append("/");

				result.Append(v);
			}

			return result.ToString();
		}
	}
}
