using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tense.Rql.UnitTests
{
    [TestClass]
    public class SelectFormatTests
    {
        [TestMethod]
        public void SelectFormat_01()
        {
            try
            {
                var node = RqlNode.Parse("select(FirstName)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.SELECT);
                Assert.IsTrue(node.Count == 1);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));

                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);

                var selectProperty = node.NonNullValue<RqlNode>(0);
                Assert.IsTrue(selectProperty.NonNullValue<string>(0).Equals("FirstName"));

            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void SelectFormat_02()
        {
            try
            {
                var node = RqlNode.Parse("select(FirstName,LastName)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.SELECT);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));

                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                var selectProperty = node.NonNullValue<RqlNode>(0);
                Assert.IsTrue(selectProperty.NonNullValue<string>(0).Equals("FirstName"));

                Assert.IsTrue(node.NonNullValue<RqlNode>(1).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(1).Count == 1);
                selectProperty = node.NonNullValue<RqlNode>(1);
                Assert.IsTrue(selectProperty.NonNullValue<string>(0).Equals("LastName"));

            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void SelectFormat_03()
        {
            try
            {
                var node = RqlNode.Parse("select");

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
        public void SelectFormat_04()
        {
            try
            {
                var node = RqlNode.Parse("select(");

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
        public void SelectFormat_05()
        {
            try
            {
                var node = RqlNode.Parse("select(FirstName");

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
        public void SelectFormat_06()
        {
            try
            {
                var node = RqlNode.Parse("select(FirstName,");

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
        public void SelectFormat_07()
        {
            try
            {
                var node = RqlNode.Parse("select&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting (.");
            }
        }

        [TestMethod]
        public void SelectFormat_08()
        {
            try
            {
                var node = RqlNode.Parse("select(&Id=10");

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
        public void SelectFormat_09()
        {
            try
            {
                var node = RqlNode.Parse("select(FirstName&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting PROPERTY or closing ).");
            }
        }

        [TestMethod]
        public void SelectFormat_10()
        {
            try
            {
                var node = RqlNode.Parse("select(FirstName,&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &.");
            }
        }
    }
}
