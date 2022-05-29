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

        [TestMethod]
        public void SortFormat_03()
        {
            try
            {
                var node = RqlNode.Parse("sort(");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_04()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName, FirstName");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting closing ).");
            }
        }

        [TestMethod]
        public void SortFormat_05()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_06()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,-");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_07()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,%2b");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_08()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,(");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at (, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_09()
        {
            try
            {
                var node = RqlNode.Parse("sort");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting (.");
            }
        }

        [TestMethod]
        public void SortFormat_10()
        {
            try
            {
                var node = RqlNode.Parse("sort(&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_11()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName, FirstName&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expected PROPERTY or closing ).");
            }
        }

        [TestMethod]
        public void SortFormat_12()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_13()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,-&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &.");
            }
        }

        [TestMethod]
        public void SortFormat_14()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,%2b&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &.");
            }
        }

        [TestMethod]
        public void SortFormat_15()
        {
            try
            {
                var node = RqlNode.Parse("sort(LastName,&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting PROPERTY.");
            }
        }

        [TestMethod]
        public void SortFormat_16()
        {
            try
            {
                var node = RqlNode.Parse("sort&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting (.");
            }
        }
    }
}
        