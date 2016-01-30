using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GetRangeBinarySearch;
using System.Diagnostics;
using System.Linq;
//using static GetRangeBinarySearch.GetRangeBinarySearchExtension;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public partial class GetRangeTest
    {
        [TestMethod]
        public void TestMethod1()
        {

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
                TestObjects.intValues.GetRangeBinarySearch(-5, -6);
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
            IList<int> range = TestObjects.intValues.GetRangeBinarySearch(12, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = TestObjects.intValues.GetRangeBinarySearch(12, 13);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_LowerThanAll()
        {
            IList<int> range;
            range = TestObjects.intValues.GetRangeBinarySearch(-6, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = TestObjects.intValues.GetRangeBinarySearch(-7, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_EqualFromTo()
        {
            IList<int> range;
            range = TestObjects.intValues.GetRangeBinarySearch(-5, -5);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5 }));

            range = TestObjects.intValues.GetRangeBinarySearch(-2, -2);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -2 }));

            range = TestObjects.intValues.GetRangeBinarySearch(0, 0);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 0, 0 }));

            range = TestObjects.intValues.GetRangeBinarySearch(3, 3);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 3 }));

            range = TestObjects.intValues.GetRangeBinarySearch(4, 4);
            Assert.IsNotNull(range);
            Assert.AreEqual(4, range.Count);

            range = TestObjects.intValues.GetRangeBinarySearch(11, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
        }

        [TestMethod]
        public void GetRangeSlow_Test()
        {
            List<int> range;
            range = GetRangeSlow(TestObjects.intValues, -6, 3).ToList();
            Assert.IsNotNull(range);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5, -2, 0, 0, 1, 2, 3 }));
        }

        [TestMethod]
        public void GetRange_Full()
        {
            IList<int> range;
            range = TestObjects.intValues.GetRangeBinarySearch(-5, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(TestObjects.intValues.Count, range.Count);

            range = TestObjects.intValues.GetRangeBinarySearch(-6, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(TestObjects.intValues.Count, range.Count);
        }

        [TestMethod]
        public void GetRange_MassTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    var range = TestObjects.intValues.GetRangeBinarySearch(from, to);
                    var expectedRange = GetRangeSlow(TestObjects.intValues, from, to);
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
                    var range = TestObjects.intValues.GetRangeEnumerationBinarySearch(from, to);
                    var expectedRange = GetRangeSlow(TestObjects.intValues, from, to);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        [TestMethod]
        public void Selector_RangeTest()
        {
            DateTime low = TestObjects.flights.First().DepartureTime.AddDays(-1).Date;
            DateTime high = TestObjects.flights.Last().DepartureTime.AddDays(1).Date;
            int length = (high - low).Days;
            for (int from = 0; from <= length; from++)
                for (int to = from; to <= length; to++)
                {
                    DateTime dtFrom = low.AddDays(from);
                    DateTime dtTo = low.AddDays(to);
                    var range = TestObjects.flights.GetRangeEnumerationBinarySearch(f => f.DepartureTime, dtFrom, dtTo);
                    var expectedRange = GetRangeSlow(TestObjects.flights, f => f.DepartureTime, dtFrom, dtTo);
                    Assert.IsTrue(expectedRange.SequenceEqual(range));
                }
        }

        private IEnumerable<T> GetRangeSlow<T>(IEnumerable<T> source, T from, T to, Comparer<T> comparer = null)
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<T>.Default;
            return source.Where(e => comparer.Compare(from, e) < 1 && comparer.Compare(to, e) > -1);
        }

        private IEnumerable<T> GetRangeSlow<T, TSelected>(IEnumerable<T> source, Func<T, TSelected> selector, TSelected from, TSelected to, Comparer<TSelected> comparer = null)
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<TSelected>.Default;
            return source.
                Where(e =>
                {
                    TSelected selected = selector(e);
                    return comparer.Compare(from, selected) < 1 && comparer.Compare(to, selected) > -1;
                });
        }
    }
}
