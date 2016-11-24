using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Ref
    {
        [JsonPropertyAttribute(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonPropertyAttribute(PropertyName = "objectId", NullValueHandling = NullValueHandling.Ignore)]
        public string objectId { get; set; }
    }
}
