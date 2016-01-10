using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearch
{
    public static class GetRangeBinarySearchExtension
    {
        private delegate int BinarySearch<T, TList>(TList sourceCollection, T value, int startIndex, IComparer<T> comparer)
           where TList : IList<T>;

        public static List<T> GetRangeBinarySearch<T>(this List<T> sourceList, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceList, from, to, BinarySearchList, comparer);
            if (range.IsEmpty)
                return new List<T>();

            return sourceList.GetRange(range.FromIndex, range.Length);
        }

        public static IEnumerable<T> GetRangeEnumerationBinarySearch<T>(this List<T> sourceList, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceList, from, to, BinarySearchList, comparer);
            if (range.IsEmpty)
                return new List<T>();
            return GetRangeFromEnumeration(sourceList, range);
        }

        public static T[] GetRangeBinarySearch<T>(this T[] sourceArray, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceArray, from, to, BinarySearchArray, comparer);
            if (range.IsEmpty)
                return new T[0];
            T[] destinationArray = new T[range.Length];
            Array.Copy(sourceArray, range.FromIndex, destinationArray, 0, range.Length);
            return destinationArray;
        }

        public static IEnumerable<T> GetRangeEnumerationBinarySearch<T>(this T[] sourceArray, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceArray, from, to, BinarySearchArray, comparer);
            return GetRangeFromEnumeration(sourceArray, range);
        }

        private static RangeIndex GetRangeIndex<T, TList>(TList source, T from, T to, BinarySearch<T, TList> binarySearch, IComparer<T> comparer = null)
            where TList : IList<T>
        {
            if (source == null)
                throw new ArgumentNullException("RangeBinarySearch source collection is null");

            if (source.Count == 0)
                return RangeIndex.CreateEmpty();

            //Get the default comparer if null
            comparer = comparer ?? Comparer<T>.Default;

            if (comparer.Compare(from, to) > 0)
                throw new ArgumentException("from should be smaller or equal than to");

            int fromIndex = GetFromIndex(source, from, comparer, binarySearch);

            //All element of the source is smaller then from => return empty
            if (fromIndex == source.Count)
                return RangeIndex.CreateEmpty();

            int toIndex = GetToIndex(source, to, fromIndex, comparer, binarySearch);

            //All element of the source is larger then to => return empty
            if (toIndex < 0)
                return RangeIndex.CreateEmpty();

            return new RangeIndex(fromIndex, toIndex);
        }

        private static IEnumerable<T> GetRangeFromEnumeration<T>(IEnumerable<T> sourceList, RangeIndex range)
        {
            return sourceList.Skip(range.FromIndex).Take(range.Length);
        }

        private static int BinarySearchList<T>(List<T> sourceCollection, T value, int startIndex, IComparer<T> comparer)
        {
            return sourceCollection.BinarySearch(startIndex, sourceCollection.Count - startIndex, value, comparer);
        }

        private static int BinarySearchArray<T>(T[] sourceCollection, T value, int startIndex, IComparer<T> comparer)
        {
            return Array.BinarySearch(sourceCollection, startIndex, sourceCollection.Length - startIndex, value, comparer);
        }

        private static int GetToIndex<T, TList>(TList source, T to, int fromIndex, IComparer<T> comparer, BinarySearch<T, TList> binarySearch)
              where TList : IList<T>
        {
            int toIndex = binarySearch(source, to, fromIndex, comparer);
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

        private static int GetFromIndex<T, TList>(TList source, T from, IComparer<T> comparer, BinarySearch<T, TList> binarySearch)
            where TList : IList<T>
        {
            int fromIndex = binarySearch(source, from, 0, comparer);
            if (fromIndex < 0)
                //The first index where the element is larger than from
                fromIndex = ~fromIndex;
            else
                //search for the first matching element
                while (fromIndex > 0 && comparer.Compare(source[fromIndex - 1], from) == 0)
                    fromIndex--;
            return fromIndex;
        }

        private class RangeIndex
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
}
