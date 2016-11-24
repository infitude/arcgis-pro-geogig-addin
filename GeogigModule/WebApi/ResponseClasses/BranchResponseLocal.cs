using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class BranchResponseLocal
    {
        [JsonPropertyAttribute(PropertyName = "Branch", NullValueHandling = NullValueHandling.Ignore)]
        public Branch branch { get; set; }
    }
}
