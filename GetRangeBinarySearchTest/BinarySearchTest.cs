using BinarySearchExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class BinarySearchTest
    {
        [TestMethod]
        public void IList_BinarySearch_Exceptions()
        {
            List<int> sourceList = null;
            TestObjectsAndHelpers.AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(0); });
            TestObjectsAndHelpers.AssertExpcetion<ArgumentNullException>(() => { (sourceList as IList<int>).BinarySearchIList(0, 1, 1); });

            sourceList = new List<int>() { 1, 2, 3 };
            TestObjectsAndHelpers.AssertExpcetion<ArgumentOutOfRangeException>(() => { (sourceList as IList<int>).BinarySearchIList(-1, 1, 1); });
            TestObjectsAndHelpers.AssertExpcetion<ArgumentOutOfRangeException>(() => { (sourceList as IList<int>).BinarySearchIList(0, -1, 1); });
            TestObjectsAndHelpers.AssertExpcetion<ArgumentException>(() => { (sourceList as IList<int>).BinarySearchIList(1, 100, 1); });

            TestObjectsAndHelpers.AssertExpcetion<InvalidOperationException>(() => { (sourceList as IList<int>).BinarySearchIList(0, new WrongIntComparer()); });
        }

        [TestMethod]
        public void IList_BinarySearchTest()
        {
            for (int i = -7; i < 13; i++)
            {
                int i1 = TestObjectsAndHelpers.IntValuesList.BinarySearch(i);
                int i2 = ((IList<int>)TestObjectsAndHelpers.IntValuesList).BinarySearchIList(i);
                Assert.AreEqual(i1, i2);
            }
        }
    }
}
