using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Passenger : Person
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
    }
}
