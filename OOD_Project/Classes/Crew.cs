using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Crew : Person
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
        

    }
}
