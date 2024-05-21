using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Airport : DataType, IReportable, IGetFields
    {
        [JsonInclude]
        public string name { get; set; }
        [JsonInclude]
        public string code { get; set; }
        [JsonInclude]
        public Single latitude { get; set; }
        [JsonInclude]
        public Single longitude { get; set; }
        [JsonInclude]
        public Single AMSL { get; set; }
        [JsonInclude]
        public string country { get; set; }

        public Airport(string type, UInt64 iD, string name, string code, Single longitude, Single latitude, Single aMSL, string country) : base(iD, type)
        {
            this.name = name;
            this.code = code;
            this.latitude = latitude;
            this.longitude = longitude;
            AMSL = aMSL;
            this.country = country;
        }
        public Airport():base()
        {

        }
        public override void Update(IDUpdateArgs args, List<Flight> flightList)
        {
            ID = args.NewObjectID;
            foreach(var flight in flightList)
            {
                if(flight.originID==args.ObjectID)
                {
                    flight.originID = ID;
                }
                if(flight.targetID == args.ObjectID)
                {
                    flight.targetID = ID;
                }
            }
        }

        public string Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }

        public static new string[]GetFields()
        {
            string[] ret = ["ID", "type", "name", "code", "latitude", "longitude", "AMSL", "country"];
            return ret;
        }
        public new string[] GetValues()
        {
            string[] ret = [ID.ToString(), type.ToString(), name, code, latitude.ToString(), longitude.ToString(), AMSL.ToString(), country];
            return ret;
        }
    }
}
