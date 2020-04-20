using Newtonsoft.Json;
using System.Collections.Generic;

namespace VSphere.Models.JsonConvert
{
    public class HostConvert
    {
        [JsonProperty("value")]
        public List<HostValue> Value { get; set; }

        public class HostValue
        {
            [JsonProperty("host")]
            public string Host { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("connection_state")]
            public string Connection { get; set; }

            [JsonProperty("power_state")]
            public string Power { get; set; }
        }
    }
}
