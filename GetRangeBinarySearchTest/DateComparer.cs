using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
