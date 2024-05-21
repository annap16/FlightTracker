using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public interface IGetFields
    {
        public static abstract string[] GetFields();
        public abstract string[] GetValues();

    }
    public abstract class DataType: IGetFields
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

        public DataType()
        {

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

        public static string[] GetFields()
        {
            string[] ret = ["ID", "type"];
            return ret;
        }

        public string[] GetValues()
        {
            string[] ret = [ID.ToString(), type];
            return ret;
        }



    }
}
