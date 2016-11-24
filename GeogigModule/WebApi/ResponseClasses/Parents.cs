using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Parents
    {
        [JsonPropertyAttribute(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
    }
}
