using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BinarySearchExtension;
using System.Diagnostics;
using System.Linq;
using System.Threading;
//using static GetRangeBinarySearch.GetRangeBinarySearchExtension;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public partial class GetRangeTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");
                TestObjects.IntValuesList.BinarySearch(0, 10000, 1, null);
            }
            catch (Exception ex)
            {

               
            }
        }

        [TestInitialize]
        public void Initalize()
        {
        }

        [TestMethod]
        public void GetRange_ArgumentException()
        {
            try
            {
                TestObjects.IntValuesList.GetRangeBinarySearch(-5, -6);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetRange_ArgumentNullException()
        {
            try
            {
                List<int> nullList = null;
                nullList.GetRangeBinarySearch(-5, -6);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetRange_LargherThanAll()
        {
            IList<int> range = TestObjects.IntValuesList.GetRangeBinarySearch(12, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = TestObjects.IntValuesList.GetRangeBinarySearch(12, 13);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_LowerThanAll()
        {
            IList<int> range;
            range = TestObjects.IntValuesList.GetRangeBinarySearch(-6, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = TestObjects.IntValuesList.GetRangeBinarySearch(-7, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_EqualFromTo()
        {
            IList<int> range;
            range = TestObjects.IntValuesList.GetRangeBinarySearch(-5, -5);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5 }));

            range = TestObjects.IntValuesList.GetRangeBinarySearch(-2, -2);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -2 }));

            range = TestObjects.IntValuesList.GetRangeBinarySearch(0, 0);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 0, 0 }));

            range = TestObjects.IntValuesList.GetRangeBinarySearch(3, 3);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 3 }));

            range = TestObjects.IntValuesList.GetRangeBinarySearch(4, 4);
            Assert.IsNotNull(range);
            Assert.AreEqual(4, range.Count);

            range = TestObjects.IntValuesList.GetRangeBinarySearch(11, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
        }

        [TestMethod]
        public void GetRangeSlow_Test()
        {
            List<int> range;
            range = TestObjects.GetRangeSlow(TestObjects.IntValuesList, -6, 3).ToList();
            Assert.IsNotNull(range);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5, -2, 0, 0, 1, 2, 3 }));
        }

        [TestMethod]
        public void GetRange_Full()
        {
            IList<int> range;
            range = TestObjects.IntValuesList.GetRangeBinarySearch(-5, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(TestObjects.IntValuesList.Count, range.Count);

            range = TestObjects.IntValuesList.GetRangeBinarySearch(-6, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(TestObjects.IntValuesList.Count, range.Count);
        }

        [TestMethod]
        public void GetRange_MassTest_List()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = TestObjects.IntValuesList.GetRangeBinarySearch(from, to);
                    var expectedRange = TestObjects.GetRangeSlow(TestObjects.IntValuesList, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void GetRange_MassTest_Array()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = TestObjects.IntValuesArray.GetRangeBinarySearch(from, to);
                    var expectedRange = TestObjects.GetRangeSlow(TestObjects.IntValuesList, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void GetRangeEnumeration_List_MassTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = TestObjects.IntValuesList.GetRangeEnumerationBinarySearch(from, to);
                    var expectedRange = TestObjects.GetRangeSlow(TestObjects.IntValuesList, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void Selector_GetRangeEnumerationTest()
        {
            DateTime low = TestObjects.Flights.First().DepartureTime.AddDays(-1).Date;
            DateTime high = TestObjects.Flights.Last().DepartureTime.AddDays(1).Date;
            int length = (high - low).Days;
            for (int from = 0; from <= length; from++)
                for (int to = from; to <= length; to++)
                {
                    DateTime dtFrom = low.AddDays(from);
                    DateTime dtTo = low.AddDays(to);
                    var range = TestObjects.Flights.GetRangeEnumerationBinarySearch(f => f.DepartureTime, dtFrom, dtTo);
                    var expectedRange = TestObjects.GetRangeSlow(TestObjects.Flights, f => f.DepartureTime, dtFrom, dtTo);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                }
        }
    }
}
