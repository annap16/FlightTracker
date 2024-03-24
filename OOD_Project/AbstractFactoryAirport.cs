using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class DataTypeFactory
    {
        public abstract DataType Create(string[] data);
        public abstract DataType Create(byte[] data);
        
    }

    public abstract class PersonFactory: DataTypeFactory
    {
        public override abstract DataType Create(string[] data);
        public override abstract DataType Create(byte[] data);

    }

    public class CrewFactory: PersonFactory
    {
        public CrewFactory(){ }
        public override Crew Create(string [] data)
        {
            string type = data[0];
            UInt64 ID = UInt64.Parse(data[1]);
            string name = data[2];
            UInt64 age = UInt64.Parse(data[3]);
            string phone = data[4];
            string email = data[5];
            UInt64 practice = UInt64.Parse(data[6]);
            string role = data[7];
            return new Crew(type, ID, name, age, phone, email, practice, role);
        }
        public override Crew Create(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string type = new string(binaryReader.ReadChars(3));
            UInt32 length = binaryReader.ReadUInt32();
            UInt64 ID = binaryReader.ReadUInt64();
            UInt16 nameLength = binaryReader.ReadUInt16();
            string name = new string(binaryReader.ReadChars(nameLength));
            UInt16 age = binaryReader.ReadUInt16();
            string phone = new string(binaryReader.ReadChars(12));
            UInt16 emailLenght = binaryReader.ReadUInt16();
            string email = new string(binaryReader.ReadChars(emailLenght));
            UInt16 practice = binaryReader.ReadUInt16();
            string role = binaryReader.ReadChar().ToString();
            return new Crew(type, ID, name, age, phone, email, practice, role);
        }

        
    }

    public class PassengerFactory: PersonFactory
    {
        public PassengerFactory() { }
        public override Passenger Create(string[] data)
        {
            string type = data[0];
            UInt64 ID = UInt64.Parse(data[1]);
            string name = data[2];
            UInt64 age = UInt64.Parse(data[3]);
            string phone = data[4];
            string email = data[5];
            string classFlight = data[6];
            UInt64 miles = UInt64.Parse(data[7]);
            return new Passenger(type, ID, name, age, phone, email, classFlight, miles);
        }

        public override Passenger Create(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string type = new string(binaryReader.ReadChars(3));
            UInt32 messageLength = binaryReader.ReadUInt32();
            UInt64 ID = binaryReader.ReadUInt64();
            UInt16 nameLength = binaryReader.ReadUInt16();
            string name = new string(binaryReader.ReadChars(nameLength));
            UInt16 age = binaryReader.ReadUInt16();
            string phone = new string(binaryReader.ReadChars(12));
            UInt16 emailLength = binaryReader.ReadUInt16();
            string email = new string(binaryReader.ReadChars(emailLength));
            string classFlight = binaryReader.ReadChar().ToString();
            UInt64 miles = binaryReader.ReadUInt64();
            return new Passenger(type, ID, name, age, phone, email, classFlight, miles);
        }
    }

    public class CargoFactory: DataTypeFactory
    {
        public CargoFactory() { }
        public override Cargo Create(string[] data)
        {
            string type = data[0];
            UInt64 ID = UInt64.Parse(data[1]);
            Single weight = Single.Parse(data[2], CultureInfo.InvariantCulture);
            string code = data[3];
            string description = data[4];
            return new Cargo(type, ID, weight, code, description);
        }
        public override Cargo Create(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string type = new string(binaryReader.ReadChars(3));
            UInt32 messageLength = binaryReader.ReadUInt32();
            UInt64 ID = binaryReader.ReadUInt64();
            Single weight = binaryReader.ReadSingle();
            string code = new string(binaryReader.ReadChars(6));
            UInt16 descriptionLength = binaryReader.ReadUInt16();
            string description = new string(binaryReader.ReadChars(descriptionLength));
            return new Cargo(type, ID, weight, code, description);
        }

    }

    public abstract class PlaneFactory: DataTypeFactory
    {
        public abstract override DataType Create(string[] data);
    }

    public class CargoPlaneFactory: PlaneFactory
    {
        public CargoPlaneFactory() { }
        public override CargoPlane Create(string[] data)
        {
            string type = data[0];
            UInt64 ID = UInt64.Parse(data[1]);
            string serial = data[2];
            string country = data[3];
            string model = data[4];
            Single maxLoad = Single.Parse(data[5], CultureInfo.InvariantCulture);
            return new CargoPlane(type, ID, serial, country, model, maxLoad);
        }
        public override CargoPlane Create(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string type = new string(binaryReader.ReadChars(3));
            UInt32 messageLength = binaryReader.ReadUInt32();
            UInt64 ID = binaryReader.ReadUInt64();
            string serial = new string(binaryReader.ReadChars(10)).Replace("\0", "");
            string country = new string(binaryReader.ReadChars(3));
            UInt16 modelLength = binaryReader.ReadUInt16();
            string model = new string(binaryReader.ReadChars(modelLength));
            Single maxLoad = binaryReader.ReadSingle();
            return new CargoPlane(type, ID, serial, country, model, maxLoad);
        }
    }

    public class PassengerPlaneFactory: PlaneFactory
    {
        public PassengerPlaneFactory() { }
        public override PassengerPlane Create(string[] data)
        {
            string type = data[0];
            UInt64 ID = UInt64.Parse(data[1]);
            string serial = data[2];
            string country = data[3];
            string model = data[4];
            UInt16 firstClassSize = UInt16.Parse(data[5]);
            UInt16 businessClassSize = UInt16.Parse(data[6]);
            UInt16 economyClassSize = UInt16.Parse(data[7]);
            return new PassengerPlane(type, ID, serial, country, model,
                firstClassSize, businessClassSize, economyClassSize);
        }
        public override PassengerPlane Create(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string type = new string(binaryReader.ReadChars(3));
            UInt32 messageLength = binaryReader.ReadUInt32();
            UInt64 ID = binaryReader.ReadUInt64();
            string serial = new string(binaryReader.ReadChars(10)).Replace("\0", "");
            string country = new string(binaryReader.ReadChars(3));
            UInt16 modelLength = binaryReader.ReadUInt16();
            string model = new string(binaryReader.ReadChars(modelLength));
            UInt16 firstClassSize = binaryReader.ReadUInt16();
            UInt16 businessClassSize = binaryReader.ReadUInt16();
            UInt16 economyClassSize = binaryReader.ReadUInt16();
            return new PassengerPlane(type, ID, serial, country, model,
                firstClassSize, businessClassSize, economyClassSize);
        }

    }

    public class AirportFactory: DataTypeFactory
    {
        public AirportFactory() { }
        public override Airport Create(string[] data)
        {
            string type = data[0];
            UInt64 ID = UInt64.Parse(data[1]);
            string name = data[2];
            string code = data[3];
            Single longitude = Single.Parse(data[4], CultureInfo.InvariantCulture);
            Single latitude = Single.Parse(data[5], CultureInfo.InvariantCulture);
            Single AMSL = Single.Parse(data[6], CultureInfo.InvariantCulture);
            string country = data[7];
            return new Airport(type, ID, name, code, longitude, latitude, AMSL, country);
        }
        public override Airport Create(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string type = new string(binaryReader.ReadChars(3));
            UInt32 meesageLength = binaryReader.ReadUInt32();
            UInt64 ID = binaryReader.ReadUInt64();
            UInt16 nameLength = binaryReader.ReadUInt16();
            string name = new string(binaryReader.ReadChars(nameLength));
            string code = new string(binaryReader.ReadChars(3));
            Single longitude = binaryReader.ReadSingle();
            Single latitude = binaryReader.ReadSingle();
            Single AMSL = binaryReader.ReadSingle();
            string country = new string(binaryReader.ReadChars(3));
            return new Airport(type, ID, name, code, longitude, latitude, AMSL, country);
        }
    }

    public class FlightFactory: DataTypeFactory
    {
        public FlightFactory() { }
        public override Flight Create(string[] data)
        {
            string type = data[0];
            UInt64 ID = UInt64.Parse(data[1]);
            UInt64 originID = UInt64.Parse(data[2]);
            UInt64 targetID = UInt64.Parse(data[3]);
            string takeOffTime = data[4];
            string landingTime = data[5];
            Single longitude = Single.Parse(data[6], CultureInfo.InvariantCulture);
            Single latitude = Single.Parse(data[7], CultureInfo.InvariantCulture);
            Single AMSL = Single.Parse(data[8], CultureInfo.InvariantCulture);
            ulong planeID = UInt64.Parse(data[9]);

            string crewIDPom = data[10].Substring(1, data[10].Length - 2);
            string loadIDPom = data[11].Substring(1, data[11].Length - 2);
            crewIDPom += '\0';
            loadIDPom += '\0';
            string[] crewIDString = crewIDPom.Split(";");
            string[] loadIDString = loadIDPom.Split(";");
            UInt64[] crewID = new UInt64[crewIDString.Length];
            UInt64[] loadID = new UInt64[loadIDString.Length];
            for (int i = 0; i < crewIDString.Length; i++)
            {
                crewID[i] = UInt64.Parse(crewIDString[i]);
            }
            for (int i = 0; i < loadIDString.Length; i++)
            {
                loadID[i] = UInt64.Parse(loadIDString[i]);
            }

            return new Flight(type, ID, originID, targetID, 
                takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewID, loadID);
        }
        public override Flight Create(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string type = new string(binaryReader.ReadChars(3));
            UInt32 messageLength = binaryReader.ReadUInt32();
            UInt64 ID = binaryReader.ReadUInt64();
            UInt64 originID = binaryReader.ReadUInt64();
            UInt64 targetID = binaryReader.ReadUInt64();
            Int64 takeOffTimeMs = binaryReader.ReadInt64();
            string takeOffTime = DateTimeOffset.FromUnixTimeMilliseconds(takeOffTimeMs).DateTime.ToString("HH:mm");
            Int64 landingTimeMs = binaryReader.ReadInt64();
            string landingTime = DateTimeOffset.FromUnixTimeMilliseconds(landingTimeMs).DateTime.ToString("HH:mm");
            UInt64 planeID = binaryReader.ReadUInt64();
            UInt16 crewCount = binaryReader.ReadUInt16();
            UInt64 [] crew = new UInt64[crewCount];
            for(int i=0; i<crewCount; i++)
            {
                crew[i] = binaryReader.ReadUInt64();
            }
            UInt16 loadCount = binaryReader.ReadUInt16();
            UInt64[] load = new UInt64[loadCount];
            for(int i=0; i<loadCount; i++)
            {
                load[i] = binaryReader.ReadUInt64();
            }
            return new Flight(type, ID, originID, targetID,
                takeOffTime, landingTime, planeID, crew, load);
        }

    }

}
