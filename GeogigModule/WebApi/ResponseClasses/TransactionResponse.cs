using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class TransactionResponse
    {
        [JsonPropertyAttribute(PropertyName = "response", NullValueHandling = NullValueHandling.Ignore)]
        public TransactionResponseType transactionResponseType { get; set; }
    }
}
