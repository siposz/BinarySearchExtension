using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearch
{
    public static class GetRangeBinarySearchExtension
    {
        public static T[] GetRangeBinarySearch<T>(this IList<T> sourceList, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceList, from, to, comparer);
            return GetRangeFromList(sourceList, range);
        }

        public static IEnumerable<T> GetRangeEnumerationBinarySearch<T>(this IList<T> sourceList, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceList, from, to, comparer);
            return GetRangeFromEnumeration(sourceList, range);
        }

        private static RangeIndex GetRangeIndex<T>(IList<T> source, T from, T to, IComparer<T> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("RangeBinarySearch source collection is null");

            if (source.Count == 0)
                return RangeIndex.CreateEmpty();

            //Get the default comparer if null
            if (comparer == null)
                comparer = comparer ?? Comparer<T>.Default;

            if (comparer.Compare(from, to) > 0)
                throw new ArgumentException("from should be smaller or equal than to");

            int fromIndex = GetFromIndex(source, from, comparer);

            //All element of the source is smaller then from => return empty
            if (fromIndex == source.Count)
                return RangeIndex.CreateEmpty();

            int toIndex = GetToIndex(source, to, fromIndex, comparer);

            //All element of the source is larger then to => return empty
            if (toIndex < 0)
                return RangeIndex.CreateEmpty();

            return new RangeIndex(fromIndex, toIndex);
        }

        private static T[] GetRangeFromList<T>(IList<T> sourceList, RangeIndex range)
        {
            T[] destinationArray = new T[range.Length];
            for (int i = 0; i < range.Length; i++)
                destinationArray[i] = sourceList[range.FromIndex + i];

            return destinationArray;
        }

        private static IEnumerable<T> GetRangeFromEnumeration<T>(IEnumerable<T> sourceList, RangeIndex range)
        {
            return sourceList.Skip(range.FromIndex).Take(range.Length);
        }

        private static int GetToIndex<T>(IList<T> source, T to, int fromIndex, IComparer<T> comparer)
        {
            int toIndex = source.BinarySearch(fromIndex, source.Count - fromIndex, to, comparer);
            if (toIndex < 0)
            {
                //This is the last index where the element is smaller than to
                toIndex = ~toIndex;
                toIndex--;
            }
            else
                //search for the last matching element
                while (toIndex < source.Count - 1 && comparer.Compare(source[toIndex + 1], to) == 0)
                    toIndex++;
            return toIndex;
        }

        private static int GetFromIndex<T>(IList<T> source, T from, IComparer<T> comparer)
        {
            int fromIndex = source.BinarySearch(0, source.Count, from, comparer);
            if (fromIndex < 0)
                //The first index where the element is larger than from
                fromIndex = ~fromIndex;
            else
                //search for the first matching element
                while (fromIndex > 0 && comparer.Compare(source[fromIndex - 1], from) == 0)
                    fromIndex--;
            return fromIndex;
        }
    }
}
