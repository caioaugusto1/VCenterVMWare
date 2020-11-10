using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSphere.Models.JsonConvert
{
    public class FolderConverter
    {
        [JsonProperty("value")]
        public List<FolderValue> Value { get; set; }

        public class FolderValue
        {
            [JsonProperty("folder")]
            public string Folder { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

        }
    }
}
