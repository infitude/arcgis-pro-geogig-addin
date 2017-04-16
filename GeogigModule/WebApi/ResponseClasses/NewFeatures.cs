using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class NewFeatures
    {
        [JsonPropertyAttribute(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        //public NewFeaturesType type { get; set; }
        public JsonObjectAttribute[] newFeatures { get; set; }
    }
}
