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
    public static class TransactionCommand
    {
        public static string BeginTransaction(Repository repository)
        {
            string request = repository.Url.Replace(".json", @"/beginTransaction?output_format=json");
            return TransactionCommand.GetTransaction(request);
        }

        public static string EndTransaction(Repository repository, string transactionId)
        {
            string parms = @"/endTransaction?transactionId=" + transactionId + "&output_format=json&list=True";
            string request = repository.Url.Replace(".json", parms);
            return TransactionCommand.GetTransaction(request);
        }

        private static string GetTransaction(string request)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "arcgis_pro");
            wc.Headers.Add("Accept", "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            string transactionId;
            using (StreamReader sr = new StreamReader(wc.OpenRead(request),
                                                      System.Text.Encoding.UTF8, true))
            {
                string response = sr.ReadToEnd();
                TransactionResponse responseObject = JsonConvert.DeserializeObject<TransactionResponse>(response);
                if (responseObject.transactionResponseType.success == false)
                {
                    throw new System.ApplicationException(response);
                }
                transactionId = responseObject.transactionResponseType.transaction.id;
            }
            return transactionId;
        }
    }
}
