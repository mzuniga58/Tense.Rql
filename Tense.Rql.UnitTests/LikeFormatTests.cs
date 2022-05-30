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
                var node = RqlNode.Parse("Like(Lastname,T*,J*)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.LIKE);
                Assert.IsTrue(node.Count == 3);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));

                var propertyNode = node.NonNullValue<RqlNode>(0);
                Assert.AreEqual(propertyNode.Operation, RqlOperation.PROPERTY);
                Assert.AreEqual(propertyNode.NonNullValue<string>(0), "Lastname");

                Assert.IsInstanceOfType(node[1], typeof(string));
                Assert.AreEqual(node.NonNullValue<string>(1), "T*");

                Assert.IsInstanceOfType(node[2], typeof(string));
                Assert.AreEqual(node.NonNullValue<string>(2), "J*");
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void LikeFormat_03()
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
        public void LikeFormat_04()
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
        public void LikeFormat_05()
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
        public void LikeFormat_06()
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
        public void LikeFormat_07()
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
        public void LikeFormat_08()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,T*,");

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
        public void LikeFormat_09()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,T*,J*");

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
        public void LikeFormat_10()
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
        public void LikeFormat_11()
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
        public void LikeFormat_12()
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
        public void LikeFormat_13()
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

        [TestMethod]
        public void LikeFormat_14()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,T*,&Id=10");

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
        public void LikeFormat_15()
        {
            try
            {
                var node = RqlNode.Parse("like(LastName,T*,J*&Id=10");

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
        public void CONTAINSFormat_01()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(Lastname,T*)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.CONTAINS);
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
        public void CONTAINSFormat_02()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(Lastname,T*,J*)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.CONTAINS);
                Assert.IsTrue(node.Count == 3);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));

                var propertyNode = node.NonNullValue<RqlNode>(0);
                Assert.AreEqual(propertyNode.Operation, RqlOperation.PROPERTY);
                Assert.AreEqual(propertyNode.NonNullValue<string>(0), "Lastname");

                Assert.IsInstanceOfType(node[1], typeof(string));
                Assert.AreEqual(node.NonNullValue<string>(1), "T*");

                Assert.IsInstanceOfType(node[2], typeof(string));
                Assert.AreEqual(node.NonNullValue<string>(2), "J*");
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void CONTAINSFormat_03()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS");

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
        public void CONTAINSFormat_04()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(");

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
        public void CONTAINSFormat_05()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName");

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
        public void CONTAINSFormat_06()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,");

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
        public void CONTAINSFormat_07()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,T*");

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
        public void CONTAINSFormat_08()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,T*,");

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
        public void CONTAINSFormat_09()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,T*,J*");

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
        public void CONTAINSFormat_10()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(&Id=10");

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
        public void CONTAINSFormat_11()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName&Id=10");

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
        public void CONTAINSFormat_12()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,&Id=10");

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
        public void CONTAINSFormat_13()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,T*&Id=10");

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
        public void CONTAINSFormat_14()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,T*,&Id=10");

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
        public void CONTAINSFormat_15()
        {
            try
            {
                var node = RqlNode.Parse("CONTAINS(LastName,T*,J*&Id=10");

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
        public void EXCLUDESFormat_01()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(Lastname,T*)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EXCLUDES);
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
        public void EXCLUDESFormat_02()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(Lastname,T*,J*)");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EXCLUDES);
                Assert.IsTrue(node.Count == 3);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));

                var propertyNode = node.NonNullValue<RqlNode>(0);
                Assert.AreEqual(propertyNode.Operation, RqlOperation.PROPERTY);
                Assert.AreEqual(propertyNode.NonNullValue<string>(0), "Lastname");

                Assert.IsInstanceOfType(node[1], typeof(string));
                Assert.AreEqual(node.NonNullValue<string>(1), "T*");

                Assert.IsInstanceOfType(node[2], typeof(string));
                Assert.AreEqual(node.NonNullValue<string>(2), "J*");
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void EXCLUDESFormat_03()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES");

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
        public void EXCLUDESFormat_04()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(");

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
        public void EXCLUDESFormat_05()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName");

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
        public void EXCLUDESFormat_06()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,");

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
        public void EXCLUDESFormat_07()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,T*");

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
        public void EXCLUDESFormat_08()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,T*,");

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
        public void EXCLUDESFormat_09()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,T*,J*");

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
        public void EXCLUDESFormat_10()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(&Id=10");

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
        public void EXCLUDESFormat_11()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName&Id=10");

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
        public void EXCLUDESFormat_12()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,&Id=10");

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
        public void EXCLUDESFormat_13()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,T*&Id=10");

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
        public void EXCLUDESFormat_14()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,T*,&Id=10");

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
        public void EXCLUDESFormat_15()
        {
            try
            {
                var node = RqlNode.Parse("EXCLUDES(LastName,T*,J*&Id=10");

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
