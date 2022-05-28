using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tense.Rql.UnitTests
{
    [TestClass]
    public class NumericFormatTests
    {
        #region Byte Formats
        [TestMethod]
        public void NumericFormat_Byte_01()
        {
            try
            {
                var node = RqlNode.Parse("Id=1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = 1;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
         
        [TestMethod]
        public void NumericFormat_Byte_02()
        {
            try
            {
                var node = RqlNode.Parse("Id=byte:1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(byte));
                var expectedValue = (byte) 1;
                
                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_03()
        {
            try
            {
                var node = RqlNode.Parse("Id=uint8:1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(byte));
                var expectedValue = (byte)1;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_04()
        {
            try
            {
                var node = RqlNode.Parse("Id=sbyte:12");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(sbyte));
                var expectedValue = (sbyte)12;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_05()
        {
            try
            {
                var node = RqlNode.Parse("Id=int8:33");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(sbyte));
                var expectedValue = (sbyte)33;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_06()
        {
            try
            {
                var node = RqlNode.Parse("Id=int8:-104");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(sbyte));
                var expectedValue = (sbyte)-104;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_07()
        {
            try
            {
                var node = RqlNode.Parse("Id=int8:+32");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(sbyte));
                var expectedValue = (sbyte)32;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_08()
        {
            try
            {
                var node = RqlNode.Parse("Id=int8:%2b32");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(sbyte));
                var expectedValue = (sbyte)32;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_09()
        {
            try
            {
                var node = RqlNode.Parse("Id=0x01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(byte));
                var expectedValue = (byte)1;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_10()
        {
            try
            {
                var node = RqlNode.Parse("Id=0xe3");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(byte));
                var expectedValue = (byte)0xe3;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_11()
        {
            try
            {
                var node = RqlNode.Parse("Id=true");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(bool));
                var expectedValue = true;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_12()
        {
            try
            {
                var node = RqlNode.Parse("Id=bool:true");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(bool));
                var expectedValue = true;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Byte_13()
        {
            try
            {
                var node = RqlNode.Parse("Id=bool:false");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(bool));
                var expectedValue = false;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region short Formats
        [TestMethod]
        public void NumericFormat_Short_01()
        {
            try
            {
                var node = RqlNode.Parse("Id=short:1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(short));
                var expectedValue = (short) 1;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Short_02()
        {
            try
            {
                var node = RqlNode.Parse("Id=int16:1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(short));
                var expectedValue = (short)1;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Short_03()
        {
            try
            {
                var node = RqlNode.Parse("Id=0x0001");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(ushort));
                var expectedValue = (ushort)1;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Short_04()
        {
            try
            {
                var node = RqlNode.Parse("Id=ushort:27");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(ushort));
                var expectedValue = (ushort)27;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Short_05()
        {
            try
            {
                var node = RqlNode.Parse("Id=uint16:88");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(ushort));
                var expectedValue = (ushort)88;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region int formats
        [TestMethod]
        public void NumericFormat_Int_01()
        {
            try
            {
                var node = RqlNode.Parse("Id=5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = (int)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_02()
        {
            try
            {
                var node = RqlNode.Parse("Id=int:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = (int)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_03()
        {
            try
            {
                var node = RqlNode.Parse("Id=integer:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = (int)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_04()
        {
            try
            {
                var node = RqlNode.Parse("Id=int32:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = (int)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_05()
        {
            try
            {
                var node = RqlNode.Parse("Id=INT:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = (int)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_06()
        {
            try
            {
                var node = RqlNode.Parse("Id=INTEGER:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = (int)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_07()
        {
            try
            {
                var node = RqlNode.Parse("Id=INT32:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(int));
                var expectedValue = (int)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_08()
        {
            try
            {
                var node = RqlNode.Parse("Id=5347u");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_09()
        {
            try
            {
                var node = RqlNode.Parse("Id=uint:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_10()
        {
            try
            {
                var node = RqlNode.Parse("Id=UINT:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_11()
        {
            try
            {
                var node = RqlNode.Parse("Id=uint32:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_12()
        {
            try
            {
                var node = RqlNode.Parse("Id=UINT32:5347");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_13()
        {
            try
            {
                var node = RqlNode.Parse("Id=5347U");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_14()
        {
            try
            {
                var node = RqlNode.Parse("Id=UINT32:5347U");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void NumericFormat_Int_15()
        {
            try
            {
                var node = RqlNode.Parse("Id=uint32:5347u");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node[0], typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));

                var actualValue = node[1];
                Assert.IsInstanceOfType(actualValue, typeof(uint));
                var expectedValue = (uint)5347;

                Assert.AreEqual(actualValue, expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion
    }
}
