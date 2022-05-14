using System.Collections.Generic;

namespace Tense.Rql
{
	internal static class RqlUtilities
	{
		/// <summary>
		/// Extracts the list of items used in a where clause
		/// </summary>
		/// <returns>The list of keys found in the <see cref="RqlNode"/></returns>
		internal static List<KeyValuePair<string, object>> ExtractKeyList(this RqlNode node)
		{
			var theList = new List<KeyValuePair<string, object>>();

			if (node != null)
			{
				if (node.Operation == RqlOperation.AND)
				{
					foreach (RqlNode child in node)
					{
						theList.AddRange(child.ExtractKeyList());
					}
				}
				else if (node.Operation == RqlOperation.OR)
				{
					foreach (RqlNode child in node)
					{
						theList.AddRange(child.ExtractKeyList());
					}
				}
				else if (node.Operation == RqlOperation.EQ)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.NE)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.LT)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.LE)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.GT)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.GE)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.DISTINCT)
				{
				}
				else if (node.Operation == RqlOperation.LIMIT)
				{
				}
				else if (node.Operation == RqlOperation.SELECT)
				{
				}
				else if (node.Operation == RqlOperation.SORT)
				{
				}
				else if (node.Operation == RqlOperation.IN)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.OUT)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.SUM)
				{
				}
				else if (node.Operation == RqlOperation.MAX)
				{
				}
				else if (node.Operation == RqlOperation.MIN)
				{
				}
				else if (node.Operation == RqlOperation.MEAN)
				{
				}
				else if (node.Operation == RqlOperation.VALUES)
				{
				}
				else if (node.Operation == RqlOperation.COUNT)
				{
				}
				else if (node.Operation == RqlOperation.FIRST)
				{
				}
				else if (node.Operation == RqlOperation.ONE)
				{
				}
				else if (node.Operation == RqlOperation.AGGREGATE)
				{
				}
				else if (node.Operation == RqlOperation.CONTAINS || node.Operation == RqlOperation.LIKE)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
				else if (node.Operation == RqlOperation.EXCLUDES)
				{
					theList.Add(new KeyValuePair<string, object>(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0), node.NonNullValue<object>(1)));
				}
			}

			return theList;
		}
	}
}
