using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public static Branch[] GetBranches(Repository repository)
        {
            Branch[] branches;

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "arcgis_pro");
            wc.Headers.Add("Accept", "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            string request = BranchCommand.GetRequest(repository);
            using (StreamReader sr = new StreamReader(wc.OpenRead(request),
                                                      System.Text.Encoding.UTF8, true))
            {
                string response = sr.ReadToEnd();
                BranchResponse responseObject = JsonConvert.DeserializeObject<BranchResponse>(response);
                if (responseObject.branchResponseType.success == false)
                {
                    throw new System.ApplicationException(response);
                }
                Branch branch = responseObject.branchResponseType.local.branch;
                branch.repository = repository;
                string objectId = RefParseCommand.GetRef(branch);
                branch.objectId = objectId;
                branches = new Branch[] { branch };
            }
            return branches;
        }
    }
}
