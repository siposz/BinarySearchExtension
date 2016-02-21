using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchExtension
{
    public static class FindOneExtension
    {
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
