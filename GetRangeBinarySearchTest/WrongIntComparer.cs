using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetRangeBinarySearchTest
{
    public class WrongIntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x / 0;
        }
    }
}
