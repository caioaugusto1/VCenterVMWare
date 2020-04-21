using Newtonsoft.Json;
using System.Collections.Generic;

namespace VSphere.Models.JsonConvert
{
    public class DataStoreConvert
    {
        [JsonProperty("value")]
        public List<HostValue> Value { get; set; }

        public class HostValue
        {
            [JsonProperty("datastore")]
            public string DataStore { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("free_space")]
            public string FreeSpace { get; set; }

            [JsonProperty("capacity")]
            public string Capacity { get; set; }
        }
    }
}
