using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class FileReader
    {
        
        public abstract List<DataType>? ReadFile(string filePath);
        public abstract DataType ReadData(byte[] data);

    }
    
    public class FileReaderFTR: FileReader
    {

        protected Dictionary<string, DataTypeFactory> dictionary;

        public FileReaderFTR()
        {

            dictionary = new Dictionary<string, DataTypeFactory>()
            {
                { "C", new CrewFactory() },
                { "P", new PassengerFactory() },
                { "CA", new CargoFactory()},
                { "CP", new CargoPlaneFactory() },
                { "PP", new PassengerPlaneFactory() },
                { "AI", new AirportFactory() },
                { "FL", new FlightFactory() }
            };
        }
        public override List<DataType>? ReadFile(string filePath)
        {
            List<DataType> objectsList = new List<DataType>();
            try
            {
                StreamReader sr = new StreamReader(filePath);
                string? line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    DataType? obj = InterpretLine(line);
                    if(obj != null)
                    {
                        objectsList.Add(obj);
                    }
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception - file reading: " + ex.Message);
                return null;
            }
            return objectsList;
        }

        public DataType? InterpretLine(string? line)
        {
            if (line == null) return null;
            string[] splitLine = line.Split(',');
            return dictionary[splitLine[0]].Create(splitLine);
        }

        public override DataType ReadData(byte[] data)
        {
            throw new NotImplementedException();
        }

    }

    public class FileReaderBinary : FileReader
    {
        protected Dictionary<string, DataTypeFactory> dictionary;

        public FileReaderBinary()
        {

            dictionary = new Dictionary<string, DataTypeFactory>()
            {
                { "NCR", new CrewFactory() },
                { "NPA", new PassengerFactory() },
                { "NCA", new CargoFactory()},
                { "NCP", new CargoPlaneFactory() },
                { "NPP", new PassengerPlaneFactory() },
                { "NAI", new AirportFactory() },
                { "NFL", new FlightFactory() }
            };
        }

        public override List<DataType>? ReadFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public override DataType ReadData(byte[] data)
        {
            string type = GetType(data);
            return dictionary[type].Create(data);
        }

        public string GetType(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            return new string(binaryReader.ReadChars(3));
        }

    }
}
