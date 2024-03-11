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
        protected UInt64 ID { get; set; }
        [JsonInclude]
        protected string type { get; set; }

        public DataType(UInt64 ID, string type)
        {
            this.ID = ID;
            this.type = type;
        }
    }
}
