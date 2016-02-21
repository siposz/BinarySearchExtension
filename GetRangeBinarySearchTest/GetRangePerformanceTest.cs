using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using BinarySearchExtension;
using System.Linq;

namespace GetRangeBinarySearchTest
{
    public partial class GetRangeTest
    {
        //[TestMethod]
        public void Performance_GetRangeBinary_RareValues()
        {
            //Measure performance, case: little number of equal values in source
            int minvalue = -1000;
            int maxValue = 1000;
            int number = 100;
            for (int mulitplier = 1; mulitplier <= 1000; mulitplier *= 10)
                TestRun(minvalue * mulitplier, maxValue * mulitplier, number * mulitplier, 10000);
        }

        //[TestMethod]
        public void Performance_GetRangeBinary_AllEquals()
        {
            //Measure performance, case: all number equals
            int minvalue = 1;
            int maxValue = 2;
            int number = 1000;
            for (int mulitplier = 1; mulitplier <= 1000; mulitplier *= 10)
                TestRun(minvalue, maxValue, number * mulitplier, 100);
        }


        private void TestRun(int minValue, int maxValue, int count, int getRangeNumber)
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
                GetRangeSlow(elements, from, to).ToArray();
            }
            sw.Stop();
            Trace.WriteLine("GetRangeSlow: " + sw.ElapsedMilliseconds.ToString());
        }
    }
}
