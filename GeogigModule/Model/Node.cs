using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Node
    {
        [JsonPropertyAttribute(PropertyName = "path", NullValueHandling = NullValueHandling.Ignore)]
        public string PathName { get; private set; }

        public Branch branch { get; set; }
    }
}
