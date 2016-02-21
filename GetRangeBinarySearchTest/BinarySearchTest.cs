using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using BinarySearchExtension;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class BinarySearchTest
    {
        [TestMethod]
        public void IList_BinarySearch_Exceptions()
        {
            List<int> sourceList = null;
            AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(0); });
            AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(0, 1, 1); });

            sourceList = new List<int>() { 1, 2, 3 };
            //AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(null, 0); });
            AssertExpcetion<ArgumentOutOfRangeException>(() => { (sourceList as IList<int>).BinarySearchIList(-1, 1, 1); });
            AssertExpcetion<ArgumentException>(() => { (sourceList as IList<int>).BinarySearchIList(1, 100, 1); });
        }

        public void AssertExpcetion<T>(Action action)
            where T : Exception
        {
            try
            {
                action();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is T))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void IList_BinarySearchTest()
        {
            for (int i = -7; i < 13; i++)
            {
                int i1 = TestObjects.IntValuesList.BinarySearch(i);
                int i2 = ((IList<int>)TestObjects.IntValuesList).BinarySearchIList(i);
                Assert.AreEqual(i1, i2);
            }
        }
    }
}
