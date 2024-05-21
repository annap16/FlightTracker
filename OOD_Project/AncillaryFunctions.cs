using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class AncillaryFunctions
    {
        public static void SetStructures(List<Flight> flightList, AllLists allLists)
        {
            for(int i = 0; i < flightList.Count; i++)
            {
                flightList[i].originAirport = allLists.airportList.FirstOrDefault(airport => airport.ID == flightList[i].originID);
                flightList[i].targetAirport = allLists.airportList.FirstOrDefault(airport => airport.ID == flightList[i].targetID);
                flightList[i].plane = allLists.passengerPlaneList.FirstOrDefault(plane => plane.ID == flightList[i].planeID);
                if (flightList[i].plane == null) 
                {
                    flightList[i].plane = allLists.cargoPlaneList.FirstOrDefault(plane => plane.ID == flightList[i].planeID);
                }
            }
        }
    }

    public class GenericCompare<T> where T: IComparable<T>
    {
        public static bool Compare(T leftParsed, T rightParsed, string operand) 
        {
            switch (operand)
            {
                case "<":
                    return leftParsed.CompareTo(rightParsed) < 0;
                case "<=":
                    return leftParsed.CompareTo(rightParsed) <= 0;
                case ">":
                    return leftParsed.CompareTo(rightParsed) > 0;
                case ">=":
                    return leftParsed.CompareTo(rightParsed) >= 0;
                case "=":
                    return leftParsed.CompareTo(rightParsed) == 0;
                case "!=":
                    return leftParsed.CompareTo(rightParsed) != 0;
                default:
                    throw new Exception("Wrong operand in where");
            }
        }

        public bool Compare(string leftParsed, string rightParsed, string operand)
        {
            switch (operand)
            {
                case "=":
                    return leftParsed == rightParsed;
                case "!=":
                    return leftParsed != rightParsed;
                default:
                    throw new Exception("Wrong operand in where");
            }
        }
    }
}
