using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Flight : DataType
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
         public double? prevLongitude { get; set; }

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
    }
}
