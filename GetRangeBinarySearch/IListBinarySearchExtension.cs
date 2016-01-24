using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearch
{
    public static class IListBinarySearchExtension
    {
        public static int BinarySearch<T, TSelected>(this IList<T> list, Func<T, TSelected> selector, TSelected value, IComparer<TSelected> comparer = null)
        {
            return new SelectWrapper<TSelected, T>(list, selector).BinarySearch(value, comparer);
        }

        public static int BinarySearch<T>(this IList<T> list, T value, IComparer<T> comparer = null)
        {
            return list.BinarySearch(0, list.Count, value, comparer);
        }

        public static int BinarySearch<T>(this IList<T> list, int index, int length, T value, IComparer<T> comparer = null)
        {
            if (list == null)
                return 0;
            //Get the default comparer if null
            if (comparer == null)
                comparer = comparer ?? Comparer<T>.Default;

            int lo = index;
            int hi = index + length - 1;
            while (lo <= hi)
            {
                int i = lo + ((hi - lo) >> 1);
                int order = comparer.Compare(list[i], value);

                if (order == 0)
                    return i;

                if (order < 0)
                    lo = i + 1;
                else
                    hi = i - 1;
            }

            return ~lo;
        }
    }
}
