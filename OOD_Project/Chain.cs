using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public interface Chain
    {
        public void SetNextChain(Chain nextChain);
        public void ParseAndTransfer(string query, AllLists allLists);
    }

    public class ChainUpadate:Chain
    {
        public Chain? nextChain;

        public void SetNextChain(Chain _nextChain)
        {
            nextChain = _nextChain;
        }

        public void ParseAndTransfer(string query, AllLists allLists)
        {
            if(query.Length>=6 && query.Substring(0, 6)=="update")
            {
                
                Console.WriteLine("Upadate chain");
                string[] words = query.Split(' ');
                if(words.Length==1)
                {
                    throw new Exception("Wrong query!!!");
                }
                string className = words[1];
                switch (className)
                {
                    case "crew":
                        Context<Crew> crewContext = new Context<Crew>(allLists, allLists.crewList);
                        crewContext.findFieldOnName = new FindFieldOnNameCrew();
                        UpdateTree<Crew> updateTreeCrew = new UpdateTree<Crew>();
                        updateTreeCrew.CreateTree(query, crewContext);
                        //create tree for expression update with given Context
                        break;
                    case "passenger":
                        Context<Passenger> passengerContext = new Context<Passenger>(allLists, allLists.passengerList);
                        passengerContext.findFieldOnName = new FindFieldOnNamePassenger();
                        UpdateTree<Passenger> updateTreePassenger = new UpdateTree<Passenger>();
                        updateTreePassenger.CreateTree(query, passengerContext);
                        //create tree for expression update with given Context
                        break;
                    case "cargo":
                        Context<Cargo> cargoContext = new Context<Cargo>(allLists, allLists.cargoList);
                        cargoContext.findFieldOnName = new FindFieldOnNameCargo();
                        UpdateTree<Cargo> updateTreeCargo = new UpdateTree<Cargo>();
                        updateTreeCargo.CreateTree(query, cargoContext);
                        //create tree for expression update with given Context
                        break;
                    case "cargoplane":
                        Context<CargoPlane> cargoPlaneContext = new Context<CargoPlane>(allLists, allLists.cargoPlaneList);
                        cargoPlaneContext.findFieldOnName = new FindFieldOnNameCargoPlane();
                        UpdateTree<CargoPlane> updateTreeCargoPlane = new UpdateTree<CargoPlane>();
                        updateTreeCargoPlane.CreateTree(query, cargoPlaneContext);
                        //create tree for expression update with given Context
                        break;
                    case "passengerplane":
                        Context<PassengerPlane> passengerPlaneContext = new Context<PassengerPlane>(allLists, allLists.passengerPlaneList);
                        passengerPlaneContext.findFieldOnName = new FindFieldOnNamePassengerPlane();
                        UpdateTree<PassengerPlane> updateTreePassengerPlane = new UpdateTree<PassengerPlane>();
                        updateTreePassengerPlane.CreateTree(query, passengerPlaneContext);
                        //create tree for expression update with given Context
                        break;
                    case "airport":
                        Context<Airport> airportContext = new Context<Airport>(allLists, allLists.airportList);
                        airportContext.findFieldOnName = new FindFieldOnNameAirport();
                        UpdateTree<Airport> updateTreeAirport = new UpdateTree<Airport>();
                        updateTreeAirport.CreateTree(query, airportContext);
                        //create tree for expression update with given Context
                        break;
                    case "flight":
                        Context<Flight> flightContext = new Context<Flight>(allLists, allLists.flightList);
                        flightContext.findFieldOnName = new FindFieldOnNameFlight();
                        UpdateTree<Flight> updateTreeFlight = new UpdateTree<Flight>();
                        updateTreeFlight.CreateTree(query, flightContext);
                        //create tree for expression update with given Context
                        break;
                }

            }
            else
            {
                nextChain.ParseAndTransfer(query, allLists);
            }
        }
    }

    public class ChainDisplay : Chain
    {
        public Chain? nextChain;
        public void SetNextChain(Chain _nextChain)
        {
            nextChain = _nextChain;
        }

        public void ParseAndTransfer(string query, AllLists allLists)
        {
            if (query.Length >= 7 && query.Substring(0, 7) == "display")
            {
                //Console.WriteLine("Display chain");
                string[] words = query.Split(' ');
                string className = "";
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == "from" && i + 1 < words.Length)
                    {
                        className = words[i + 1];
                    }
                }
                switch (className)
                {
                    case "crew":
                        Context<Crew> crewContext = new Context<Crew>(allLists, allLists.crewList);
                        crewContext.findFieldOnName = new FindFieldOnNameCrew();
                        DisplayTree<Crew> crewTree = new DisplayTree<Crew>();
                        crewTree.CreateTree(query, crewContext);
                        break;
                    case "passenger":
                        Context<Passenger> passengerContext = new Context<Passenger>(allLists, allLists.passengerList);
                        passengerContext.findFieldOnName = new FindFieldOnNamePassenger();
                        DisplayTree<Passenger> passengerTree = new DisplayTree<Passenger>();
                        passengerTree.CreateTree(query, passengerContext);
                        break;
                    case "cargo":
                        Context<Cargo> cargoContext = new Context<Cargo>(allLists, allLists.cargoList);
                        cargoContext.findFieldOnName = new FindFieldOnNameCargo();
                        DisplayTree<Cargo> cargoTree = new DisplayTree<Cargo>();
                        cargoTree.CreateTree(query, cargoContext);
                        break;
                    case "cargoplane":
                        Context<CargoPlane> cargoPlaneContext = new Context<CargoPlane>(allLists, allLists.cargoPlaneList);
                        cargoPlaneContext.findFieldOnName = new FindFieldOnNameCargoPlane();
                        DisplayTree<CargoPlane> cargoPlaneTree = new DisplayTree<CargoPlane>();
                        cargoPlaneTree.CreateTree(query, cargoPlaneContext);
                        break;
                    case "passengerplane":
                        Context<PassengerPlane> passengerPlaneContext = new Context<PassengerPlane>(allLists, allLists.passengerPlaneList);
                        passengerPlaneContext.findFieldOnName = new FindFieldOnNamePassengerPlane();
                        DisplayTree<PassengerPlane> passengerPlaneTree = new DisplayTree<PassengerPlane>();
                        passengerPlaneTree.CreateTree(query, passengerPlaneContext);
                        break;
                    case "airport":
                        Context<Airport> airportContext = new Context<Airport>(allLists, allLists.airportList);
                        airportContext.findFieldOnName = new FindFieldOnNameAirport();
                        DisplayTree<Airport> airportTree = new DisplayTree<Airport>();
                        airportTree.CreateTree(query, airportContext);
                        break;
                    case "flight":
                        Context<Flight> flightContext = new Context<Flight>(allLists, allLists.flightList);
                        flightContext.findFieldOnName = new FindFieldOnNameFlight();
                        DisplayTree<Flight> flightTree = new DisplayTree<Flight>();
                        flightTree.CreateTree(query, flightContext);
                        break;

                }
            }
            else
            {
                nextChain.ParseAndTransfer(query, allLists);
            }
        }
    }

    public class ChainDelete : Chain
    {
        public Chain? nextChain;
        public void SetNextChain(Chain _nextChain)
        {
            nextChain = _nextChain;
        }

        public void ParseAndTransfer(string query, AllLists allLists)
        {
            if (query.Length >= 6 && query.Substring(0, 6) == "delete")
            {
                Console.WriteLine("Delete chain");
                string[] words = query.Split(' ');
                if (words.Length == 1)
                {
                    throw new Exception("Wrong query!!!");
                }
                string className = words[1];
                switch (className)
                {
                    case "crew":
                        Context<Crew> crewContext = new Context<Crew>(allLists, allLists.crewList);
                        crewContext.findFieldOnName = new FindFieldOnNameCrew();
                        DeleteTree<Crew> crewTree = new DeleteTree<Crew>();
                        crewTree.CreateTree(query, crewContext);
                        // create tree for expression update with given Context
                        break;
                    case "passenger":
                        Context<Passenger> passengerContext = new Context<Passenger>(allLists, allLists.passengerList);
                        passengerContext.findFieldOnName = new FindFieldOnNamePassenger();
                        DeleteTree<Passenger> passengerTree = new DeleteTree<Passenger>();
                        passengerTree.CreateTree(query, passengerContext);
                        // create tree for expression update with given Context
                        break;
                    case "cargo":
                        Context<Cargo> cargoContext = new Context<Cargo>(allLists, allLists.cargoList);
                        cargoContext.findFieldOnName = new FindFieldOnNameCargo();
                        DeleteTree<Cargo> cargoTree = new DeleteTree<Cargo>();
                        cargoTree.CreateTree(query, cargoContext);
                        // create tree for expression update with given Context
                        break;
                    case "cargoplane":
                        Context<CargoPlane> cargoPlaneContext = new Context<CargoPlane>(allLists, allLists.cargoPlaneList);
                        cargoPlaneContext.findFieldOnName = new FindFieldOnNameCargoPlane();
                        DeleteTree<CargoPlane> cargoPlaneTree = new DeleteTree<CargoPlane>();
                        cargoPlaneTree.CreateTree(query, cargoPlaneContext);
                        // create tree for expression update with given Context
                        break;
                    case "passengerplane":
                        Context<PassengerPlane> passengerPlaneContext = new Context<PassengerPlane>(allLists, allLists.passengerPlaneList);
                        passengerPlaneContext.findFieldOnName = new FindFieldOnNamePassengerPlane();
                        DeleteTree<PassengerPlane> passengerPlaneTree = new DeleteTree<PassengerPlane>();
                        passengerPlaneTree.CreateTree(query, passengerPlaneContext);
                        // create tree for expression update with given Context
                        break;
                    case "airport":
                        Context<Airport> airportContext = new Context<Airport>(allLists, allLists.airportList);
                        airportContext.findFieldOnName = new FindFieldOnNameAirport();
                        DeleteTree<Airport> airportTree = new DeleteTree<Airport>();
                        airportTree.CreateTree(query, airportContext);
                        // create tree for expression update with given Context
                        break;
                    case "flight":
                        Context<Flight> flightContext = new Context<Flight>(allLists, allLists.flightList);
                        flightContext.findFieldOnName = new FindFieldOnNameFlight();
                        DeleteTree<Flight> flightTree = new DeleteTree<Flight>();
                        flightTree.CreateTree(query, flightContext);
                        // create tree for expression update with given Context
                        break;
                    default:
                        throw new ArgumentException("Invalid class name");
                }

            }
            else
            {
                nextChain.ParseAndTransfer(query, allLists);
            }
        }
    }

    public class ChainAdd : Chain
    {
        public Chain? nextChain;
        public void SetNextChain(Chain _nextChain)
        {
            nextChain = _nextChain;
        }

        public void ParseAndTransfer(string query, AllLists allLists)
        {
            if (query.Length >= 3 && query.Substring(0, 3) == "add")
            {
                Console.WriteLine("Add chain");
                string[] words = query.Split(' ');
                if (words.Length == 1)
                {
                    throw new Exception("Wrong query!!!");
                }
                string className = words[1];
                switch (className)
                {
                    case "crew":
                        Context<Crew> crewContext = new Context<Crew>(allLists, allLists.crewList);
                        crewContext.findFieldOnName = new FindFieldOnNameCrew();
                        AddTree<Crew> crewTree = new AddTree<Crew>();
                        crewTree.CreateTree(query, crewContext);
                        //create tree for expression update with given Context
                        break;
                    case "passenger":
                        Context<Passenger> passengerContext = new Context<Passenger>(allLists, allLists.passengerList);
                        passengerContext.findFieldOnName = new FindFieldOnNamePassenger();
                        AddTree<Passenger> passengerTree = new AddTree<Passenger>();
                        passengerTree.CreateTree(query, passengerContext);
                        //create tree for expression update with given Context
                        break;
                    case "cargo":
                        Context<Cargo> cargoContext = new Context<Cargo>(allLists, allLists.cargoList);
                        cargoContext.findFieldOnName = new FindFieldOnNameCargo();
                        AddTree<Cargo> cargoTree = new AddTree<Cargo>();
                        cargoTree.CreateTree(query, cargoContext);
                        //create tree for expression update with given Context
                        break;
                    case "cargoplane":
                        Context<CargoPlane> cargoPlaneContext = new Context<CargoPlane>(allLists, allLists.cargoPlaneList);
                        cargoPlaneContext.findFieldOnName = new FindFieldOnNameCargoPlane();
                        AddTree<CargoPlane> cargoPlaneTree = new AddTree<CargoPlane>();
                        cargoPlaneTree.CreateTree(query, cargoPlaneContext);
                        //create tree for expression update with given Context
                        break;
                    case "passengerplane":
                        Context<PassengerPlane> passengerPlaneContext = new Context<PassengerPlane>(allLists, allLists.passengerPlaneList);
                        passengerPlaneContext.findFieldOnName = new FindFieldOnNamePassengerPlane();
                        AddTree<PassengerPlane> passengerPlaneTree = new AddTree<PassengerPlane>();
                        passengerPlaneTree.CreateTree(query, passengerPlaneContext);
                        //create tree for expression update with given Context
                        break;
                    case "airport":
                        Context<Airport> airportContext = new Context<Airport>(allLists, allLists.airportList);
                        airportContext.findFieldOnName = new FindFieldOnNameAirport();
                        AddTree<Airport> airportTree = new AddTree<Airport>();
                        airportTree.CreateTree(query, airportContext);
                        //create tree for expression update with given Context
                        break;
                    case "flight":
                        Context<Flight> flightContext = new Context<Flight>(allLists, allLists.flightList);
                        flightContext.findFieldOnName = new FindFieldOnNameFlight();
                        AddTree<Flight> flightTree = new AddTree<Flight>();
                        flightTree.CreateTree(query, flightContext);
                        //create tree for expression update with given Context
                        break;
                }


            }
            else
            {
                Console.WriteLine("Wrong command! Only: display, update, delete, add");
            }
        }
    }
}
