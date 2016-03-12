using System;
using System.Collections.Generic;

namespace GetRangeBinarySearchTest
{
    internal class DateComparer : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}
