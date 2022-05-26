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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<int>(1), typeof(int));

                var expectedValue = 1;
                var actualValue = node.NonNullValue<int>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<byte>(1), typeof(byte));

                var expectedValue = (byte)1;
                var actualValue = node.NonNullValue<byte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<byte>(1), typeof(byte));

                var expectedValue = (byte)1;
                var actualValue = node.NonNullValue<byte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<sbyte>(1), typeof(sbyte));

                var expectedValue = (sbyte)12;
                var actualValue = node.NonNullValue<byte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<sbyte>(1), typeof(sbyte));

                var expectedValue = (sbyte)33;
                var actualValue = node.NonNullValue<sbyte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<sbyte>(1), typeof(sbyte));

                var expectedValue = (sbyte)-104;
                var actualValue = node.NonNullValue<sbyte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<sbyte>(1), typeof(sbyte));

                var expectedValue = (sbyte)32;
                var actualValue = node.NonNullValue<sbyte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<sbyte>(1), typeof(sbyte));

                var expectedValue = (sbyte)32;
                var actualValue = node.NonNullValue<sbyte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<byte>(1), typeof(byte));

                var expectedValue = (byte)0x01;
                var actualValue = node.NonNullValue<byte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<byte>(1), typeof(byte));

                var expectedValue = (byte)0xe3;
                var actualValue = node.NonNullValue<byte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<byte>(1), typeof(byte));

                var expectedValue = (byte)1;
                var actualValue = node.NonNullValue<byte>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<bool>(1), typeof(bool));

                var expectedValue = (bool)true;
                var actualValue = node.NonNullValue<bool>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<bool>(1), typeof(bool));

                var expectedValue = (bool)false;
                var actualValue = node.NonNullValue<bool>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<short>(1), typeof(short));

                var expectedValue = 1;
                var actualValue = node.NonNullValue<short>(1);

                Assert.IsTrue(actualValue == expectedValue);
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
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("Id"));
                Assert.IsInstanceOfType(node.NonNullValue<short>(1), typeof(short));

                var expectedValue = 1;
                var actualValue = node.NonNullValue<short>(1);

                Assert.IsTrue(actualValue == expectedValue);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion
    }
}
