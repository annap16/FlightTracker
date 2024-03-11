using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class Plane : DataType
    {
        [JsonInclude]
        protected string serial { get; set; }
        [JsonInclude]
        protected string country { get; set; }
        [JsonInclude]
        protected string model { get; set; }

        public Plane(string type, UInt64 iD, string serial, string country, string model) : base(iD, type)
        {
            this.serial = serial;
            this.country = country;
            this.model = model;
        }
    }
}
