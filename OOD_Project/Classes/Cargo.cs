using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Cargo : DataType
    {
        [JsonInclude]
        public Single weight { get; set; }
        [JsonInclude]
        public string code { get; set; }
        [JsonInclude]
        public string description { get; set; }

        public Cargo(string type, UInt64 ID, Single weight, string code, string description) : base(ID, type)
        {
            this.weight = weight;
            this.code = code;
            this.description = description;
        }
    }
}
