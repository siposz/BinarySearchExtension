using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearch
{
    internal class RangeIndex
    {
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }
        public int Length { get { return ToIndex - FromIndex + 1; } }
        public bool IsEmpty { get { return ToIndex < FromIndex; } }

        public static RangeIndex CreateEmpty()
        {
            return new RangeIndex(-1, -2);
        }

        public RangeIndex(int fromIndex, int toIndex)
        {
            FromIndex = fromIndex;
            ToIndex = toIndex;
        }
    }
}
