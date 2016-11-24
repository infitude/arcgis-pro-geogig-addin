using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class TaskResponseType
    {
        [JsonPropertyAttribute(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }

        [JsonPropertyAttribute(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }

        [JsonPropertyAttribute(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }

        [JsonPropertyAttribute(PropertyName = "href", NullValueHandling = NullValueHandling.Ignore)]
        public string href { get; set; }

    }
}
