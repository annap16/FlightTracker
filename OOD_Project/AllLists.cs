using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSourceSimulator;

namespace OOD_Project
{
    public class AllLists
    {
        public List<Crew> crewList;
        public List<Passenger> passengerList;
        public List<Cargo> cargoList;
        public List<CargoPlane> cargoPlaneList;
        public List<PassengerPlane> passengerPlaneList;
        public List<Airport> airportList;
        public List<Flight> flightList;


        public AllLists()
        {
            crewList = new List<Crew>();
            passengerList = new List<Passenger>();
            cargoList = new List<Cargo>();
            cargoPlaneList = new List<CargoPlane>();
            passengerPlaneList = new List<PassengerPlane>();
            airportList = new List<Airport>();
            flightList = new List<Flight>();
        }

        public static DataType AddCrew(string[] data, AllLists lists)
        {
            CrewFactory pomCrew = new CrewFactory();
            Crew newCrew = pomCrew.Create(data);
            lists.crewList.Add(newCrew);
            return newCrew;
        }

        public static DataType AddCrew(byte[] data, AllLists lists)
        {
            CrewFactory pomCrew = new CrewFactory();
            Crew newCrew = pomCrew.Create(data);
            lists.crewList.Add(newCrew);
            return newCrew;
        }

        public static DataType AddPassenger(string[]data, AllLists lists)
        {
            PassengerFactory pomPassenger = new PassengerFactory();
            Passenger newPassenger = pomPassenger.Create(data);
            lists.passengerList.Add(newPassenger);
            return newPassenger;
        }

        public static DataType AddPassenger(byte[] data, AllLists lists)
        {
            PassengerFactory pomPassenger = new PassengerFactory();
            Passenger newPassenger = pomPassenger.Create(data);
            lists.passengerList.Add(newPassenger);
            return newPassenger;
        }

        public static DataType AddCargo(string[]data, AllLists lists) 
        {
            CargoFactory pomCargo = new CargoFactory();
            Cargo newCargo = pomCargo.Create(data);
            lists.cargoList.Add(newCargo);
            return newCargo;
        }

        public static DataType AddCargo(byte[] data, AllLists lists)
        {
            CargoFactory pomCargo = new CargoFactory();
            Cargo newCargo = pomCargo.Create(data);
            lists.cargoList.Add(newCargo);
            return newCargo;
        }

        public static DataType AddCargoPlane(string[]data, AllLists lists)
        {
            CargoPlaneFactory pomCargoPlane = new CargoPlaneFactory();
            CargoPlane newCargoPlane = pomCargoPlane.Create(data);
            lists.cargoPlaneList.Add(newCargoPlane);
            return newCargoPlane;
        }

        public static DataType AddCargoPlane(byte[] data, AllLists lists)
        {
            CargoPlaneFactory pomCargoPlane = new CargoPlaneFactory();
            CargoPlane newCargoPlane = pomCargoPlane.Create(data);
            lists.cargoPlaneList.Add(newCargoPlane);
            return newCargoPlane;
        }

        public static DataType AddPassengerPlane(string[]data, AllLists lists)
        {
            PassengerPlaneFactory pomPassengerPlane = new PassengerPlaneFactory();
            PassengerPlane newPassengerPlane = pomPassengerPlane.Create(data);
            lists.passengerPlaneList.Add(newPassengerPlane);
            return newPassengerPlane;
        }

        public static DataType AddPassengerPlane(byte[] data, AllLists lists)
        {
            PassengerPlaneFactory pomPassengerPlane = new PassengerPlaneFactory();
            PassengerPlane newPassengerPlane = pomPassengerPlane.Create(data);
            lists.passengerPlaneList.Add(newPassengerPlane);
            return newPassengerPlane;
        }

        public static DataType AddAirport(string[]data, AllLists lists)
        {
            AirportFactory pomAirport = new AirportFactory();
            Airport newAirport = pomAirport.Create(data);
            lists.airportList.Add(newAirport);
            return newAirport;
        }

        public static DataType AddAirport(byte[] data, AllLists lists)
        {
            AirportFactory pomAirport = new AirportFactory();
            Airport newAirport = pomAirport.Create(data);
            lists.airportList.Add(newAirport);
            return newAirport;
        }

        public static DataType AddFlight(string[]data, AllLists lists)
        {
            FlightFactory pomFlight = new FlightFactory();
            Flight newFlight = pomFlight.Create(data);
            lists.flightList.Add(newFlight);
            return newFlight;
        }

        public static DataType AddFlight(byte[] data, AllLists lists)
        {
            FlightFactory pomFlight = new FlightFactory();
            Flight newFlight = pomFlight.Create(data);
            lists.flightList.Add(newFlight);
            return newFlight;
        }


    }
}
