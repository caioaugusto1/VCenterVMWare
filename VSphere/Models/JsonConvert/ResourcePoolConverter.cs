using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSphere.Models.JsonConvert
{
    public class ResourcePoolConverter
    {
        [JsonProperty("value")]
        public List<ResourcePoolValue> Value { get; set; }

        public class ResourcePoolValue
        {
            [JsonProperty("resource_pool")]
            public string ResourcePool { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

        }
    }
}
