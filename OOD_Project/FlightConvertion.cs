using ExCSS;
using Mapsui.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class FlightConvertion
    {
        public List<FlightGUI> flightGUIList;
        
        public FlightConvertion(List<Flight> flightList, List<Airport>airportList)
        {
            flightGUIList = new List<FlightGUI>();
            foreach(Flight flight in flightList)
            {
               
                Airport? originAirport = airportList.Find(airport => airport.ID == flight.originID);
                Airport? targetAirport = airportList.Find(airport => airport.ID == flight.targetID);
                
                double timeDiffOrigin = (DateTime.Now - DateTime.ParseExact(flight.takeOffTime, "HH:mm", null)).TotalMilliseconds;
                double timeDiffTarget = (DateTime.ParseExact(flight.landingTime, "HH:mm", null) - DateTime.Now).TotalMilliseconds;
                double timeDiffFlight = (DateTime.ParseExact(flight.landingTime, "HH:mm", null) - DateTime.ParseExact(flight.takeOffTime, "HH:mm", null)).TotalMilliseconds;
                
                if (originAirport==null || targetAirport==null)
                {
                    throw new Exception("Airport not found");
                }

                if ((timeDiffOrigin > 0 && timeDiffTarget>0) || (timeDiffFlight<0 && timeDiffOrigin>0))
                {

                    if (timeDiffFlight < 0)
                    {
                        timeDiffFlight = (DateTime.ParseExact(flight.landingTime, "HH:mm", null).AddDays(1) - 
                            DateTime.ParseExact(flight.takeOffTime, "HH:mm", null)).TotalMilliseconds;
                    }

                    if (flight.prevLatitude == null || flight.prevLongitude == null)
                    {
                        flight.prevLongitude = originAirport.longitude;
                        flight.prevLatitude = originAirport.latitude;
                    }

                    (double latitude, double longitude)pos = CalculateWorldPosition(originAirport, targetAirport, timeDiffFlight, timeDiffOrigin);
                    double roatation = CalculateRotation(flight, (flight.prevLatitude.Value, flight.prevLongitude.Value),(pos.latitude, pos.longitude));
                    
                    FlightGUI pom = new FlightGUI()
                    {
                        ID = flight.ID,
                        MapCoordRotation = roatation,
                        WorldPosition = new WorldPosition(pos.latitude, pos.longitude)
                    };

                    flightGUIList.Add(pom);

                    flight.prevLatitude = pos.latitude;
                    flight.prevLongitude = pos.longitude;
                }
            }
        }

        // Using methods described on: https://www.movable-type.co.uk/scripts/latlong.html
        public (double latitude, double longitude) CalculateWorldPosition(Airport originAirport, Airport targetAirport, double timeDiffAirport, double timeDiffNow)
        {
            double fraction = timeDiffNow / timeDiffAirport;

            double lat1 = originAirport.latitude * Math.PI / 180.0;
            double lon1 = originAirport.longitude * Math.PI / 180.0;
            double lat2 = targetAirport.latitude * Math.PI / 180.0;
            double lon2 = targetAirport.longitude * Math.PI / 180.0;

            double aDistance = Math.Sin((lat2 - lat1) / 2) * Math.Sin((lat2 - lat1) / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin((lon2 - lon1) / 2) * Math.Sin((lon2 - lon1) / 2);

            double angularDistance = 2 * Math.Atan2(Math.Sqrt(aDistance), Math.Sqrt(1 - aDistance));

            double a = Math.Sin((1 - fraction) * angularDistance) / Math.Sin(angularDistance);
            double b = Math.Sin(fraction * angularDistance) / Math.Sin(angularDistance);
            double x = a * Math.Cos(lat1) * Math.Cos(lon1) + b * Math.Cos(lat2) * Math.Cos(lon2);
            double y = a * Math.Cos(lat1) * Math.Sin(lon1) + b * Math.Cos(lat2) * Math.Sin(lon2);
            double z = a * Math.Sin(lat1) + b * Math.Sin(lat2);

            double latitude = Math.Atan2(z, Math.Sqrt(x * x + y * y))*180.0/Math.PI;
            double longitude = Math.Atan2(y, x)*180.0/Math.PI;

            return (latitude, longitude);
        }

        public double CalculateRotation(Flight flight, (double latitude, double longitude)positionPrev,(double latitude, double longitude)position)
        {
            (double posPrevX, double posPrevY) = SphericalMercator.FromLonLat(positionPrev.longitude, positionPrev.latitude);
            (double posX, double posY) = SphericalMercator.FromLonLat(position.longitude, position.latitude);
            double deltaX = posX - posPrevX;
            double deltaY = posY - posPrevY;
            return Math.Atan2(deltaX, deltaY);
        }
    }
}
