using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tense.Rql.UnitTests
{
    [TestClass]
    public class LimitFormatTests
    {
        [TestMethod]
        public void LimitFormat_01()
        {
            try
            {
                var node = RqlNode.Parse("limit(1)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.LIMIT);
                Assert.IsTrue(node.Count == 1);
                Assert.IsInstanceOfType(node[0], typeof(ulong));

                Assert.AreEqual(node[0], 1ul);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void LimitFormat_02()
        {
            try
            {
                var node = RqlNode.Parse("limit(1,10)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.LIMIT);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(ulong));
                Assert.AreEqual(node[0], 1ul);
                Assert.IsInstanceOfType(node[1], typeof(ulong));
                Assert.AreEqual(node[1], 10ul);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void LimitFormat_03()
        {
            try
            {
                var node = RqlNode.Parse("limit");

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
        public void LimitFormat_04()
        {
            try
            {
                var node = RqlNode.Parse("limit(");

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
        public void LimitFormat_05()
        {
            try
            {
                var node = RqlNode.Parse("limit(1");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error, expecting VALUE or closing ).");
            }
        }

        [TestMethod]
        public void LimitFormat_06()
        {
            try
            {
                var node = RqlNode.Parse("limit(1,");

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
        public void LimitFormat_07()
        {
            try
            {
                var node = RqlNode.Parse("limit(1,10");

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
        public void LimitFormat_08()
        {
            try
            {
                var node = RqlNode.Parse("limit&Id=10");

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
        public void LimitFormat_09()
        {
            try
            {
                var node = RqlNode.Parse("limit(&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting VALUE.");
            }
        }

        [TestMethod]
        public void LimitFormat_10()
        {
            try
            {
                var node = RqlNode.Parse("limit(1&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting VALUE or closing ).");
            }
        }

        [TestMethod]
        public void LimitFormat_11()
        {
            try
            {
                var node = RqlNode.Parse("limit(1,&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting VALUE.");
            }
        }

        [TestMethod]
        public void LimitFormat_12()
        {
            try
            {
                var node = RqlNode.Parse("limit(1,10&Id=10");

                Assert.Fail("RQL invalid syntax undetected.");
            }
            catch (Exception error)
            {
                if (error.GetType() != typeof(RqlFormatException))
                    Assert.Fail(error.Message);

                Assert.AreEqual(error.Message, "RQL Query syntax error at &, expecting closing ).");
            }
        }
    }
}
