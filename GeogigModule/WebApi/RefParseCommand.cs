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
    /// GET http://localhost:8182/repos/repo/refparse?output_format=json&name=master HTTP/1.1
    /// 
    /// {"response":{"success":true,"Ref":{"name":"refs\/heads\/master","objectId":"99d57538c21c00f89dad4c39b5f6824ce4e697be"}}}
    ///
    /// </summary>
    public static class RefParseCommand
    {
        public static string GetRef(Branch branch)
        {
            StringBuilder requestUrl = new StringBuilder();
            requestUrl.Append(branch.repository.server.Url);
            requestUrl.Append(@"/repos/");
            requestUrl.Append(branch.repository.RepositoryName);
            requestUrl.Append("/refparse?output_format=json&name=");
            requestUrl.Append(branch.BranchName);
            
            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "arcgis_pro");
            wc.Headers.Add("Accept", "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            using (StreamReader sr = new StreamReader(wc.OpenRead(requestUrl.ToString()),
                                                      System.Text.Encoding.UTF8, true))
            {
                string response = sr.ReadToEnd();
                RefParseResponse responseObject = JsonConvert.DeserializeObject<RefParseResponse>(response);
                if(responseObject.refParseResponseType.success == false)
                {
                    throw new System.ApplicationException(response);
                }
                string objectid = responseObject.refParseResponseType.refobj.objectId;
                return objectid;
            }
        }
    }
}
