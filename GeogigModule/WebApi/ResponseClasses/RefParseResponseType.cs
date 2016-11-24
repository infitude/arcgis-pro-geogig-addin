using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class RefParseResponseType
    {
        [JsonPropertyAttribute(PropertyName = "success", NullValueHandling = NullValueHandling.Ignore)]
        public bool success { get; set; }

        [JsonPropertyAttribute(PropertyName = "Ref", NullValueHandling = NullValueHandling.Ignore)]
        public Ref refobj { get; set; }
    }
}
