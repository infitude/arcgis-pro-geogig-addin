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
    /// <summary>
    /// https://gist.github.com/isaacabraham/7592405
    /// 
    /// GET http://localhost:8182/repos/repo/export.json?table=citytown&path=citytown&root=99d57538c21c00f89dad4c39b5f6824ce4e697be&interchange=True&format=gpkg HTTP/1.1
    //Host: localhost:8182
    //Connection: keep-alive
    //Accept-Encoding: gzip, deflate
    //Accept: */*
    //User-Agent: python-requests/2.11.1


    //HTTP/1.1 200 OK
    //Date: Sat, 19 Nov 2016 08:50:46 GMT
    //Server: Noelios-Restlet-Engine/1.0..8
    //Content-Type: application/json
    //Vary: Accept-Charset, Accept-Encoding, Accept-Language, Accept
    //Content-Length: 171

    //{"task":{"id":1,"status":"RUNNING","description":"Export to Geopackage database with geogig interchange format extension","href":"http:\/\/localhost:8182\/tasks\/1.json"}}

    /// </summary>
    public static class ExportCommand
    {
 
        public static async Task<Boolean> ExecuteExport(Node node)
        {
            // call export
            string status = GetExport(node);
            while (true)
            {
                switch(status)
                {
                    case "RUNNING":
                        await Task.Delay(1000);
                        status = GetExportUpdate(node);
                        break;
                    case "FINISHED":
                        status = GetExportFile(node);
                        return true;
                    default:
                        return false;
                }
            }
        }


        private static string GetExport(Node node)
        {

            StringBuilder requestUrl = new StringBuilder();
            requestUrl.Append(node.branch.repository.server.Url);
            requestUrl.Append(@"/repos/");
            requestUrl.Append(node.branch.repository.RepositoryName);
            requestUrl.Append("/export.json?table=");
            requestUrl.Append(node.PathName);
            requestUrl.Append("&path=");
            requestUrl.Append(node.PathName);
            requestUrl.Append("&root=");
            requestUrl.Append(node.branch.objectId); 
            requestUrl.Append("&interchange=True&format=gpkg");

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
                string status = responseObject.taskResponseType.status;
                return status;
            }

        }


        private static string GetExportUpdate(Node node)
        {
            return "";
        }

        private static string GetExportFile(Node node)
        {
            return "";
        }

    }




    /// <summary>
    /// Task JSON response classes
    /// </summary>
    public class TaskResponse
    {
        [JsonPropertyAttribute(PropertyName = "task", NullValueHandling = NullValueHandling.Ignore)]
        public TaskResponseType taskResponseType { get; set; }
    }

    public class TaskResponseType
    {
        [JsonPropertyAttribute(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }

        [JsonPropertyAttribute(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }

        [JsonPropertyAttribute(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }

        [JsonPropertyAttribute(PropertyName = "href", NullValueHandling = NullValueHandling.Ignore)]
        public string href { get; set; }

    }


}
