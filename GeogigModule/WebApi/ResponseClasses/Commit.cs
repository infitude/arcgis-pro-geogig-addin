using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Commit
    {
        [JsonPropertyAttribute(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

        [JsonPropertyAttribute(PropertyName = "tree", NullValueHandling = NullValueHandling.Ignore)]
        public string tree { get; set; }

        [JsonPropertyAttribute(PropertyName = "parents", NullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(ParentsConverter))]
        public Parents parents { get; set; }

        [JsonPropertyAttribute(PropertyName = "author", NullValueHandling = NullValueHandling.Ignore)]
        public User author { get; set; }

        [JsonPropertyAttribute(PropertyName = "committer", NullValueHandling = NullValueHandling.Ignore)]
        public User committer { get; set; }

        [JsonPropertyAttribute(PropertyName = "adds", NullValueHandling = NullValueHandling.Ignore)]
        public int adds { get; set; }

        [JsonPropertyAttribute(PropertyName = "modifies", NullValueHandling = NullValueHandling.Ignore)]
        public int modifies { get; set; }

        [JsonPropertyAttribute(PropertyName = "removes", NullValueHandling = NullValueHandling.Ignore)]
        public int removes { get; set; }

        [JsonPropertyAttribute(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }

    }
}
