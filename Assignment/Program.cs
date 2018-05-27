using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRepublic.FlightCodingTest;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FlightBuilder obj = new FlightBuilder();
                var AllFlightsResult = obj.GetFlights();
                if (AllFlightsResult != null)
                {
                    var Allflights = AllFlightsResult.ToList();
                    GetDepartFlightsBeforeTime(DateTime.Now, Allflights);
                    SegmentsForArrivalDateBeforeDeparture(Allflights);
                    TwoHoursGapOnGround(Allflights, 2);
                    Console.ReadKey();
                }
            }
            catch
            {
                throw;
            }

        }

        public static void GetDepartFlightsBeforeTime(DateTime CurrentTime, List<Flight> flights)
        {
            if (flights != null)
            {
                Console.WriteLine("--------Flights Which Depart before the current date/time.---------");
                var result = flights.Where(x => x.Segments.Any(z => z.DepartureDate < CurrentTime)).ToList();
                foreach (var item in result)
                {
                    foreach (var segment in item.Segments)
                    {
                        Console.WriteLine("Flight : Departure -  " + segment.DepartureDate + " Arrival - " + segment.ArrivalDate);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void SegmentsForArrivalDateBeforeDeparture(List<Flight> flights)
        {
            if (flights != null)
            {
                Console.WriteLine("------Flights Which Have a segment with an arrival date before the departure date.---");
                var result = flights.Where(x => x.Segments.Any(y => y.ArrivalDate < y.DepartureDate)).ToList();
                foreach (var item in result)
                {
                    foreach (var segment in item.Segments)
                    {
                        Console.WriteLine("Flight : Departure -  " + segment.DepartureDate + " Arrival - " + segment.ArrivalDate);
                    }
                }
                Console.WriteLine();
            }
        }
        public static void TwoHoursGapOnGround(List<Flight> flights, int SpendTime)
        {
            if (flights != null)
            {
                Console.WriteLine("--------Flights Which Spend more than " + SpendTime + " hours on the ground---------");

                List<Segment> lst = new List<Segment>();
                foreach (var item in flights)
                {
                    if (item.Segments.Count > 1)
                    {
                        Segment previousSegmnets = null;
                        foreach (var segment in item.Segments)
                        {
                            if (previousSegmnets != null)
                            {
                                if (previousSegmnets.ArrivalDate.AddHours(SpendTime) <= segment.DepartureDate)
                                {
                                    Console.WriteLine("Flight : Departure -  " + segment.DepartureDate + " Arrival - " + segment.ArrivalDate);
                                }
                            }
                            previousSegmnets = segment;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
