using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using GetRangeBinarySearch;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class BinarySearchTest
    {
        [TestMethod]
        public void IList_BinarySearch_Exceptions()
        {
            List<int> sourceList = null;
            AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(0); });
            AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(0, 1, 1); });

            sourceList = new List<int>() { 1, 2, 3 };
            AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(null, 0); });
            AssertExpcetion<ArgumentOutOfRangeException>(() => { (sourceList as IList<int>).BinarySearchIList(-1, 1, 1); });
            AssertExpcetion<ArgumentException>(() => { (sourceList as IList<int>).BinarySearchIList(1, 100, 1); });
        }

        public void AssertExpcetion<T>(Action action)
            where T : Exception
        {
            try
            {
                action();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is T))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void IList_BinarySearchTest()
        {
            for (int i = -7; i < 13; i++)
            {
                int i1 = TestObjects.intValues.BinarySearch(i);
                int i2 = ((IList<int>)TestObjects.intValues).BinarySearchIList(i);
                Assert.AreEqual(i1, i2);
            }
        }

        [TestMethod]
        public void Selector_BinarySearchTest()
        {
            DateTime oneDayBeforeFirst = TestObjects.flights.First().DepartureTime.AddDays(-1).Date;
            DateTime lastDay = TestObjects.flights.Last().DepartureTime.AddDays(1).Date;
            int length = (lastDay - oneDayBeforeFirst).Days;
            for (int i = 0; i < length; i++)
            {
                DateTime day = oneDayBeforeFirst.AddDays(i);
                Flight flightOnDay = TestObjects.flights.FirstOrDefault(f => f.DepartureTime.Date == day);
                int expectedIndex = TestObjects.flights.IndexOf(flightOnDay);
                int binarySearchIndex = TestObjects.flights.BinarySearchIList(f => f.DepartureTime, day, new DateComparer());
                if (expectedIndex < 0)
                    Assert.IsTrue(binarySearchIndex < 0);
                else
                    Assert.AreEqual(TestObjects.flights[expectedIndex].DepartureTime.Date, TestObjects.flights[binarySearchIndex].DepartureTime.Date);
            }
        }
    }
}
