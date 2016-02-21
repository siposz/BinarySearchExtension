using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchExtension
{
    internal class SelectWrapper<TSource, TSelected> : IList<TSelected>
    {
        private IList<TSource> WrappedList;
        Func<TSource, TSelected> Selector;

        public SelectWrapper(IList<TSource> sourceList, Func<TSource, TSelected> selector)
        {
            if (sourceList == null)
                throw new ArgumentNullException("sourceList");
            if (selector == null)
                throw new ArgumentNullException("selector");
            WrappedList = sourceList;
            Selector = selector;
        }

        public TSelected this[int index]
        {
            get
            {
                return Selector(WrappedList[index]);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                return WrappedList.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public void Add(TSelected item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TSelected item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TSelected[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TSelected> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(TSelected item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, TSelected item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TSelected item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
