using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using GeogigModule;
using Newtonsoft.Json.Linq;

namespace UnitTestGeogigModule
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestDeserializeBranchJson()
        {
            string json = @"{ ""response"":{ ""success"":true,""Local"":{ ""Branch"":{ ""name"":""master""} },""Remote"":""""} }";
            BranchResponse responseObject  = JsonConvert.DeserializeObject<BranchResponse>(json);

            Branch branch = responseObject.branchResponseType.local.branch;

        }
    }

    public class BranchResponse
    {
        [JsonPropertyAttribute(PropertyName = "response", NullValueHandling = NullValueHandling.Ignore)]
        public BranchResponseType branchResponseType { get; set; }
    }

    public class BranchResponseType
    {
        [JsonPropertyAttribute(PropertyName = "success", NullValueHandling = NullValueHandling.Ignore)]
        public bool success { get; set; }

        [JsonPropertyAttribute(PropertyName = "Local", NullValueHandling = NullValueHandling.Ignore)]
        public BranchResponseLocal local { get; set; }

    }

    public class BranchResponseLocal
    {
        [JsonPropertyAttribute(PropertyName = "Branch", NullValueHandling = NullValueHandling.Ignore)]
        public Branch branch { get; set; }

    }
}
