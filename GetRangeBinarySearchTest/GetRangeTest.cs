using BinarySearchExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public partial class GetRangeTest
    {
        [TestMethod]
        public void GetRange_ArgumentException()
        {
            TestObjectsAndHelpers.AssertExpcetion<ArgumentException>(() => { TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(-5, -6); });
        }

        [TestMethod]
        public void GetRange_ArgumentNullException()
        {
            List<int> nullList = null;
            TestObjectsAndHelpers.AssertExpcetion<ArgumentNullException>(() => { nullList.GetRangeBinarySearch(0, 1); });
        }

        [TestMethod]
        public void GetRange_EmptyList()
        {
            List<int> emptyList = new List<int>();
            int[] empty = emptyList.GetRangeBinarySearch(-5, -6);
            Assert.IsNotNull(empty);
            Assert.AreEqual(empty.Length, 0);

        }

        [TestMethod]
        public void GetRange_LargherThanAll()
        {
            IList<int> range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(12, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(12, 13);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_LowerThanAll()
        {
            IList<int> range;
            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(-6, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(-7, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_EqualFromTo()
        {
            IList<int> range;
            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(-5, -5);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5 }));

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(-2, -2);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -2 }));

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(0, 0);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 0, 0 }));

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(3, 3);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 3 }));

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(4, 4);
            Assert.IsNotNull(range);
            Assert.AreEqual(4, range.Count);

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(11, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
        }

        [TestMethod]
        public void GetRangeSlow_Test()
        {
            List<int> range;
            range = TestObjectsAndHelpers.GetRangeSlow(TestObjectsAndHelpers.IntValuesList, -6, 3).ToList();
            Assert.IsNotNull(range);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5, -2, 0, 0, 1, 2, 3 }));
        }

        [TestMethod]
        public void GetRange_Full()
        {
            IList<int> range;
            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(-5, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(TestObjectsAndHelpers.IntValuesList.Count, range.Count);

            range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(-6, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(TestObjectsAndHelpers.IntValuesList.Count, range.Count);
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
                    var range = TestObjectsAndHelpers.IntValuesList.GetRangeBinarySearch(from, to);
                    var expectedRange = TestObjectsAndHelpers.GetRangeSlow(TestObjectsAndHelpers.IntValuesList, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void GetRangeArrayTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = TestObjectsAndHelpers.IntValuesArray.GetRangeBinarySearch(from, to);
                    var expectedRange = TestObjectsAndHelpers.GetRangeSlow(TestObjectsAndHelpers.IntValuesList, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void Selector_GetRangeArrayTest()
        {
            DateTime low = TestObjectsAndHelpers.Flights.First().DepartureTime.AddDays(-1).Date;
            DateTime high = TestObjectsAndHelpers.Flights.Last().DepartureTime.AddDays(1).Date;
            int length = (high - low).Days;
            for (int from = 0; from <= length; from++)
                for (int to = from; to <= length; to++)
                {
                    DateTime dtFrom = low.AddDays(from);
                    DateTime dtTo = low.AddDays(to);
                    Flight[] range = TestObjectsAndHelpers.Flights.GetRangeBinarySearch(f => f.DepartureTime, dtFrom, dtTo);
                    var expectedRange = TestObjectsAndHelpers.GetRangeSlow(TestObjectsAndHelpers.Flights, f => f.DepartureTime, dtFrom, dtTo);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                }
        }

        [TestMethod]
        public void GetRangeEnumerationTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = TestObjectsAndHelpers.IntValuesList.GetRangeEnumerationBinarySearch(from, to);
                    var expectedRange = TestObjectsAndHelpers.GetRangeSlow(TestObjectsAndHelpers.IntValuesList, from, to);
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
            DateTime low = TestObjectsAndHelpers.Flights.First().DepartureTime.AddDays(-1).Date;
            DateTime high = TestObjectsAndHelpers.Flights.Last().DepartureTime.AddDays(1).Date;
            int length = (high - low).Days;
            for (int from = 0; from <= length; from++)
                for (int to = from; to <= length; to++)
                {
                    DateTime dtFrom = low.AddDays(from);
                    DateTime dtTo = low.AddDays(to);
                    var range = TestObjectsAndHelpers.Flights.GetRangeEnumerationBinarySearch(f => f.DepartureTime, dtFrom, dtTo);
                    var expectedRange = TestObjectsAndHelpers.GetRangeSlow(TestObjectsAndHelpers.Flights, f => f.DepartureTime, dtFrom, dtTo);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                }
        }
    }
}
