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
    public static class ReposCommand
    {
        public static string GetRequest(Server server)
        {
            return server.Url + @"/repos";
        }

        public static Repository[] GetRepositories(Server server)
        {
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
                foreach (var repository in repos.repo.repositories)
                {
                    repository.server = server;
                }
            }
            return repos.repo.repositories;
        }

    }
    /// <summary>
    /// These two classes are tempoary 'shims' for the /repos response from geogig - suspect the response should be an array
    /// but will need to check....
    /// </summary>
    public class repos
    {
        [JsonPropertyAttribute(PropertyName = "repos", NullValueHandling = NullValueHandling.Ignore)]
        public static repo repo { get; set; }
    }

    public class repo
    {
        [JsonPropertyAttribute(PropertyName = "repo", NullValueHandling = NullValueHandling.Ignore)]
        public Repository[] repositories { get; set; }
    }


}
