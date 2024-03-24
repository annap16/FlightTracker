using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class Serialization
    {
        public abstract void Serialize(List<DataType> objectsList, string fileName);
    }

    public class SerializationJSON:Serialization
    {
        public override void Serialize(List<DataType> objectslist, string fileName)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals
            };
            string s = JsonSerializer.Serialize<object[]>(objectslist.ToArray(), jsonSerializerOptions);
            StreamWriter sw2 = new StreamWriter(fileName);
            sw2.Write(s);
            sw2.Close();
        } 
    }

    
}
