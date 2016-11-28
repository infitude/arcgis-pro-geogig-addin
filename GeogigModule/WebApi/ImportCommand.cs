using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public static class ImportCommand
    {
        public static async Task<Boolean> ExecuteImport(Node node, string transactionId)
        {
            TaskResponse task = await PostImport(node, transactionId);
            string status = task.taskResponseType.status;
            while (true)
            {
                switch (status)
                {
                    case "RUNNING":
                        await Task.Delay(1000);
                        status = GetImportUpdate(node, task.taskResponseType.id);
                        break;
                    case "FINISHED":
                        return true;
                    default:
                        return false;
                }
            }
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
                TaskResponse responseObject = JsonConvert.DeserializeObject<TaskResponse>(response);
                return responseObject.taskResponseType.status;
            }
        }


        // POST http://localhost:8182/repos/repo/import.json?interchange=True&format=gpkg&destPath=citytown&authorEmail=pete%40example.com&authorName=Pete&transactionId=ab52b2e9-aecc-4607-a435-18952de3e04f&message=moved+another HTTP/1.1
        // Host: localhost:8182
        // Connection: keep-alive
        // Accept-Encoding: gzip, deflate
        // Accept: */*
        // User-Agent: python-requests/2.11.1
        // Content-Length: 45211
        // Content-Type: multipart/form-data; boundary=c8dac9b1566f4f4d84a7563a6e9d22d7

        //--c8dac9b1566f4f4d84a7563a6e9d22d7
        //Content-Disposition: form-data; name="fileUpload"; filename="citytown.gpkg"
        // SQLite format...
        // --c8dac9b1566f4f4d84a7563a6e9d22d7--


        private static async Task<TaskResponse> PostImport(Node node, string transactionId)
        {
            FileInfo fi = new FileInfo(@"c:\temp\largefile.gpkg");
            var fileBytes = File.ReadAllBytes(fi.FullName);

            StringBuilder request = new StringBuilder();
            request.Append(@"http://localhost.fiddler:8182/repos/repo/import.json?interchange=True&format=gpkg&destPath=citytown&authorEmail=pete%40example.com&authorName=Pete&transactionId=");
            request.Append(transactionId);
            request.Append(@"&message=moved+another&output_format=json");

            using (var client = new HttpClient())
            {                
                using (var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    content.Add(new StreamContent(new MemoryStream(fileBytes)), "fileUpload", "citytown.gpkg");

                    using (
                       var message =
                           await client.PostAsync(request.ToString(), content))
                    {
                        var input = await message.Content.ReadAsStringAsync();
                        TaskResponse responseObject = JsonConvert.DeserializeObject<TaskResponse>(input);
                        return responseObject;
                    }
                }
            }
        }
    }    
}
