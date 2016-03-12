using BinarySearchExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class PerformanceTest
    {
        #region GetRange
        //[TestMethod]
        public void Performance_GetRangeBinary_RareValues()
        {
            //Measure performance, case: little number of equal values in source
            int minvalue = -1000;
            int maxValue = 1000;
            int number = 100;
            for (int mulitplier = 1; mulitplier <= 1000; mulitplier *= 10)
                TestGetRangeRun(minvalue * mulitplier, maxValue * mulitplier, number * mulitplier, 10000);
        }

        //[TestMethod]
        public void Performance_GetRangeBinary_AllEquals()
        {
            //Measure performance, case: all number equals
            int minvalue = 1;
            int maxValue = 2;
            int number = 1000;
            for (int mulitplier = 1; mulitplier <= 1000; mulitplier *= 10)
                TestGetRangeRun(minvalue, maxValue, number * mulitplier, 100);
        }

        private void TestGetRangeRun(int minValue, int maxValue, int count, int getRangeNumber)
        {
            Random rnd = new Random();
            int[] elements = new int[count];
            for (int i = 0; i < count; i++)
                elements[i] = rnd.Next(minValue, maxValue);
            Array.Sort(elements);
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < getRangeNumber; i++)
            {
                int from = rnd.Next(minValue, maxValue);
                int to = from + 1000;
                elements.GetRangeBinarySearch(from, to);
            }
            sw.Stop();
            Trace.WriteLine("GetRangeBinarySearch: " + sw.ElapsedMilliseconds.ToString());
            sw.Restart();
            for (int i = 0; i < getRangeNumber; i++)
            {
                int from = rnd.Next(minValue, maxValue);
                int to = from + 1000;
                TestObjectsAndHelpers.GetRangeSlow(elements, from, to).ToArray();
            }
            sw.Stop();
            Trace.WriteLine("GetRangeSlow: " + sw.ElapsedMilliseconds.ToString());
        }

        #endregion

        #region FindOne

        //[TestMethod]
        public void FindOnePerformanceTest_LongList()
        {
            FindOne_TestRun(365, 10000, 500);
        }


        //[TestMethod]
        public void FindOnePerformanceTest_ShortList()
        {
            FindOne_TestRun(1, 100000, 2);
        }

        public void FindOne_TestRun(int flightDays, int searchCount, int testDays)
        {
            Random rnd = new Random();
            Flight[] elements = new Flight[flightDays * 12];
            DateComparer comparer = new DateComparer();
            DateTime tomorrow = DateTime.UtcNow.Date.AddDays(1);
            for (int i = 0; i < flightDays * 12; i++)
                elements[i] = new Flight() { DepartureStation = "X", ArrivalStation = "Y", DepartureTime = tomorrow.AddHours(2 * i) };
            elements = elements.OrderBy(f => f.DepartureTime).ToArray();

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < searchCount; i++)
            {
                int dayNumber = rnd.Next(0, testDays);
                DateTime day = tomorrow.AddDays(dayNumber);
                Flight flight = elements.FindOneOrDefaultBinarySearch(f => f.DepartureTime, day, comparer);
                Assert.AreEqual(flight != null, dayNumber < flightDays);
            }
            sw.Stop();
            Trace.WriteLine("FindOneOrDefault: " + sw.ElapsedMilliseconds.ToString());
            sw.Restart();
            for (int i = 0; i < searchCount; i++)
            {
                int dayNumber = rnd.Next(0, testDays);
                DateTime day = tomorrow.AddDays(dayNumber);
                Flight flight = elements.FirstOrDefault(f => f.DepartureTime.Date == day);
                Assert.AreEqual(flight != null, dayNumber < flightDays);
            }
            sw.Stop();
            Trace.WriteLine("FirstOrDefault: " + sw.ElapsedMilliseconds.ToString());
        }

        #endregion
    }
}
