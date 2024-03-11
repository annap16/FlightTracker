using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class Person : DataType
    {
        [JsonInclude]
        protected string name { get; set; }
        [JsonInclude]
        protected UInt64 age { get; set; }
        [JsonInclude]
        protected string phone { get; set; }
        [JsonInclude]
        protected string email { get; set; }

        public Person(string type, UInt64 iD, string name, UInt64 age, string phone, string email) : base(iD, type)
        {
            this.name = name;
            this.age = age;
            this.phone = phone;
            this.email = email;
        }
    }
}
