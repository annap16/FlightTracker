using NetworkSourceSimulator;
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
        public string serial { get; set; }
        [JsonInclude]
        public string country { get; set; }
        [JsonInclude]
        public string model { get; set; }

        public Plane(string type, UInt64 iD, string serial, string country, string model) : base(iD, type)
        {
            this.serial = serial;
            this.country = country;
            this.model = model;
        }

        public virtual void Update(IDUpdateArgs args, List<Flight> flightList)
        {
            ID = args.NewObjectID;
            foreach (var flight in flightList)
            {
                if (flight.ID == args.ObjectID)
                {
                    flight.ID = ID;
                    
                }
            }
        }
        public override void Update(PositionUpdateArgs args, List<Flight> flightList = null)
        {
            bool success=false;
            foreach(var flight in flightList)
            {
                if(flight.planeID==ID)
                {
                    flight.longitude = args.Longitude;
                    flight.latitude = args.Latitude;
                    flight.AMSL = args.AMSL;
                    flight.prevLatitude = args.Latitude;
                    flight.prevLongitude = args.Longitude;
                    flight.startLatitude = args.Latitude;
                    flight.startLongitude = args.Longitude;
                    flight.startTime = DateTime.Now;
                    success = true;
                }
            }
            if(!success)
            {
                Logger.NewLog("Object (" + ID.ToString() + ") does not exists");
            }
        }


    }
}
