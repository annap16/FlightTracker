using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Cargo : DataType, IGetFields
    {
        [JsonInclude]
        public Single weight { get; set; }
        [JsonInclude]
        public string code { get; set; }
        [JsonInclude]
        public string description { get; set; }

        public Cargo(string type, UInt64 ID, Single weight, string code, string description) : base(ID, type)
        {
            this.weight = weight;
            this.code = code;
            this.description = description;
        }

        public Cargo():base()
        {

        }

        public virtual void Update(IDUpdateArgs args, List<Flight> flightList)
        {
            ID = args.NewObjectID;
            foreach(var flight in flightList)
            {
                for(int i=0; i<flight.loadID.Length; i++) 
                {
                    if (flight.loadID[i] == args.ObjectID)
                    {
                        flight.loadID[i] = ID;
                    }
                }
            }
        }

        public static new string[] GetFields()
        {
            string[] ret = ["ID", "type", "weight", "code", "description"];
            return ret;
        }

        public new string[] GetValues()
        {
            string[] ret = [ID.ToString(), type, weight.ToString(), code, description];
            return ret;
        }

    }
}
