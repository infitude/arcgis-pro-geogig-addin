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
    public static class ImportCommand
    {
        public static async Task<Boolean> ExecuteImport(Node node)
        {
            // call export
            TaskResponseType task = PostImport(node);
            string status = task.status;
            while (true)
            {
                switch (status)
                {
                    case "RUNNING":
                        await Task.Delay(1000);
                        status = GetImportUpdate(node, task.id);
                        break;
                    case "FINISHED":
                        return true;
                    default:
                        return false;
                }
            }
        }

        // POST http://localhost:8182/repos/repo/import.json?interchange=True&format=gpkg&destPath=citytown&authorEmail=pete%40example.com&authorName=Pete&transactionId=ab52b2e9-aecc-4607-a435-18952de3e04f&message=moved+another HTTP/1.1

        private static TaskResponseType PostImport(Node node)
        {
            // TODO post
            return new TaskResponseType();
        }

        private static string GetImportUpdate(Node node, int task)
        {
            StringBuilder requestUrl = new StringBuilder();
            requestUrl.Append(node.branch.repository.server.Url);
            requestUrl.Append(@"/tasks/");
            requestUrl.Append(task.ToString());
            requestUrl.Append(".json");

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "arcgis_pro");
            wc.Headers.Add("Accept", "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            using (StreamReader sr = new StreamReader(wc.OpenRead(requestUrl.ToString()),
                                                      System.Text.Encoding.UTF8, true))
            {
                string response = sr.ReadToEnd();
                //if (ResponseIsError(response))
                //{
                //    //throw
                //    throw new System.ApplicationException(response);
                //}
                TaskResponse responseObject = JsonConvert.DeserializeObject<TaskResponse>(response);
                return responseObject.taskResponseType.status;
            }
        }


    }
}
