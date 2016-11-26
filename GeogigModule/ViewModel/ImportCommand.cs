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

/*

        private static TaskResponseType OldPostImport(Node node, string transactionId)
        {
            FileInfo fi = new FileInfo(@"c:\temp\largefile.gpkg");
            var fileBytes = File.ReadAllBytes(fi.FullName);
            var Params = new Dictionary<string, string> { { "userid", "9" } };
            StringBuilder request = new StringBuilder();
            request.Append(@"http://localhost.fiddler:8182/repos/repo/import.json?interchange=True&format=gpkg&destPath=citytown&authorEmail=pete%40example.com&authorName=Pete&transactionId=");
            request.Append(transactionId);
            request.Append(@"&message = moved + another");
            UploadFilesToServer(new Uri(request.ToString()), Params, Path.GetFileName(fi.FullName), "application/octet-stream", fileBytes);

            TaskResponseType trt = new TaskResponseType();
            trt.status = "RUNNING";
            return trt;
        }


       /// <summary>
       /// Creates HTTP POST request & uploads database to server. Author : Farhan Ghumra
       /// </summary>
       private static void UploadFilesToServer(Uri uri, Dictionary<string, string> data, string fileName, string fileContentType, byte[] fileData)
        {
           string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
           HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
           httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
           httpWebRequest.Method = "POST";
           httpWebRequest.BeginGetRequestStream((result) =>
           {
               try
               {
                   HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                   using (Stream requestStream = request.EndGetRequestStream(result))
                   {
                       WriteMultipartForm(requestStream, boundary, data, fileName, fileContentType, fileData);
                   }
                   request.BeginGetResponse(a =>
                   {
                       try
                       {
                           var response = request.EndGetResponse(a);
                           var responseStream = response.GetResponseStream();
                           using (var sr = new StreamReader(responseStream))
                           {
                               using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                               {
                                   string responseString = streamReader.ReadToEnd();

                                   TaskResponse responseObject = JsonConvert.DeserializeObject<TaskResponse>(responseString);


                                   //responseString is depend upon your web service.
                                   if (responseString == "Success")
                                   {
                                       //MessageBox.Show("Backup stored successfully on server.");
                                   }
                                   else
                                   {
                                       throw new System.ApplicationException("Error");
                                   } 
                               }
                           }
                       }
                       catch (Exception)
                       {

                       }
                   }, null);
               }
               catch (Exception)
               {
                   throw new System.ApplicationException("Couldn't write to request stream");
               }
           }, httpWebRequest);
        }

        /// <summary>
        /// Writes multi part HTTP POST request. Author : Farhan Ghumra
        /// </summary>
        private static void WriteMultipartForm(Stream s, string boundary, Dictionary<string, string> data, string fileName, string fileContentType, byte[] fileData)
       {
           /// The first boundary
           byte[] boundarybytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
           /// the last boundary.
           byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
           /// the form data, properly formatted
           string formdataTemplate = "Content-Dis-data; name=\"{0}\"\r\n\r\n{1}";
           /// the form-data file upload, properly formatted
           string fileheaderTemplate = "Content-Dis-data; name=\"{0}\"; filename=\"{1}\";\r\nContent-Type: {2}\r\n\r\n";

           /// Added to track if we need a CRLF or not.
           bool bNeedsCRLF = false;

           if (data != null)
           {
               foreach (string key in data.Keys)
               {
                   /// if we need to drop a CRLF, do that.
                   if (bNeedsCRLF)
                       WriteToStream(s, "\r\n");

                   /// Write the boundary.
                   WriteToStream(s, boundarybytes);

                   /// Write the key.
                   WriteToStream(s, string.Format(formdataTemplate, key, data[key]));
                   bNeedsCRLF = true;
               }
           }

           /// If we don't have keys, we don't need a crlf.
           if (bNeedsCRLF)
               WriteToStream(s, "\r\n");

           WriteToStream(s, boundarybytes);
           WriteToStream(s, string.Format(fileheaderTemplate, "file", fileName, fileContentType));
           /// Write the file data to the stream.
           WriteToStream(s, fileData);
           WriteToStream(s, trailer);
       }

       /// <summary>
       /// Writes string to stream. Author : Farhan Ghumra
       /// </summary>
       private static void WriteToStream(Stream s, string txt)
       {
           byte[] bytes = Encoding.UTF8.GetBytes(txt);
           s.Write(bytes, 0, bytes.Length);
       }

       /// <summary>
       /// Writes byte array to stream. Author : Farhan Ghumra
       /// </summary>
       private static void WriteToStream(Stream s, byte[] bytes)
       {
           s.Write(bytes, 0, bytes.Length);
       }

       ///// <summary>
       ///// Returns byte array from StorageFile. Author : Farhan Ghumra
       ///// </summary>
       //private static async Task<byte[]> GetBytesAsync(StorageFile file)
       //{
       //    byte[] fileBytes = null;
       //    using (var stream = await file.OpenReadAsync())
       //    {
       //        fileBytes = new byte[stream.Size];
       //        using (var reader = new DataReader(stream))
       //        {
       //            await reader.LoadAsync((uint)stream.Size);
       //            reader.ReadBytes(fileBytes);
       //        }
       //    }

       //    return fileBytes;
       //}   
     */
    }    
}
