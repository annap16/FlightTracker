using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Passenger : Person, IGetFields
    {
        [JsonInclude]
        public string classFlight { get; set; }
        [JsonInclude]
        public UInt64 miles { get; set; }

        public Passenger(string type, UInt64 iD, string name, UInt64 age, string phone, string email, string classFlight, UInt64 miles) : base(type, iD, name, age, phone, email)
        {
            this.classFlight = classFlight;
            this.miles = miles;
        }

        public Passenger():base()
        {

        }

        public virtual void Update(IDUpdateArgs args, List<Flight> flightList)
        {
            ID = args.NewObjectID;
            foreach (var flight in flightList)
            {
                for (int i = 0; i < flight.loadID.Length; i++)
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
            string[] ret = ["ID", "type", "name", "age", "phone", "email", "classFlight", "miles"];
            return ret;
        }

        public new string[] GetValues()
        {
            string[] ret = [ID.ToString(), type, name, age.ToString(), phone, email, classFlight, miles.ToString()];
            return ret;
        }
    }
}
