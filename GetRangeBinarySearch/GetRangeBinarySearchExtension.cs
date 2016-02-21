using System;
using System.Collections.Generic;
using System.Linq;

namespace BinarySearchExtension
{
    public static class GetRangeBinarySearchExtension
    {
        public static TSource[] GetRangeBinarySearch<TSource, TSelected>(this IList<TSource> sourceList, Func<TSource, TSelected> selector, TSelected from, TSelected to, IComparer<TSelected> comparer = null)
        {
            RangeIndex range = GetRangeIndex(new SelectWrapper<TSource, TSelected>(sourceList, selector), from, to, comparer);
            return GetRangeFromList(sourceList, range);
        }

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

        public static IEnumerable<TSource> GetRangeEnumerationBinarySearch<TSource, TSelected>(this IList<TSource> sourceList, Func<TSource, TSelected> selector, TSelected from, TSelected to, IComparer<TSelected> comparer = null)
        {
            RangeIndex range = GetRangeIndex(new SelectWrapper<TSource, TSelected>(sourceList, selector), from, to, comparer);
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

            Func<IList<T>, int, int, T, IComparer<T>, int> binarySearch = GetBinarySearchFunc(source);

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

        private static Func<IList<T>, int, int, T, IComparer<T>, int> GetBinarySearchFunc<T>(IList<T> source)
        {
            if (source is List<T>)
                return ((IList<T> s, int index, int length, T item, IComparer<T> comp) => (s as List<T>).BinarySearch(0, s.Count, item, comp));

            if (source is T[])
                return ((IList<T> s, int index, int length, T item, IComparer<T> comp) => Array.BinarySearch(s as T[], 0, s.Count, item, comp));

            return ((IList<T> s, int index, int length, T item, IComparer<T> comp) => s.BinarySearchIList(0, s.Count, item, comp));
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

        private static int GetToIndex<T>(IList<T> sourceList, T to, int fromIndex, IComparer<T> comparer, Func<IList<T>, int, int, T, IComparer<T>, int> binarySearch)
        {
            int toIndex = binarySearch(sourceList, fromIndex, sourceList.Count - fromIndex, to, comparer);
            if (toIndex < 0)
            {
                //This is the last index where the element is smaller than to
                toIndex = ~toIndex;
                toIndex--;
            }
            else
                //search for the last matching element
                while (toIndex < sourceList.Count - 1 && comparer.Compare(sourceList[toIndex + 1], to) == 0)
                    toIndex++;
            return toIndex;
        }

        private static int GetFromIndex<T>(IList<T> sourceList, T from, IComparer<T> comparer, Func<IList<T>, int, int, T, IComparer<T>, int> binarySearch)
        {
            int fromIndex = binarySearch(sourceList, 0, sourceList.Count, from, comparer);
            if (fromIndex < 0)
                //The first index where the element is larger than from
                fromIndex = ~fromIndex;
            else
                //search for the first matching element
                while (fromIndex > 0 && comparer.Compare(sourceList[fromIndex - 1], from) == 0)
                    fromIndex--;
            return fromIndex;
        }
    }
}
