using System.Collections.Generic;

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
