using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class CheckoutResponseType
    {
        [JsonPropertyAttribute(PropertyName = "success", NullValueHandling = NullValueHandling.Ignore)]
        public bool success { get; set; }

        [JsonPropertyAttribute(PropertyName = "OldTarget", NullValueHandling = NullValueHandling.Ignore)]
        public string oldTarget { get; set; }

        [JsonPropertyAttribute(PropertyName = "NewTarget", NullValueHandling = NullValueHandling.Ignore)]
        public string newTarget { get; set; }

    }
}
