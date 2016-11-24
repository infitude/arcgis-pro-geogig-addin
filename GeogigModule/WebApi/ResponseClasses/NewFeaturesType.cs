using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class NewFeaturesType
    {
        [JsonPropertyAttribute(PropertyName = "@name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

    }
}
