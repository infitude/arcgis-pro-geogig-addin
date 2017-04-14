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

        public static Node[] GetNodes(Branch branch)
        {
            Node[] nodes;

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "arcgis_pro");
            wc.Headers.Add("Accept", "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            string request = LsTreeCommand.GetRequest(branch);
            using (StreamReader sr = new StreamReader(wc.OpenRead(request),
                                                      System.Text.Encoding.UTF8, true))
            {
                string response = sr.ReadToEnd();
                LsTreeResponse responseObject = JsonConvert.DeserializeObject<LsTreeResponse>(response);
                if (responseObject.lsTreeResponseType.success == false)
                {
                    throw new System.ApplicationException(response);
                }
                Node node = responseObject.lsTreeResponseType.nodes[0];
                node.branch = branch;
                nodes = new Node[] { node };
            }
            return nodes;
        }

    }

}
