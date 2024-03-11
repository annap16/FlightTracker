using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Airport : DataType
    {
        [JsonInclude]
        protected string name { get; set; }
        [JsonInclude]
        protected string code { get; set; }
        [JsonInclude]
        protected Single latitude { get; set; }
        [JsonInclude]
        protected Single longitude { get; set; }
        [JsonInclude]
        protected Single AMSL { get; set; }
        [JsonInclude]
        protected string country { get; set; }

        public Airport(string type, UInt64 iD, string name, string code, Single longitude, Single latitude, Single aMSL, string country) : base(iD, type)
        {
            this.name = name;
            this.code = code;
            this.latitude = latitude;
            this.longitude = longitude;
            AMSL = aMSL;
            this.country = country;
        }
    }
}
