using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class User
    {
        [JsonPropertyAttribute(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonPropertyAttribute(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string email { get; set; }

        [JsonPropertyAttribute(PropertyName = "timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 timestamp { get; set; }

        [JsonPropertyAttribute(PropertyName = "timeZoneOffset", NullValueHandling = NullValueHandling.Ignore)]
        public int timeZoneOffset { get; set; }

    }
}
