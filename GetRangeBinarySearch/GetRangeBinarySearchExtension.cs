using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearch
{
    public static class GetRangeBinarySearchExtension
    {
        public static List<T> GetRangeBinarySearch<T>(this List<T> source, T from, T to, Comparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(source, from, to, comparer);
            if (range.IsEmpty)
                return new List<T>();

            return source.GetRange(range.FromIndex, range.ToIndex - range.FromIndex + 1);
        }

        private static RangeIndex GetRangeIndex<T>(List<T> source, T from, T to, Comparer<T> comparer = null)
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<T>.Default;

            if (source.Count == 0)
                return RangeIndex.CreateEmpty();

            if (comparer.Compare(from, to) > 0)
                throw new ArgumentException("from should be smaller or equal than to");

            int fromIndex = source.BinarySearch(from, comparer);

            if (fromIndex < 0)
                //The first index where the element is larger than from
                fromIndex = ~fromIndex;
            else
                //search for the first matching element
                while (fromIndex > 0 && comparer.Compare(source[fromIndex - 1], from) == 0)
                    fromIndex--;

            //All element of the source is smaller then from => return empty
            if (fromIndex == source.Count)
                return RangeIndex.CreateEmpty();

            int toIndex = source.BinarySearch(to, comparer);

            if (toIndex < 0)
            {
                //This is the last index where the element is smaller than to
                toIndex = ~toIndex;
                toIndex--;
            }
            else
                //search for the last matching element
                while (toIndex < source.Count -1 && comparer.Compare(source[toIndex + 1], to) == 0)
                    toIndex++;

            //All element of the source is larger then to => return empty
            if (toIndex < 0)
                return RangeIndex.CreateEmpty();

            return new RangeIndex(fromIndex, toIndex);
        }

        private class RangeIndex
        {
            public int FromIndex { get; private set; }
            public int ToIndex { get; private set; }

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
}
