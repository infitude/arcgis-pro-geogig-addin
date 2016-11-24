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
            Repository[] repositories = ReposCommand.GetRepositories(server);
            return repositories;
        }
        
        public static Branch[] GetBranches(Repository repository)
        {
            Branch[] branches = BranchCommand.GetBranches(repository);            
            return branches;
        }

        public static Node[] GetNodes(Branch branch)
        {
            Node[] nodes = LsTreeCommand.GetNodes(branch);
            return nodes;
        }
    }
}
