using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GetRangeBinarySearch;
using System.Diagnostics;
using System.Linq;
using static GetRangeBinarySearch.GetRangeBinarySearchExtension;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class GetRangeTest
    {
        private class Flight
        {
            public string DepartureStation { get; set; }
            public string ArrivalStation { get; set; }

            public DateTime DepartureTime { get; set; }
        }

        private class DateComparer : IComparer<DateTime>
        {
            public int Compare(DateTime x, DateTime y)
            {
                return x.Date.CompareTo(y.Date);
            }
        }


        List<int> intValues = new List<int>() { -5, -2, 0, 0, 1, 2, 3, 4, 4, 4, 4, 6, 6, 8, 9, 11, 11 };

        int[] intArray;

        List<Flight> flights = new List<Flight>()
        {
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,1,10,30,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,3,10,30,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,3,16,30,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,5,16,30,0,DateTimeKind.Utc) },
        };

        [TestMethod]
        public void TestMethod1()
        {
            
        }

        [TestInitialize]
        public void Initalize()
        {
            intArray = intValues.ToArray();
        }

        [TestMethod]
        public void GetRange_ArgumentException()
        {
            try
            {
                intValues.GetRangeBinarySearch(-5, -6);
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
            var range = intValues.GetRangeBinarySearch(12, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = intValues.GetRangeBinarySearch(12, 13);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_LowerThanAll()
        {
            List<int> range;
            range = intValues.GetRangeBinarySearch(-6, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = intValues.GetRangeBinarySearch(-7, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_EqualFromTo()
        {
            List<int> range;
            range = intValues.GetRangeBinarySearch(-5, -5);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5 }));

            range = intValues.GetRangeBinarySearch(-2, -2);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -2 }));

            range = intValues.GetRangeBinarySearch(0, 0);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 0, 0 }));

            range = intValues.GetRangeBinarySearch(3, 3);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 3 }));

            range = intValues.GetRangeBinarySearch(4, 4);
            Assert.IsNotNull(range);
            Assert.AreEqual(4, range.Count);

            range = intValues.GetRangeBinarySearch(11, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
        }

        [TestMethod]
        public void GetRangeSlow()
        {
            List<int> range;
            range = GetRangeSlow(intValues, -6, 3).ToList();
            Assert.IsNotNull(range);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5, -2, 0, 0, 1, 2, 3 }));
        }

        [TestMethod]
        public void GetRange_Full()
        {
            List<int> range;
            range = intValues.GetRangeBinarySearch(-5, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(intValues.Count, range.Count);

            range = intValues.GetRangeBinarySearch(-6, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(intValues.Count, range.Count);
        }

        [TestMethod]
        public void GetRange_List_MassTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = intValues.GetRangeBinarySearch(from, to);
                    var expectedRange = GetRangeSlow(intValues, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void GetRange_Array_MassTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = intArray.GetRangeBinarySearch(from, to);
                    var expectedRange = GetRangeSlow(intArray, from, to);
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
                    var range = intValues.GetRangeEnumerationBinarySearch(from, to);
                    var expectedRange = GetRangeSlow(intValues, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void GetRangeEnumeration_Array_MassTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = intArray.GetRangeEnumerationBinarySearch(from, to);
                    var expectedRange = GetRangeSlow(intArray, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void IList_BinarySearchTest()
        {
            for (int i = -7; i < 13; i++)
            {
                int i1 = intValues.BinarySearch(i);
                int i2 = ((IList<int>)intValues).BinarySearch(i);
                Assert.AreEqual(i1, i2);
            }
        }

        private IEnumerable<T> GetRangeSlow<T>(IEnumerable<T> source, T from, T to, Comparer<T> comparer = null)
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<T>.Default;
            return source.Where(e => comparer.Compare(from, e) < 1 && comparer.Compare(to, e) > -1);
        }
    }
}
