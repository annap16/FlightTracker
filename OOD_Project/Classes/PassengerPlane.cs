using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class PassengerPlane : Plane, IReportable
    {
        [JsonInclude]
        public UInt16 firstClassSize { get; set; }
        [JsonInclude]
        public UInt16 businessClassSize { get; set; }
        [JsonInclude]
        public UInt16 economyClassSize { get; set; }

        public PassengerPlane(string type, UInt64 iD, string serial, string country, string model, UInt16 firstClassSize, UInt16 businessClassSize, UInt16 economyClassSize) : base(type, iD, serial, country, model)
        {
            this.firstClassSize = firstClassSize;
            this.businessClassSize = businessClassSize;
            this.economyClassSize = economyClassSize;
        }
        public string Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
