using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Crew : Person, IGetFields
    {
        [JsonInclude]
        public UInt64 practice { get; set; }
        [JsonInclude]
        public string role { get; set; }

        public Crew(string type, UInt64 iD, string name, UInt64 age, string phone, string email, UInt64 practice, string role) : base(type, iD, name, age, phone, email)
        {
            this.practice = practice;
            this.role = role;
        }
        public Crew():base()
        {

        }

        public virtual void Update(IDUpdateArgs args, List<Flight> flightList)
        {
            ID = args.NewObjectID;
            foreach (var flight in flightList)
            {
                for(int i =0; i<flight.crewID.Length;i++)
                {
                    if (flight.crewID[i]==args.ObjectID)
                    {
                        flight.crewID[i] = ID;
                    }
                }
            }
        }

        public static new string[] GetFields()
        {
            string[] ret = ["ID", "type", "name", "age", "phone", "email", "practice", "role"];
            return ret;
        }

        public new string[] GetValues()
        {
            string[] ret = [ID.ToString(), type, name, age.ToString(), phone, email, practice.ToString(), role];
            return ret;
        }



    }
}
