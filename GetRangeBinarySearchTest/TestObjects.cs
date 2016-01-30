using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRangeBinarySearchTest
{
    internal static class TestObjects
    {
        internal static List<int> intValues = new List<int>() { -5, -2, 0, 0, 1, 2, 3, 4, 4, 4, 4, 6, 6, 8, 9, 11, 11 };

        internal static List<Flight> flights = new List<Flight>()
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

    }
}
