using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public static class LsTreeCommand
    {
        public static string GetRequest(Branch branch)
        {
            StringBuilder request = new StringBuilder();
            request.Append(branch.repository.server.Url);
            request.Append(@"/repos/");
            request.Append(branch.repository.RepositoryName);
            request.Append("/ls-tree?path=");
            request.Append(branch.BranchName);
            request.Append("&output_format=json&onlyTrees=True");
            return request.ToString();             
        }
    }



    /// <summary>
    /// LsTree command JSON response classes
    /// </summary>
    public class LsTreeResponse
    {
        [JsonPropertyAttribute(PropertyName = "response", NullValueHandling = NullValueHandling.Ignore)]
        public LsTreeResponseType lsTreeResponseType { get; set; }
    }

    public class LsTreeResponseType
    {
        [JsonPropertyAttribute(PropertyName = "success", NullValueHandling = NullValueHandling.Ignore)]
        public bool success { get; set; }

        [JsonPropertyAttribute(PropertyName = "node", NullValueHandling = NullValueHandling.Ignore)]
        public Node node { get; set; }
    }
    
}
