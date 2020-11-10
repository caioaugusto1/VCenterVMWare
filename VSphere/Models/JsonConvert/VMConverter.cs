using Newtonsoft.Json;
using System.Collections.Generic;

namespace VSphere.Models.JsonConvert
{
    public class VMConverter
    {
        [JsonProperty("value")]
        public List<VMValue> Value { get; set; }

        public class VMValue
        {
            [JsonProperty("memory_size_MiB")]
            public string Memory { get; set; }

            [JsonProperty("vm")]
            public string VM { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("power_state")]
            public string Power { get; set; }

            [JsonProperty("cpu_count")]
            public int CPU { get; set; }
        }
    }
}
