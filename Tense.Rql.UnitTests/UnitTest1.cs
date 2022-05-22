using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tense.Rql.UnitTests
{
    [TestClass]
    public class DateTimeTests
    {
        [TestMethod]
        public void DateFormat01()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat02()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat03()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat04()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat05()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat06()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat07()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat08()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat09()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat10()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat11()
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
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat12()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-1-1");


                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat13()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat14()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat15()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.0");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat16()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat17()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat18()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat19()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.00000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat20()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.000000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat21()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.000000%2b0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat22()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.000000-0000");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat23()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.000000%2b00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat24()
        {
            try
            {
                var node = RqlNode.Parse("ADate=1960-01-01T00:00:00.000000-00:00");

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Operation == RqlOperation.EQ);
                Assert.IsTrue(node.Count == 2);
                Assert.IsInstanceOfType(node.NonNullValue<RqlNode>(0), typeof(RqlNode));
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Operation == RqlOperation.PROPERTY);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).Count == 1);
                Assert.IsTrue(node.NonNullValue<RqlNode>(0).NonNullValue<string>(0).Equals("ADate"));
                Assert.IsInstanceOfType(node.NonNullValue<DateTime>(1), typeof(DateTime));
                Assert.IsTrue(node.NonNullValue<DateTime>(1).Kind == DateTimeKind.Local);

                var expectedDate = DateTimeOffset.Parse($"1960-01-01T00:00:00.00000{TimeZoneInfo.Local.BaseUtcOffset.Hours}:00").LocalDateTime;
                var actualDate = node.NonNullValue<DateTime>(1);

                Assert.IsTrue(actualDate == expectedDate);
            }
            catch (Exception error)
            {
                Assert.Fail(error.Message);
            }
        }

        [TestMethod]
        public void DateFormat25()
        {
            try
            {
                var node = RqlNode.Parse("ADate=utc:1960-01-01");

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
    }
}