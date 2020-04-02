using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VCenter.Models.VCenterAPIs
{
    public class VM
    {
        [BsonElement("memory_size_MiB")]
        public int Memory_size { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("vm")]
        public string VM_Id { get; set; }

        [BsonElement("power_state")]
        public string Power_State { get; set; }

        [BsonElement("cpu_count")]
        public int CPU_Count { get; set; }
    }
}
