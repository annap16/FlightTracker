using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Flight : DataType, IGetFields
    {
        [JsonInclude]
        public UInt64 originID { get; set; }
        [JsonInclude]
        public UInt64 targetID { get; set; }
        [JsonInclude]
        public string takeOffTime { get; set; }
        [JsonInclude]
        public string landingTime { get; set; }
        [JsonInclude]
        public Single longitude { get; set; }
        [JsonInclude]
        public Single latitude { get; set; }
        [JsonInclude]
        public Single AMSL { get; set; }
        [JsonInclude]
        public UInt64 planeID { get; set; }
        [JsonInclude]
        public UInt64[] crewID { get; set; }
        [JsonInclude]
        public UInt64[] loadID { get; set; }

        [JsonIgnore]
        public double? prevLatitude { get; set; }
        [JsonIgnore]
        public double? prevLongitude { get; set; }
        [JsonIgnore]
        public double? startLatitude { get; set; }
        [JsonIgnore]
        public double? startLongitude { get; set; }
        [JsonIgnore]
        public DateTime? startTime { get; set; }
        [JsonIgnore]
        public Airport? originAirport { get; set; }
        [JsonIgnore]
        public Airport? targetAirport { get; set; }
        [JsonIgnore]
        public Plane? plane { get; set; }




        public Flight(string type, UInt64 iD, UInt64 originID, UInt64 targetID, string takeOffTime, string landingTime, Single longitude, Single latitude, Single aMSL, UInt64 planeID, UInt64[] crewID, UInt64[] loadID) : base(iD, type)
        {
            this.originID = originID;
            this.targetID = targetID;
            this.takeOffTime = takeOffTime;
            this.landingTime = landingTime;
            this.longitude = longitude;
            this.latitude = latitude;
            AMSL = aMSL;
            this.planeID = planeID;
            this.crewID = crewID[..];
            this.loadID = loadID[..];
            prevLatitude = null;
            prevLongitude = null;
            startLatitude = null;
            startLongitude = null;
            startTime = null;
            
        }

        public Flight(string type, UInt64 iD, UInt64 originID, UInt64 targetID, string takeOffTime, string landingTime, UInt64 planeID, UInt64[] crewID, UInt64[] loadID) : base(iD, type)
        {
            this.originID = originID;
            this.targetID = targetID;
            this.takeOffTime = takeOffTime;
            this.landingTime = landingTime;
            this.planeID = planeID;
            this.crewID = crewID[..];
            this.loadID = loadID[..];
            latitude = Single.NaN;
            longitude = Single.NaN;
            AMSL = Single.NaN;
            prevLatitude = null;
            prevLongitude = null;
        }

        public Flight():base()
        {

        }
        public override void Update(PositionUpdateArgs args, List<Flight>flightList=null)
        {
            longitude = args.Longitude;
            latitude = args.Latitude;
            AMSL = args.AMSL;
            prevLatitude = args.Latitude;
            prevLongitude = args.Longitude;
            startLatitude = args.Latitude;
            startLongitude = args.Longitude;
            startTime = DateTime.Now;
        }

        public static new string[] GetFields()
        {
            string[] ret = ["ID", "type", "originID", "targetID", "takeOffTime", "landingTime", "longitude", "latitude", "AMSL", "planeID", "crewID", "loadID", "plane.ID",
                "plane.firstClassSize", "plane.businessClassSize", "plane.economyClassSize", "plane.maxLoad", "plane.type", "plane.serial", "plane.country", "plane.model",
            "originAirport.ID", "originAirport.longitude", "originAirport.latitude", "originAirport.AMSL", "originAirport.type", "originAirport.name", "originAirport.code",
            "originAirport.country", "targetAirport.ID", "targetAirport.longitude", "targetAirport.latitude", "targetAirport.AMSL", "targetAirport.type", "targetAirport.name",
            "targetAirport.code", "targetAirport.country"];
            return ret;
        }

        public new string[] GetValues()
        {
            List<string> ret = [ID.ToString(), type, originID.ToString(), targetID.ToString(), takeOffTime, landingTime, longitude.ToString(), latitude.ToString(), AMSL.ToString(), planeID.ToString()];
            string crew = "[";
            for(int i=0; i<crewID.Length;i++)
            {
                crew = crew + crewID[i].ToString() + ";";
            }
            crew += "]";
            ret.Add(crew);
            string load = "[";
            for(int i=0; i<loadID.Length;i++)
            {
                load = load + loadID[i].ToString() + ";";
            }
            load+= "]";
            ret.Add(load);
            ret.Add(plane.ID.ToString());
            ret.Add((plane as PassengerPlane)?.firstClassSize.ToString() ?? string.Empty);
            ret.Add((plane as PassengerPlane)?.businessClassSize.ToString() ?? string.Empty);
            ret.Add((plane as PassengerPlane)?.economyClassSize.ToString() ?? string.Empty);
            ret.Add((plane as CargoPlane)?.maxLoad.ToString() ?? string.Empty);
            ret.Add(plane.type);
            ret.Add(plane.serial);
            ret.Add(plane.country);
            ret.Add(plane.model);
            ret.Add(originAirport.ID.ToString());
            ret.Add(originAirport.longitude.ToString());
            ret.Add(originAirport.latitude.ToString());
            ret.Add(originAirport.AMSL.ToString());
            ret.Add(originAirport.type);
            ret.Add(originAirport.name);
            ret.Add(originAirport.code);
            ret.Add(originAirport.country);
            ret.Add(targetAirport.ID.ToString());
            ret.Add(targetAirport.longitude.ToString());
            ret.Add(targetAirport.latitude.ToString());
            ret.Add(targetAirport.AMSL.ToString());
            ret.Add(targetAirport.type);
            ret.Add(targetAirport.name);
            ret.Add(targetAirport.code);
            ret.Add(targetAirport.country);

            return ret.ToArray();
        }
    }
}
