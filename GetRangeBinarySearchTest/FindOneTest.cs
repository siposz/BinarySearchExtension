using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BinarySearchExtension;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class FindOneTest
    {
        [TestMethod]
        public void Selector_BinarySearchTest()
        {
            DateTime oneDayBeforeFirst = TestObjects.Flights.First().DepartureTime.AddDays(-1).Date;
            DateTime lastDay = TestObjects.Flights.Last().DepartureTime.AddDays(1).Date;
            int length = (lastDay - oneDayBeforeFirst).Days;
            for (int i = 0; i < length; i++)
            {
                DateTime day = oneDayBeforeFirst.AddDays(i);
                Flight flightOnDayExpected = TestObjects.Flights.FirstOrDefault(f => f.DepartureTime.Date == day);

                Flight flightOnDay = TestObjects.Flights.FindOneOrDefaultBinarySearch(f => f.DepartureTime, day, new DateComparer());
                if (flightOnDayExpected  == null)
                    Assert.IsNull(flightOnDay);
                else
                    Assert.AreEqual(flightOnDayExpected.DepartureTime.Date, flightOnDay.DepartureTime.Date);
            }
        }
    }
}
