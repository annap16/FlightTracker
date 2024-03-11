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
        protected UInt64 originID { get; set; }
        [JsonInclude]
        protected UInt64 targetID { get; set; }
        [JsonInclude]
        protected string takeOffTime { get; set; }
        [JsonInclude]
        protected string landingTime { get; set; }
        [JsonInclude]
        protected Single longitude { get; set; }
        [JsonInclude]
        protected Single latitude { get; set; }
        [JsonInclude]
        protected Single AMSL { get; set; }
        [JsonInclude]
        protected UInt64 planeID { get; set; }
        [JsonInclude]
        protected UInt64[] crewID { get; set; }
        [JsonInclude]
        protected UInt64[] loadID { get; set; }

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
            this.latitude = Single.NaN;
            this.longitude = Single.NaN;
            this.AMSL = Single.NaN;
        }
    }
}
