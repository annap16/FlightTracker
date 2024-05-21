using System;
using System.Collections.Generic;

namespace OOD_Project
{
    public abstract class FindFieldOnName
    {
        public Dictionary<string, Func<DataType, UInt64>> dictionaryUInt64;
        public Dictionary<string, Func<DataType, UInt16>> dictionaryUInt16;
        public Dictionary<string, Func<DataType, Single>> dictionarySingle;
        public Dictionary<string, Func<DataType, string>> dictionaryString;

        public Dictionary<string, Action<DataType, UInt64>> setDictionaryUInt64;
        public Dictionary<string, Action<DataType, UInt16>> setDictionaryUInt16;
        public Dictionary<string, Action<DataType, Single>> setDictionarySingle;
        public Dictionary<string, Action<DataType, string>> setDictionaryString;


        public FindFieldOnName()
        {
            dictionaryUInt64 = new Dictionary<string, Func<DataType, UInt64>>();
            dictionaryUInt16 = new Dictionary<string, Func<DataType, UInt16>>();
            dictionarySingle = new Dictionary<string, Func<DataType, Single>>();
            dictionaryString = new Dictionary<string, Func<DataType, string>>();

            setDictionaryUInt64 = new Dictionary<string, Action<DataType, UInt64>>();
            setDictionaryUInt16 = new Dictionary<string, Action<DataType, UInt16>>();
            setDictionarySingle = new Dictionary<string, Action<DataType, Single>>();
            setDictionaryString = new Dictionary<string, Action<DataType, string>>();
        }
        
        public void ParseTableFLightCrew(Flight flight, string table)
        {
            string pattern = @"[\[\]]";
            string tableClean = table.Replace(pattern, "");
            string[] stringDiv = tableClean.Split(";");
            flight.crewID = new UInt64[stringDiv.Length];
            for(int i=0; i<stringDiv.Length;i++)
            {
                string pom = stringDiv[i].Trim();
                flight.crewID[i] = UInt64.Parse(pom);   
            }
        }
        public void ParseTableFLightLoad(Flight flight, string table)
        {
            string pattern = @"[\[\]]";
            string tableClean = table.Replace(pattern, "");
            string[] stringDiv = tableClean.Split(";");
            flight.loadID = new UInt64[stringDiv.Length];
            for (int i = 0; i < stringDiv.Length; i++)
            {
                string pom = stringDiv[i].Trim();
                flight.loadID[i] = UInt64.Parse(pom);
            }
        }
    }

    public class FindFieldOnNameAirport : FindFieldOnName
    {
        public FindFieldOnNameAirport()
        {
            dictionaryUInt64["ID"] = airport => (airport as Airport).ID;
            dictionarySingle["longitude"] = airport => (airport as Airport).longitude;
            dictionarySingle["latitude"] = airport => (airport as Airport).latitude;
            dictionarySingle["AMSL"] = airport => (airport as Airport).AMSL;
            dictionaryString["type"] = airport => (airport as Airport).type;
            dictionaryString["name"] = airport => (airport as Airport).name;
            dictionaryString["code"] = airport => (airport as Airport).code;
            dictionaryString["country"] = airport => (airport as Airport).country;

            setDictionaryUInt64["ID"] = (airport, value) => (airport as Airport).ID = value;
            setDictionarySingle["longitude"] = (airport, value) => (airport as Airport).longitude = value;
            setDictionarySingle["latitude"] = (airport, value) => (airport as Airport).latitude = value;
            setDictionarySingle["AMSL"] = (airport, value) => (airport as Airport).AMSL = value;
            setDictionaryString["type"] = (airport, value) => (airport as Airport).type = value;
            setDictionaryString["name"] = (airport, value) => (airport as Airport).name = value;
            setDictionaryString["code"] = (airport, value) => (airport as Airport).code = value;
            setDictionaryString["country"] = (airport, value) => (airport as Airport).country = value;

        }
    }

    public class FindFieldOnNameCargo : FindFieldOnName
    {
        public FindFieldOnNameCargo()
        {
            dictionaryUInt64["ID"] = cargo => (cargo as Cargo).ID;
            dictionarySingle["weight"] = cargo => (cargo as Cargo).weight;
            dictionaryString["type"] = cargo => (cargo as Cargo).type;
            dictionaryString["code"] = cargo => (cargo as Cargo).code;
            dictionaryString["description"] = cargo => (cargo as Cargo).description;

            setDictionaryUInt64["ID"] = (cargo, value) => (cargo as Cargo).ID = value;
            setDictionarySingle["weight"] = (cargo, value) => (cargo as Cargo).weight = value;
            setDictionaryString["type"] = (cargo, value) => (cargo as Cargo).type = value;
            setDictionaryString["code"] = (cargo, value) => (cargo as Cargo).code = value;
            setDictionaryString["description"] = (cargo, value) => (cargo as Cargo).description = value;
        }
    }

    public class FindFieldOnNameCargoPlane : FindFieldOnName
    {
        public FindFieldOnNameCargoPlane()
        {
            dictionaryUInt64["ID"] = cargoplane => (cargoplane as CargoPlane).ID;
            dictionarySingle["maxLoad"] = cargoplane => (cargoplane as CargoPlane).maxLoad;
            dictionaryString["type"] = cargoplane => (cargoplane as CargoPlane).type;
            dictionaryString["serial"] = cargoplane => (cargoplane as CargoPlane).serial;
            dictionaryString["country"] = cargoplane => (cargoplane as CargoPlane).country;
            dictionaryString["model"] = cargoplane => (cargoplane as CargoPlane).model;

            setDictionaryUInt64["ID"] = (cargoplane, value) => (cargoplane as CargoPlane).ID = value;
            setDictionarySingle["maxLoad"] = (cargoplane, value) => (cargoplane as CargoPlane).maxLoad = value;
            setDictionaryString["type"] = (cargoplane, value) => (cargoplane as CargoPlane).type = value;
            setDictionaryString["serial"] = (cargoplane, value) => (cargoplane as CargoPlane).serial = value;
            setDictionaryString["country"] = (cargoplane, value) => (cargoplane as CargoPlane).country = value;
            setDictionaryString["model"] = (cargoplane, value) => (cargoplane as CargoPlane).model = value;
        }
    }

    public class FindFieldOnNameCrew : FindFieldOnName
    {
        public FindFieldOnNameCrew()
        {
            dictionaryUInt64["ID"] = crew => (crew as Crew).ID;
            dictionaryUInt64["age"] = crew => (crew as Crew).age;
            dictionaryUInt64["practice"] = crew => (crew as Crew).practice;
            dictionaryString["type"] = crew => (crew as Crew).type;
            dictionaryString["name"] = crew => (crew as Crew).name;
            dictionaryString["phone"] = crew => (crew as Crew).phone;
            dictionaryString["email"] = crew => (crew as Crew).email;
            dictionaryString["role"] = crew => (crew as Crew).role;

            setDictionaryUInt64["ID"] = (crew, value) => (crew as Crew).ID = value;
            setDictionaryUInt64["age"] = (crew, value) => (crew as Crew).age = value;
            setDictionaryUInt64["practice"] = (crew, value) => (crew as Crew).practice = value;
            setDictionaryString["type"] = (crew, value) => (crew as Crew).type = value;
            setDictionaryString["name"] = (crew, value) => (crew as Crew).name = value;
            setDictionaryString["phone"] = (crew, value) => (crew as Crew).phone = value;
            setDictionaryString["email"] = (crew, value) => (crew as Crew).email = value;
            setDictionaryString["role"] = (crew, value) => (crew as Crew).role = value;
        }
    }

    public class FindFieldOnNameFlight : FindFieldOnName
    {
        
        public FindFieldOnNameFlight()
        {
            dictionaryUInt64["ID"] = flight => (flight as Flight).ID;
            dictionaryUInt64["originID"] = flight => (flight as Flight).originID;
            dictionaryUInt64["targetID"] = flight => (flight as Flight).targetID;
            dictionaryUInt64["planeID"] = flight => (flight as Flight).planeID;
            dictionarySingle["longitude"] = flight => (flight as Flight).longitude;
            dictionarySingle["latitude"] = flight => (flight as Flight).latitude;
            dictionarySingle["AMSL"] = flight => (flight as Flight).AMSL;
            dictionaryString["type"] = flight => (flight as Flight).type;
            dictionaryString["takeOffTime"] = flight => (flight as Flight).takeOffTime;
            dictionaryString["landingTime"] = flight => (flight as Flight).landingTime;

            setDictionaryUInt64["ID"] = (flight, value) => (flight as Flight).ID = value;
            setDictionaryUInt64["originID"] = (flight, value) => (flight as Flight).originID = value;
            setDictionaryUInt64["targetID"] = (flight, value) => (flight as Flight).targetID = value;
            setDictionaryUInt64["planeID"] = (flight, value) => (flight as Flight).planeID = value;
            setDictionarySingle["longitude"] = (flight, value) => (flight as Flight).longitude = value;
            setDictionarySingle["latitude"] = (flight, value) => (flight as Flight).latitude = value;
            setDictionarySingle["AMSL"] = (flight, value) => (flight as Flight).AMSL = value;
            setDictionaryString["type"] = (flight, value) => (flight as Flight).type = value;
            setDictionaryString["takeOffTime"] = (flight, value) => (flight as Flight).takeOffTime = value;
            setDictionaryString["landingTime"] = (flight, value) => (flight as Flight).landingTime = value;
            setDictionaryString["crewID"] = (flight, value) => ParseTableFLightCrew(flight as Flight, value);
            setDictionaryString["loadID"] = (flight, value) => ParseTableFLightLoad(flight as Flight, value);


            dictionaryUInt64["plane.ID"] = flight => (flight as Flight).plane.ID;
            dictionaryUInt16["plane.firstClassSize"] = flight => ((flight as Flight).plane as PassengerPlane).firstClassSize;
            dictionaryUInt16["plane.businessClassSize"] = flight => ((flight as Flight).plane as PassengerPlane).businessClassSize;
            dictionaryUInt16["plane.economyClassSize"] = flight => ((flight as Flight).plane as PassengerPlane).economyClassSize;
            dictionarySingle["plane.maxLoad"] = flight => ((flight as Flight).plane as CargoPlane).maxLoad;
            dictionaryString["plane.type"] = flight => (flight as Flight).plane.type;
            dictionaryString["plane.serial"] = flight => (flight as Flight).plane.serial;
            dictionaryString["plane.country"] = flight => (flight as Flight).plane.country;
            dictionaryString["plane.model"] = flight => (flight as Flight).plane.model;

            setDictionaryUInt64["plane.ID"] = (flight, value) => (flight as Flight).plane.ID = value;
            setDictionaryUInt16["plane.firstClassSize"] = (flight, value) => ((flight as Flight).plane as PassengerPlane).firstClassSize = value;
            setDictionaryUInt16["plane.businessClassSize"] = (flight, value) => ((flight as Flight).plane as PassengerPlane).businessClassSize = value;
            setDictionaryUInt16["plane.economyClassSize"] = (flight, value) => ((flight as Flight).plane as PassengerPlane).economyClassSize = value;
            setDictionarySingle["maxLoad"] = (flight, value) => ((flight as Flight).plane as CargoPlane).maxLoad = value;
            setDictionaryString["plane.type"] = (flight, value) => (flight as Flight).plane.type = value;
            setDictionaryString["plane.serial"] = (flight, value) => (flight as Flight).plane.serial = value;
            setDictionaryString["plane.country"] = (flight, value) => (flight as Flight).plane.country = value;
            setDictionaryString["plane.model"] = (flight, value) => (flight as Flight).plane.model = value;

            dictionaryUInt64["originAirport.ID"] = flight => (flight as Flight).originAirport.ID;
            dictionarySingle["originAirport.longitude"] = flight => (flight as Flight).originAirport.longitude;
            dictionarySingle["originAirport.latitude"] = flight => (flight as Flight).originAirport.latitude;
            dictionarySingle["originAirport.AMSL"] = flight => (flight as Flight).originAirport.AMSL;
            dictionaryString["originAirport.type"] = flight => (flight as Flight).originAirport.type;
            dictionaryString["originAirport.name"] = flight => (flight as Flight).originAirport.name;
            dictionaryString["originAirport.code"] = flight => (flight as Flight).originAirport.code;
            dictionaryString["originAirport.country"] = flight => (flight as Flight).originAirport.country;

            setDictionaryUInt64["originAirport.ID"] = (flight, value) => (flight as Flight).originAirport.ID = value;
            setDictionarySingle["originAirport.longitude"] = (flight, value) => (flight as Flight).originAirport.longitude = value;
            setDictionarySingle["originAirport.latitude"] = (flight, value) => (flight as Flight).originAirport.latitude = value;
            setDictionarySingle["originAirport.AMSL"] = (flight, value) => (flight as Flight).originAirport.AMSL = value;
            setDictionaryString["originAirport.type"] = (flight, value) => (flight as Flight).originAirport.type = value;
            setDictionaryString["originAirport.name"] = (flight, value) => (flight as Flight).originAirport.name = value;
            setDictionaryString["originAirport.code"] = (flight, value) => (flight as Flight).originAirport.code = value;
            setDictionaryString["originAirport.country"] = (flight, value) => (flight as Flight).originAirport.country = value;

            dictionaryUInt64["targetAirport.ID"] = flight => (flight as Flight).targetAirport.ID;
            dictionarySingle["targetAirport.longitude"] = flight => (flight as Flight).targetAirport.longitude;
            dictionarySingle["targetAirport.latitude"] = flight => (flight as Flight).targetAirport.latitude;
            dictionarySingle["targetAirport.AMSL"] = flight => (flight as Flight).targetAirport.AMSL;
            dictionaryString["targetAirport.type"] = flight => (flight as Flight).targetAirport.type;
            dictionaryString["targetAirport.name"] = flight => (flight as Flight).targetAirport.name;
            dictionaryString["targetAirport.code"] = flight => (flight as Flight).targetAirport.code;
            dictionaryString["targetAirport.country"] = flight => (flight as Flight).targetAirport.country;

            setDictionaryUInt64["targetAirport.ID"] = (flight, value) => (flight as Flight).targetAirport.ID = value;
            setDictionarySingle["targetAirport.longitude"] = (flight, value) => (flight as Flight).targetAirport.longitude = value;
            setDictionarySingle["targetAirport.latitude"] = (flight, value) => (flight as Flight).targetAirport.latitude = value;
            setDictionarySingle["targetAirport.AMSL"] = (flight, value) => (flight as Flight).targetAirport.AMSL = value;
            setDictionaryString["targetAirport.type"] = (flight, value) => (flight as Flight).targetAirport.type = value;
            setDictionaryString["targetAirport.name"] = (flight, value) => (flight as Flight).targetAirport.name = value;
            setDictionaryString["targetAirport.code"] = (flight, value) => (flight as Flight).targetAirport.code = value;
            setDictionaryString["targetAirport.country"] = (flight, value) => (flight as Flight).targetAirport.country = value;



        }
    }

    public class FindFieldOnNamePassenger : FindFieldOnName
    {
        public FindFieldOnNamePassenger()
        {
            dictionaryUInt64["ID"] = passenger => (passenger as Passenger).ID;
            dictionaryUInt64["age"] = passenger => (passenger as Passenger).age;
            dictionaryUInt64["miles"] = passenger => (passenger as Passenger).miles;
            dictionaryString["type"] = passenger => (passenger as Passenger).type;
            dictionaryString["name"] = passenger => (passenger as Passenger).name;
            dictionaryString["phone"] = passenger => (passenger as Passenger).phone;
            dictionaryString["email"] = passenger => (passenger as Passenger).email;
            dictionaryString["class"] = passenger => (passenger as Passenger).classFlight;

            setDictionaryUInt64["ID"] = (passenger, value) => (passenger as Passenger).ID = value;
            setDictionaryUInt64["age"] = (passenger, value) => (passenger as Passenger).age = value;
            setDictionaryUInt64["miles"] = (passenger, value) => (passenger as Passenger).miles = value;
            setDictionaryString["type"] = (passenger, value) => (passenger as Passenger).type = value;
            setDictionaryString["name"] = (passenger, value) => (passenger as Passenger).name = value;
            setDictionaryString["phone"] = (passenger, value) => (passenger as Passenger).phone = value;
            setDictionaryString["email"] = (passenger, value) => (passenger as Passenger).email = value;
            setDictionaryString["class"] = (passenger, value) => (passenger as Passenger).classFlight = value;
        }
    }

    public class FindFieldOnNamePassengerPlane : FindFieldOnName
    {
        public FindFieldOnNamePassengerPlane()
        {
            dictionaryUInt64["ID"] = passplane => (passplane as PassengerPlane).ID;
            dictionaryUInt16["firstClassSize"] = passplane => (passplane as PassengerPlane).firstClassSize;
            dictionaryUInt16["businessClassSize"] = passplane => (passplane as PassengerPlane).businessClassSize;
            dictionaryUInt16["economyClassSize"] = passplane => (passplane as PassengerPlane).economyClassSize;
            dictionaryString["type"] = passplane => (passplane as PassengerPlane).type;
            dictionaryString["serial"] = passplane => (passplane as PassengerPlane).serial;
            dictionaryString["country"] = passplane => (passplane as PassengerPlane).country;
            dictionaryString["model"] = passplane => (passplane as PassengerPlane).model;

            setDictionaryUInt64["ID"] = (passplane, value) => (passplane as PassengerPlane).ID = value;
            setDictionaryUInt16["firstClassSize"] = (passplane, value) => (passplane as PassengerPlane).firstClassSize = value;
            setDictionaryUInt16["businessClassSize"] = (passplane, value) => (passplane as PassengerPlane).businessClassSize = value;
            setDictionaryUInt16["economyClassSize"] = (passplane, value) => (passplane as PassengerPlane).economyClassSize = value;
            setDictionaryString["type"] = (passplane, value) => (passplane as PassengerPlane).type = value;
            setDictionaryString["serial"] = (passplane, value) => (passplane as PassengerPlane).serial = value;
            setDictionaryString["country"] = (passplane, value) => (passplane as PassengerPlane).country = value;
            setDictionaryString["model"] = (passplane, value) => (passplane as PassengerPlane).model = value;

        }
    }
}
