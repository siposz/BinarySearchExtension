using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GetRangeBinarySearch;
using System.Diagnostics;
using System.Linq;

namespace GetRangeBinarySearchTest
{
    [TestClass]
    public class GetRangeTest
    {
        //[TestMethod]
        //[Ignore]
        //public void TestMethod1()
        //{
        //    int[] ints = new int[3] { 1, 2, 3 };

        //    Assert.AreEqual(1, Array.BinarySearch(ints, 2));
        //    Assert.AreEqual(2, Array.BinarySearch(ints, 1, ints.Length - 1, 3));
        //}

        public List<int> intValues = new List<int>() { -5, -2, 0, 0, 1, 2, 3, 4, 4, 4, 4, 6, 6, 8, 9, 11, 11 };

        [TestMethod]
        public void GetRange_ArgumentException()
        {
            try
            {
                intValues.GetRangeBinarySearch(-5, -6);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void GetRange_LargherThanAll()
        {
            var range = intValues.GetRangeBinarySearch(12, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = intValues.GetRangeBinarySearch(12, 13);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_LowerThanAll()
        {
            List<int> range;
            range = intValues.GetRangeBinarySearch(-6, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);

            range = intValues.GetRangeBinarySearch(-7, -6);
            Assert.IsNotNull(range);
            Assert.AreEqual(0, range.Count);
        }

        [TestMethod]
        public void GetRange_EqualFromTo()
        {
            List<int> range;
            range = intValues.GetRangeBinarySearch(-5, -5);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5 }));

            range = intValues.GetRangeBinarySearch(-2, -2);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -2 }));

            range = intValues.GetRangeBinarySearch(0, 0);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 0, 0 }));

            range = intValues.GetRangeBinarySearch(3, 3);
            Assert.IsNotNull(range);
            Assert.AreEqual(1, range.Count);
            Assert.IsTrue(range.SequenceEqual(new List<int> { 3 }));

            range = intValues.GetRangeBinarySearch(4, 4);
            Assert.IsNotNull(range);
            Assert.AreEqual(4, range.Count);

            range = intValues.GetRangeBinarySearch(11, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(2, range.Count);
        }

        [TestMethod]
        public void GetRangeSlow()
        {
            List<int> range;
            range = GetRangeSlow(intValues, -6, 3).ToList();
            Assert.IsNotNull(range);
            Assert.IsTrue(range.SequenceEqual(new List<int> { -5, -2, 0, 0, 1, 2, 3 }));


        }

        [TestMethod]
        public void GetRange_Full()
        {
            List<int> range;
            range = intValues.GetRangeBinarySearch(-5, 11);
            Assert.IsNotNull(range);
            Assert.AreEqual(intValues.Count, range.Count);

            range = intValues.GetRangeBinarySearch(-6, 12);
            Assert.IsNotNull(range);
            Assert.AreEqual(intValues.Count, range.Count);
        }



        [TestMethod]
        public void GetRange_MassTest()
        {
            int counter = 0;
            int low = -8;
            int high = 14;
            for (int from = low; from <= high; from++)
                for (int to = from; to <= high; to++)
                {
                    GetRangeSlow(intValues, from, to).SequenceEqual(intValues.GetRangeBinarySearch(from, to));
                    counter++;
                }
            int n = high - low + 1;
            Assert.AreEqual((n * (n + 1)) / 2, counter);
            Trace.WriteLine(counter);
        }

        private IEnumerable<T> GetRangeSlow<T>(IEnumerable<T> source, T from, T to, Comparer<T> comparer = null) 
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<T>.Default;
            return source.Where(e => comparer.Compare(from, e) < 1 && comparer.Compare(to, e) > -1);
        }
    }
}
