using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearch
{
    public static class FindOneExtension
    {
        public static T FindOne<T, TSelected>(this IList<T> sourceList, Func<T, TSelected> selector, TSelected value, IComparer<T> compare = null)
        {
            return default(T);
        }
    }
}
