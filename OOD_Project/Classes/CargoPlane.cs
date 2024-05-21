using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class CargoPlane : Plane, IReportable, IGetFields
    {
        [JsonInclude]
        public Single maxLoad { get; set; }

        public CargoPlane(string type, UInt64 iD, string serial, string country, string model, Single maxLoad) : base(type, iD, serial, country, model)
        {
            this.maxLoad = maxLoad;
        }

        public CargoPlane():base()
        {

        }
        public string Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }

        public static new string[] GetFields()
        {
            string[] ret = ["ID", "type", "serial", "country", "model", "maxLoad"];
            return ret;
        }
        public new string[] GetValues()
        {
            string[] ret = [ID.ToString(), type, serial, country, model, maxLoad.ToString()];
            return ret;
        }
       
    }
}
