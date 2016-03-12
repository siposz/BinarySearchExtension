using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchExtension
{
    public static class FindOneExtension
    {
        /// <summary>
        /// Find an element in an ordered list which selected value equals with valueToSearch
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="TSelected">The type of the value returned by selector.</typeparam>
        /// <param name="sourceList"></param>
        /// <param name="selector">A transform function to apply to elements during search.</param>
        /// <param name="valueToSearch">The value to search</param>
        /// <param name="comparer">A <see cref="System.Collections.Generic.IComparer{T}" /> to compare values.</param>
        /// <returns>An element in source whose selected value is equals to valueToSearch if exist; default(T) if not</returns>
        public static T FindOneOrDefaultBinarySearch<T, TSelected>(this IList<T> sourceList, Func<T, TSelected> selector, TSelected valueToSearch, IComparer<TSelected> comparer = null)
        {
            int index = new SelectWrapper<T, TSelected>(sourceList, selector).BinarySearchIList(valueToSearch, comparer);
            if (index < 0)
                return default(T);
            else
                return sourceList[index];
        }
    }
}
