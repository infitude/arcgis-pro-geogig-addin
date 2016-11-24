using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Transaction
    {
        [JsonPropertyAttribute(PropertyName = "ID", NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

    }
}
