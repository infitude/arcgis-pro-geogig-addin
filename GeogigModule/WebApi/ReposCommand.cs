using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public static class ReposCommand
    {
        public static string GetRequest(Server server)
        {
            return server.Url + @"/repos";
        }
    }

    /// <summary>
    /// These two classes are tempoary 'shims' for the /repos response from geogig - suspect the response should be an array
    /// but will need to check....
    /// </summary>
    public class repos
    {
        [JsonPropertyAttribute(PropertyName = "repos", NullValueHandling = NullValueHandling.Ignore)]
        public repo repo { get; set; }
    }

    public class repo
    {
        [JsonPropertyAttribute(PropertyName = "repo", NullValueHandling = NullValueHandling.Ignore)]
        public Repository repository { get; set; }
    }


}
