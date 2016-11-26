using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Repository
    {
        [JsonPropertyAttribute(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string RepositoryName { get; private set; }


        [JsonPropertyAttribute(PropertyName = "href", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }


        public Repository(string repositoryName)
        {
            this.RepositoryName = repositoryName;
        }
        
        public Server server { get; set; }
    }
}
