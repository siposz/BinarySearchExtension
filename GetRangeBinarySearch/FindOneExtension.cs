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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelected"></typeparam>
        /// <param name="sourceList"></param>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static T FindOneOrDefaultBinarySearch<T, TSelected>(this IList<T> sourceList, Func<T, TSelected> selector, TSelected value, IComparer<TSelected> comparer = null)
        {
            int index = new SelectWrapper<T, TSelected>(sourceList, selector).BinarySearchIList(value, comparer);
            if (index < 0)
                return default(T);
            else
                return sourceList[index];
        }
    }
}
