using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Result
    {
        [JsonPropertyAttribute(PropertyName = "newCommit", NullValueHandling = NullValueHandling.Ignore)]
        public Commit newCommit { get; set; }

        [JsonPropertyAttribute(PropertyName = "importCommit", NullValueHandling = NullValueHandling.Ignore)]
        public Commit importCommit { get; set; }

        [JsonPropertyAttribute(PropertyName = "NewFeatures", NullValueHandling = NullValueHandling.Ignore)]
        public NewFeatures newFeatures { get; set; }
    }
}
