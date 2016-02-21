using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchExtension
{
    public static class IListBinarySearchExtension
    {

        const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";
        const string Argument_InvalidOffLen = "Offset and length were out of bounds for the list or count is greater than the number of elements from index to the end of the source collection.";

        //public static int BinarySearchIList<TSource, TSelected>(this IList<TSource> sourceList, Func<TSource, TSelected> selector, TSelected value, IComparer<TSelected> comparer = null)
        //{
        //    return new SelectWrapper<TSource, TSelected>(sourceList, selector).BinarySearchIList(value, comparer);
        //}

        public static int BinarySearchIList<T>(this IList<T> sourceList, T value, IComparer<T> comparer = null)
        {
            if (sourceList == null)
                throw new ArgumentNullException("sourceList");
            return BinarySearchIList(sourceList, 0, sourceList.Count, value, comparer);
        }

        public static int BinarySearchIList<T>(this IList<T> sourceList, int index, int length, T value, IComparer<T> comparer = null)
        {
            if (sourceList == null)
                throw new ArgumentNullException("sourceList");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", ArgumentOutOfRange_NeedNonNegNum);
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", ArgumentOutOfRange_NeedNonNegNum);
            if (sourceList.Count - (index - 0) < length)
                throw new ArgumentException(Argument_InvalidOffLen);

            //Get the default comparer if null
            if (comparer == null)
                comparer = comparer ?? Comparer<T>.Default;

            int lowIndex = index;
            int hiIndex = index + length - 1;
            while (lowIndex <= hiIndex)
            {
                int i = lowIndex + ((hiIndex - lowIndex) >> 1);
                int order = comparer.Compare(sourceList[i], value);

                if (order == 0)
                    return i;

                if (order < 0)
                    lowIndex = i + 1;
                else
                    hiIndex = i - 1;
            }

            return ~lowIndex;
        }
    }
}
