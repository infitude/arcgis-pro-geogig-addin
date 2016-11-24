using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class TaskResponse
    {
        [JsonPropertyAttribute(PropertyName = "task", NullValueHandling = NullValueHandling.Ignore)]
        public TaskResponseType taskResponseType { get; set; }
    }
}
