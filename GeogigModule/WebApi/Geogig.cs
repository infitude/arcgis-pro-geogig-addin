using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    /// <summary>
    /// http://geogig.org/docs/interaction/web-api.html
    /// </summary>
    public static class Geogig
    {
        public static Server[] GetServers()
        {
            GeogigModule.Properties.Settings settings = GeogigModule.Properties.Settings.Default;
            Server[] servers = JsonConvert.DeserializeObject<Server[]>(settings.RepositoriesSetting);
            return servers;
        }


        public static Repository[] GetRepositories(Server server)
        {
            Repository[] repositories;

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "arcgis_pro");
            wc.Headers.Add("Accept", "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            using (StreamReader sr = new StreamReader(wc.OpenRead(ReposCommand.GetRequest(server)),
                                                      System.Text.Encoding.UTF8, true))
            {
                string response = sr.ReadToEnd();
                //if (ResponseIsError(response))
                //{
                //    //throw
                //    throw new System.ApplicationException(response);
                //}
                repos repos = JsonConvert.DeserializeObject<repos>(response);

                repositories = new Repository[] { repos.repo.repository };
            }
            return repositories;
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
                //if (ResponseIsError(response))
                //{
                //    //throw
                //    throw new System.ApplicationException(response);
                //}
                BranchResponse responseObject = JsonConvert.DeserializeObject<BranchResponse>(response);
                Branch branch = responseObject.branchResponseType.local.branch;
                branches = new Branch[] { branch };
            }
            return branches;
        }

        public static Node[] GetNodes(string repositoryName, Branch branch)
        {
            Node[] nodes;

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "arcgis_pro");
            wc.Headers.Add("Accept", "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            string request = LsTreeCommand.GetRequest(repositoryName, branch);
            using (StreamReader sr = new StreamReader(wc.OpenRead(request),
                                                      System.Text.Encoding.UTF8, true))
            {
                string response = sr.ReadToEnd();
                //if (ResponseIsError(response))
                //{
                //    //throw
                //    throw new System.ApplicationException(response);
                //}
                BranchResponse responseObject = JsonConvert.DeserializeObject<BranchResponse>(response);
                //Branch branch = responseObject.branchResponseType.local.branch;
                nodes = new Node[] { };
            }

            return nodes;
        }

    }

}
