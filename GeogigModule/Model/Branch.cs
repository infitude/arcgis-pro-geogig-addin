using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Branch
    {
        public Branch(string branchName)
        {
            this.BranchName = branchName;
        }

        [JsonPropertyAttribute(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string BranchName { get;  set; }

        public Repository repository { get; set; }
    }
}
