using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public static class BranchCommand
    {
        public static string GetRequest(Repository repository)
        {
            return repository.Url.Replace(".json", @"/branch?output_format=json&list=True");
        }
    }


    /// <summary>
    /// Branch command JSON response classes
    /// </summary>
    public class BranchResponse
    {
        [JsonPropertyAttribute(PropertyName = "response", NullValueHandling = NullValueHandling.Ignore)]
        public BranchResponseType branchResponseType { get; set; }
    }

    public class BranchResponseType
    {
        [JsonPropertyAttribute(PropertyName = "success", NullValueHandling = NullValueHandling.Ignore)]
        public bool success { get; set; }

        [JsonPropertyAttribute(PropertyName = "Local", NullValueHandling = NullValueHandling.Ignore)]
        public BranchResponseLocal local { get; set; }
    }

    public class BranchResponseLocal
    {
        [JsonPropertyAttribute(PropertyName = "Branch", NullValueHandling = NullValueHandling.Ignore)]
        public Branch branch { get; set; }
    }



}
