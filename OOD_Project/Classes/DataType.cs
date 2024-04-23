using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class DataType
    {
        [JsonInclude]
        public UInt64 ID { get; set; }
        [JsonInclude]
        public string type { get; set; }

        public DataType(UInt64 ID, string type)
        {
            this.ID = ID;
            this.type = type;
        }
        public virtual void Update(IDUpdateArgs args, List<Flight>flightList)
        {
            ID = args.NewObjectID;
        }
        public virtual void Update(PositionUpdateArgs args, List<Flight> flightList = null)
        {
            Logger.NewLog("Update not possible for this type of object");
        }

        public virtual void Update(ContactInfoUpdateArgs args)
        {
            Logger.NewLog("Update not possible for this type of object");
        }


    }
}
