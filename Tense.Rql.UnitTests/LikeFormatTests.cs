using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tense.Rql.UnitTests
{
    [TestClass]
    public class LikeFormatTests
    {
        [TestMethod]
        public void LikeFormat_01()
        {
            try
            {
                var node = RqlNode.Parse("Like(Lastname,T*)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.LIKE);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));

                var propertyNode = node.NonNullValue<RqlNode>(0);
                Assert.AreEqual(propertyNode.Operation, RqlOperation.PROPERTY);
                Assert.AreEqual(propertyNode.NonNullValue<string>(0), "Lastname");

                Assert.IsInstanceOfType(node[1], typeof(string));
                Assert.AreEqual(node.NonNullValue<string>(1), "T*");
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void LikeFormat_02()
        {
            try
            {
                var node = RqlNode.Parse("like");

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
        public void LikeFormat_03()
        {
            try
            {
                var node = RqlNode.Parse("like(");

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
        public void LikeFormat_04()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting VALUE.");
            }
        }

        [TestMethod]
        public void LikeFormat_05()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting VALUE.");
            }
        }

        [TestMethod]
        public void LikeFormat_06()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,T*");

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
        public void LikeFormat_07()
        {
            try
            {
                var node = RqlNode.Parse("like&Id=10");

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
        public void LikeFormat_08()
        {
            try
            {
                var node = RqlNode.Parse("like(&Id=10");

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
        public void LikeFormat_09()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName&Id=10");

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
        public void LikeFormat_10()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,&Id=10");

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
        public void LikeFormat_11()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,T*&Id=10");

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
