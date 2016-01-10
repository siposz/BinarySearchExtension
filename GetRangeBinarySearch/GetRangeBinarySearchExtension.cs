using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearch
{
    public static class GetRangeBinarySearchExtension
    {
        public static List<T> GetRangeBinarySearch<T>(this List<T> sourceList, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceList, from, to, (list, value, valueComparer) => { return list.BinarySearch(value, comparer); }, comparer);
            if (range.IsEmpty)
                return new List<T>();

            return sourceList.GetRange(range.FromIndex, range.ToIndex - range.FromIndex + 1);
        }

        public static T[] GetRangeBinarySearch<T>(this T[] sourceArray, T from, T to, IComparer<T> comparer = null)
        {
            RangeIndex range = GetRangeIndex(sourceArray, from, to, (array, value, valueComparer) => { return Array.BinarySearch(array, value, comparer); }, comparer);
            if (range.IsEmpty)
                return new T[0];
            T[] destinationArray = new T[range.ToIndex - range.FromIndex + 1];
            Array.Copy(sourceArray, range.FromIndex, destinationArray, 0, range.ToIndex - range.FromIndex + 1);
            return destinationArray; 
        }

        private static RangeIndex GetRangeIndex<T, TList>(TList source, T from, T to, Func<TList, T, IComparer<T>, int> BinarySearch, IComparer<T> comparer = null)
            where TList : IList<T>
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<T>.Default;

            if (source.Count == 0)
                return RangeIndex.CreateEmpty();

            if (comparer.Compare(from, to) > 0)
                throw new ArgumentException("from should be smaller or equal than to");

            int fromIndex = GetFromIndex(source, from, comparer, BinarySearch);

            //All element of the source is smaller then from => return empty
            if (fromIndex == source.Count)
                return RangeIndex.CreateEmpty();

            int toIndex = GetToIndex(source, to, comparer, BinarySearch);

            //All element of the source is larger then to => return empty
            if (toIndex < 0)
                return RangeIndex.CreateEmpty();

            return new RangeIndex(fromIndex, toIndex);
        }

        private static int GetToIndex<T, TList>(TList source, T to, IComparer<T> comparer, Func<TList, T, IComparer<T>, int> BinarySearch)
              where TList : IList<T>
        {
            int toIndex = BinarySearch(source, to, comparer);
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

        private static int GetFromIndex<T, TList>(TList source, T from, IComparer<T> comparer, Func<TList, T, IComparer<T>, int> BinarySearch)
            where TList : IList<T>
        {
            int fromIndex = BinarySearch(source, from, comparer);
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
