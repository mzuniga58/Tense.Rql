using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tense.Rql.UnitTests
{
    [TestClass]
    public class DateTimeTests
    {
        #region MMDDYYYY
        [TestMethod]
        public void DateFormat_MMDDYYYY_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_02()
        {
            try
            {
                var node = RqlNode.Parse("ADate=01/01/1960");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=01/01/364");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"364-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=01/01/1960Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_05()
        {
            try
            {
                var node = RqlNode.Parse("ADate=01/01/1960 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_06()
        {
            try
            {
                var node = RqlNode.Parse("ADate=01/01/1960gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=01/01/1960 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_08()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:01/01/1960");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:01/01/364");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"364-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_11()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:01/01/1960Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_12()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:01/01/1960 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_13()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:01/01/1960gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_14()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:01/01/1960 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_15()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_16()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:01/01/1960");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_17()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:01/01/364");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"364-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_18()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:01/01/1960Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_19()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:01/01/1960 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_20()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:01/01/1960gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_21()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:01/01/1960 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region MMDDYYYY_HHMMSS
        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_02()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 0:0:0");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_05()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_06()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_08()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 0:0:0");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_11()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_12()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_13()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_14()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 0:0:0");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_15()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_16()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_17()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYYYY_HHMMSS_18()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region MMDDYYYY_HHMMSSF
        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.0");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_02()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_05()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.00000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_06()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.000000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.000000z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_08()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.00000 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.00000GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.00000 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_11()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.0");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_12()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.00");
                
                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_13()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_14()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_15()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.00000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_16()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.000000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_17()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.000000z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_18()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.00000 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_19()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.00000GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_20()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.00000 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }


        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_21()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.0");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_22()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_23()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_24()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_25()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.00000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_26()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_27()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_28()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.00000 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_29()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.00000GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSF_30()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.00000 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region MMDDYYYY_HHMMSSFZ
        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.000000%2b0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_02()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.000000-0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.000000%2b00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1/1/1960 00:00:00.000000-00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_05()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.000000%2b0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_06()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.000000-0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.000000%2b00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_08()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1/1/1960 00:00:00.000000-00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000%2b0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000-0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_11()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000%2b00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_MMDDYY_HHMMSSFZ_12()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000-00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region YYYYMMDD
        [TestMethod]
        public void DateFormat_YYYYMMDD_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_02()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/01/01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=364/01/01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"364-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/01/01Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_05()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/01/01 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_06()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/01/01gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/01/01 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_08()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/01/01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:364/01/01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"364-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_11()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/01/01Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_12()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/01/01 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_13()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/01/01gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_14()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/01/01 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_15()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_16()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/01/01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_17()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:364/01/01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"364-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_18()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/01/01Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_19()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/01/01 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_20()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/01/01gmt");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_21()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/01/01 GMT");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region YYYYMMDD_HHMMSS
        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1T00:00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1T00:00:00z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1T00:00:00 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1T00:00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1T00:00:00z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1T00:00:00 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_13()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1T00:00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_15()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1T00:00:00z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSS_16()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1T00:00:00 Z");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region YYYYMMDD_HHMMSSFZ
        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1T00:00:00.000000%2b0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_02()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1T00:00:00.000000-0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1T00:00:00.000000%2b00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960/1/1T00:00:00.000000-00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_05()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1T00:00:00.000000%2b0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_06()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1T00:00:00.000000-0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1T00:00:00.000000%2b00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_08()
        {
            try
            {
                var node = RqlNode.Parse("ADate=DateTime:1960/1/1T00:00:00.000000-00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1T00:00:00.000000%2b0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1T00:00:00.000000-0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_11()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1T00:00:00.000000%2b00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_YYYYMMDD_HHMMSSFZ_12()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960/1/1T00:00:00.000000-00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion

        #region DATE_STATUS
        [TestMethod]
        public void DateFormat_DS_01()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_02()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_03()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_04()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1/1/1960 00:00:00.000000-0000&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_05()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960-01-01&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_06()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960-01-01T00:00:00&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_07()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960-01-01T00:00:00-0000&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_08()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960-01-01T00:00:00.000000-0000&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_09()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960-01-01-0000&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat_DS_10()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960-01-01%2b0000&status=ACTIVE");

                Assert.IsNotNull(node);

                Assert.IsTrue(node.Operation == RqlOperation.AND);
                Assert.IsTrue(node.Count == 2);

                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(1), typeof(RqlNode));

                var node1 = node.NonNullValue<RqlNode>(0);

                Assert.IsTrue(node1.Operation == RqlOperation.EQ);
                Assert.IsTrue(node1.Count == 2);
                Assert.IsInstanceOfType(node1.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node1.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node1.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node1.NonNullValue<DateTime>(1).Kind == DateTimeKind.Utc);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000+00:00").LocalDateTime;
                var actualDate = node1.NonNullValue<DateTime>(1).ToLocalTime();

                Assert.IsTrue(actualDate == expectedDate);

                var node2 = node.NonNullValue<RqlNode>(1);

                Assert.IsTrue(node2.Operation == RqlOperation.EQ);
                Assert.IsTrue(node2.Count == 2);
                Assert.IsInstanceOfType(node2.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node2.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("status"));
                Assert.IsInstanceOfType(node2.NonNullValue<string>(1), typeof(string));
                Assert.IsTrue(node2.NonNullValue<string>(1).Equals("ACTIVE"));
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }
        #endregion
    }
}