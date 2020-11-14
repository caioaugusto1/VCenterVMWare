using MongoDB.Bson.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSphere.Models.JsonConvert
{
    public class VMPost
    {
        [JsonProperty("spec")]
        public SpecConverter Spec { get; set; } = new SpecConverter();

        public class SpecConverter
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("guest_OS")]
            public string Guest_OS { get; set; }

            [JsonProperty("placement")]
            public PlacementConverter Placement { get; set; } = new PlacementConverter();

            [JsonProperty("memory")]
            public MemoryConverter Memory { get; set; } = new MemoryConverter();

            [JsonProperty("cpu")]
            public CPUConverter CPU { get; set; } = new CPUConverter();

            [JsonProperty("cdroms")]
            public List<CdRoomsConverter> CDROMS { get; set; } = new List<CdRoomsConverter>();

            [JsonProperty("disks")]
            public List<DisksConverter> DISKS { get; set; } = new List<DisksConverter>();

            public class PlacementConverter
            {
                [JsonProperty("datastore")]
                public string DataStore { get; set; }

                [JsonProperty("folder")]
                public string Folder { get; set; }

                [JsonProperty("resource_pool")]
                public string Resource_Pool { get; set; }
            }

            public class MemoryConverter
            {
                [JsonProperty("size_MiB")]
                public int Size { get; set; }

                [JsonProperty("hot_add_enabled")]
                public bool Hot_Add_Enabled { get; set; } = true;
            }

            public class CPUConverter
            {
                [JsonProperty("hot_remove_enabled")]
                public bool Hot_Remove_Enabled { get; set; } = true;

                [JsonProperty("count")]
                public int Count { get; set; } = 1;

                [JsonProperty("hot_add_enabled")]
                public bool Hot_Add_Enabled { get; set; } = true;

                [JsonProperty("cores_per_socket")]
                public int Cores_Per_Socket { get; set; } = 1;
            }

            public class CdRoomsConverter
            {
                [JsonProperty("type")]
                public string Type { get; set; }
            }

            public class DisksConverter
            {
                [JsonProperty("new_vmdk")]
                public object New_VMDK { get; set; } = new object();

                [JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("ide")]
                public object IDE { get; set; } = new object();
            }

        }
    }
}
