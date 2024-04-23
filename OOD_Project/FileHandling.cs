using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class FileReader
    {
        
        public abstract List<DataType>? ReadFile(string filePath, AllLists lists, Publisher publisher);
        public abstract DataType ReadData(byte[] data, AllLists lists, Publisher publisher);

    }
    
    public class FileReaderFTR: FileReader
    {

        protected Dictionary<string, Func<string[], AllLists, DataType>>dictionary;

        public FileReaderFTR()
        {
            dictionary = new Dictionary<string, Func<string[], AllLists, DataType>>()
            {
                { "C", AllLists.AddCrew},
                { "P",   AllLists.AddPassenger},
                { "CA",  AllLists.AddCargo},
                { "CP",  AllLists.AddCargoPlane },
                { "PP", AllLists.AddPassengerPlane },
                { "AI",  AllLists.AddAirport },
                { "FL", AllLists.AddFlight}
            };
        }
        public override List<DataType>? ReadFile(string filePath, AllLists lists, Publisher publisher)
        {
            List<DataType> objectsList = new List<DataType>();
            try
            {
                StreamReader sr = new StreamReader(filePath);
                string? line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    DataType? obj = InterpretLine(line, lists);
                    if(obj != null)
                    {
                        objectsList.Add(obj);
                        publisher.Subscribe(obj);
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

        public DataType? InterpretLine(string? line, AllLists lists)
        {
            if (line == null) return null;
            string[] splitLine = line.Split(',');
            return dictionary[splitLine[0]](splitLine, lists);
        }

        public override DataType ReadData(byte[] data, AllLists lists, Publisher publisher)
        {
            throw new NotImplementedException();
        }

    }

    public class FileReaderBinary : FileReader
    {
        protected Dictionary<string, Func<byte[], AllLists, DataType>> dictionary;

        public FileReaderBinary()
        {

            dictionary = new Dictionary<string, Func<byte[], AllLists, DataType>>()
            {
                { "NCR", AllLists.AddCrew },
                { "NPA", AllLists.AddPassenger },
                { "NCA", AllLists.AddCargo},
                { "NCP", AllLists.AddCargoPlane },
                { "NPP", AllLists.AddPassengerPlane },
                { "NAI", AllLists.AddAirport },
                { "NFL", AllLists.AddFlight }
            };
        }

        public override List<DataType>? ReadFile(string filePath, AllLists lists, Publisher publisher)
        {
            throw new NotImplementedException();
        }

        public override DataType ReadData(byte[] data, AllLists lists, Publisher publisher)
        {
            string type = GetType(data);
            DataType obj = dictionary[type](data, lists);
            publisher.Subscribe(obj);
            return obj;
        }

        public string GetType(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            return new string(binaryReader.ReadChars(3));
        }

    }
}
