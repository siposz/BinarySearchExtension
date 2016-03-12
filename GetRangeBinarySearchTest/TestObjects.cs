using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearchTest
{
    internal static class TestObjectsAndHelpers
    {
        internal static List<int> IntValuesList = new List<int>() { -5, -2, 0, 0, 1, 2, 3, 4, 4, 4, 4, 6, 6, 8, 9, 11, 11 };

        internal static int[] IntValuesArray = new int[] { -5, -2, 0, 0, 1, 2, 3, 4, 4, 4, 4, 6, 6, 8, 9, 11, 11 };

        internal static List<Flight> Flights = new List<Flight>()
        {
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,1,10,30,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,3,10,30,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,3,16,30,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,5,16,30,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,6,10,0,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,8,10,0,0,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,8,10,0,1,DateTimeKind.Utc) },
             new Flight() { DepartureStation = "VNO", ArrivalStation = "LTN", DepartureTime = new DateTime(2015,1,20,10,0,1,DateTimeKind.Utc) },
        };

        public static void AssertExpcetion<T>(Action action)
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

        internal static IEnumerable<T> GetRangeSlow<T>(IEnumerable<T> source, T from, T to, Comparer<T> comparer = null)
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<T>.Default;
            return source.Where(e => comparer.Compare(from, e) < 1 && comparer.Compare(to, e) > -1);
        }

        internal static IEnumerable<T> GetRangeSlow<T, TSelected>(IEnumerable<T> source, Func<T, TSelected> selector, TSelected from, TSelected to, Comparer<TSelected> comparer = null)
        {
            //Get the default comparer
            comparer = comparer ?? Comparer<TSelected>.Default;
            return source.
                Where(e =>
                {
                    TSelected selected = selector(e);
                    return comparer.Compare(from, selected) < 1 && comparer.Compare(to, selected) > -1;
                });
        }

    }
}
