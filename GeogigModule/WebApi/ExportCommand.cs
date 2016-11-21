using System;
using System.Collections.Generic;
using System.Linq;
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
        public static string GetRequest(Node node)
        {
            StringBuilder request = new StringBuilder();
            request.Append(node.branch.repository.server.Url);
            request.Append(@"/repos/");
            request.Append(node.branch.repository.RepositoryName);
            request.Append("/export.json?table=");
            request.Append(node.PathName);
            request.Append("&path=");
            request.Append(node.PathName);
            request.Append("&root=");
            request.Append(node.PathName); // root ?
            request.Append("&interchange=True&format=gpkg");

            return request.ToString();
        }


        //public static async Task ExecuteExport()
        //{
        //    var result = await GetExportStatus();
        //}

        public static async Task<Boolean> ExecuteExport()
        {
            // call export

            while(true)
            {
                switch("ExportStatus")
                {
                    case "RUNNING":
                        await Task.Delay(1000);
                        break;
                    case "FINISHED":
                        //download export
                        return true;
                    default:
                        return false;
                }
            }
        }
    }

    /*
      public class ExecuteExport()
      {

            timer(1000)
        }

        onTimer()
        {
            check if finished
            {   
                stop timer
                download
                done - return data
                }
        check if waited too long         
    }

    private async Task<int> WaitForMessages()
    {
        int messageCount = popClient.GetMessageCount();

        while (messageCount == 0)
        {
            await Task.Delay(1000);
            messageCount = popClient.GetMessageCount();
        }

        return messageCount;
    }

    */
}
