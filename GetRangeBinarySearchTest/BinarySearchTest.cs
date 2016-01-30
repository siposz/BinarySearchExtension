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
        public void IList_BinarySearchTest()
        {
            for (int i = -7; i < 13; i++)
            {
                int i1 = TestObjects.intValues.BinarySearch(i);
                int i2 = ((IList<int>)TestObjects.intValues).BinarySearch(i);
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
                int binarySearchIndex = TestObjects.flights.BinarySearch(f => f.DepartureTime, day, new DateComparer());
                if (expectedIndex < 0)
                    Assert.IsTrue(binarySearchIndex < 0);
                else
                    Assert.AreEqual(TestObjects.flights[expectedIndex].DepartureTime.Date, TestObjects.flights[binarySearchIndex].DepartureTime.Date);
            }
        }
    }
}
