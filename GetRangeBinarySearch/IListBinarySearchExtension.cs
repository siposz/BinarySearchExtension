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

        /// <summary>
        ///    Searches a range of elements in the sorted, zero-based indexed System.Collections.Generic.IList`1
        ///    for an element using the specified comparer and returns the zero-based index
        ///    of the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <param name="value">The object to locate. </param>
        /// <param name="comparer">The System.Collections.Generic.IComparer implementation to use when comparing
        ///     elements, or null to use the default comparer.</param>
        /// <returns>  The zero-based index of item in the sorted System.Collections.Generic.IList,
        ///     if item is found; otherwise, a negative number that is the bitwise complement
        ///     of the index of the next element that is larger than item or, if there is no
        ///     larger element, the bitwise complement of System.Collections.Generic.IList.Count.
        ///</returns>
        public static int BinarySearchIList<T>(this IList<T> sourceList, T value, IComparer<T> comparer = null)
        {
            if (sourceList == null)
                throw new ArgumentNullException("sourceList");
            return BinarySearchIList(sourceList, 0, sourceList.Count, value, comparer);
        }

        /// <summary>
        ///    Searches a range of elements in the sorted, zero-based indexed System.Collections.Generic.IList`1
        ///    for an element using the specified comparer and returns the zero-based index
        ///    of the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <param name="index"> The zero-based starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The object to locate. </param>
        /// <param name="comparer">The System.Collections.Generic.IComparer implementation to use when comparing
        ///     elements, or null to use the default comparer.</param>
        /// <returns>  The zero-based index of item in the sorted System.Collections.Generic.IList,
        ///     if item is found; otherwise, a negative number that is the bitwise complement
        ///     of the index of the next element that is larger than item or, if there is no
        ///     larger element, the bitwise complement of System.Collections.Generic.IList.Count.
        ///</returns>
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

            try
            {
                while (lowIndex <= hiIndex)
                {
                    int i = lowIndex + ((hiIndex - lowIndex) >> 1);
                    int order = order = comparer.Compare(sourceList[i], value);

                    if (order == 0)
                        return i;

                    if (order < 0)
                        lowIndex = i + 1;
                    else
                        hiIndex = i - 1;
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("InvalidOperation IComparerFailed", e);
            }

            return ~lowIndex;
        }
    }
}
