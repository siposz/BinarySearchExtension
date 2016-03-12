using BinarySearchExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class FindOneTest
    {
        [TestMethod]
        public void FindOneOrDefaultBinarySearch()
        {
            DateTime oneDayBeforeFirst = TestObjectsAndHelpers.Flights.First().DepartureTime.AddDays(-1).Date;
            DateTime lastDay = TestObjectsAndHelpers.Flights.Last().DepartureTime.AddDays(1).Date;
            int length = (lastDay - oneDayBeforeFirst).Days;
            for (int i = 0; i < length; i++)
            {
                DateTime day = oneDayBeforeFirst.AddDays(i);
                Flight flightOnDayExpected = TestObjectsAndHelpers.Flights.FirstOrDefault(f => f.DepartureTime.Date == day);

                Flight flightOnDay = TestObjectsAndHelpers.Flights.FindOneOrDefaultBinarySearch(f => f.DepartureTime, day, new DateComparer());
                if (flightOnDayExpected  == null)
                    Assert.IsNull(flightOnDay);
                else
                    Assert.AreEqual(flightOnDayExpected.DepartureTime.Date, flightOnDay.DepartureTime.Date);
            }
        }

        [TestMethod]
        public void FindOneOrDefaultArgumentNullException()
        {
            DateTime oneDayBeforeFirst = TestObjectsAndHelpers.Flights.First().DepartureTime.AddDays(-1).Date;
            DateTime lastDay = TestObjectsAndHelpers.Flights.Last().DepartureTime.AddDays(1).Date;
            int length = (lastDay - oneDayBeforeFirst).Days;
            for (int i = 0; i < length; i++)
            {
                DateTime day = oneDayBeforeFirst.AddDays(i);
                Flight flightOnDayExpected = TestObjectsAndHelpers.Flights.FirstOrDefault(f => f.DepartureTime.Date == day);

                Flight flightOnDay = TestObjectsAndHelpers.Flights.FindOneOrDefaultBinarySearch(f => f.DepartureTime, day, new DateComparer());
                if (flightOnDayExpected == null)
                    Assert.IsNull(flightOnDay);
                else
                    Assert.AreEqual(flightOnDayExpected.DepartureTime.Date, flightOnDay.DepartureTime.Date);
            }
        }

        
    }
}
