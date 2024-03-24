using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class CargoPlane : Plane
    {
        [JsonInclude]
        public Single maxLoad { get; set; }

        public CargoPlane(string type, UInt64 iD, string serial, string country, string model, Single maxLoad) : base(type, iD, serial, country, model)
        {
            this.maxLoad = maxLoad;
        }
    }
}
