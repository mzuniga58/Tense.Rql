using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tense.Rql.UnitTests
{
    [TestClass]
    public class SortFormatTests
    {
        #region short Formats
        [TestMethod]
        public void SortFormat_01()
        {
            try
            {
                var node = RqlNode.Parse("sort(FirstName)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.SORT);
                Assert.IsTrue(node.Count == 1);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.SORTPROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 2);

                var sortProperty = node.NonNullValue<RqlNode>(0);
                var order = sortProperty.NonNullValue<RqlSortOrder>(0);
                var property = sortProperty.NonNullValue<RqlNode>(1);

                Assert.IsTrue(order == RqlSortOrder.Ascending);
                Assert.IsTrue(property.NonNullValue<string>(0).Equals("FirstName"));

            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void SortFormat_02()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName, FirstName)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.SORT);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.SORTPROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 2);

                var sortProperty = node.NonNullValue<RqlNode>(0);
                var order = sortProperty.NonNullValue<RqlSortOrder>(0);
                var property = sortProperty.NonNullValue<RqlNode>(1);

                Assert.IsTrue(order == RqlSortOrder.Ascending);
                Assert.IsTrue(property.NonNullValue<string>(0).Equals("LastName"));

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(1).Operation == RqlOperation.SORTPROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(1).Count == 2);

                sortProperty = node.NonNullValue<RqlNode>(1);
                order = sortProperty.NonNullValue<RqlSortOrder>(0);
                property = sortProperty.NonNullValue<RqlNode>(1);

                Assert.IsTrue(order == RqlSortOrder.Ascending);
                Assert.IsTrue(property.NonNullValue<string>(0).Equals("FirstName"));

            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion
    }
}
