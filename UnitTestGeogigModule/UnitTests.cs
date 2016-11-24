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
            // GET http://localhost:8182/repos/repo/branch?output_format=json&list=True HTTP/1.1
            string json = @"{ ""response"":{ ""success"":true,""Local"":{ ""Branch"":{ ""name"":""master""} },""Remote"":""""} }";
            BranchResponse responseObject  = JsonConvert.DeserializeObject<BranchResponse>(json);
            Assert.AreEqual(responseObject.branchResponseType.success, true);
            Assert.AreEqual(responseObject.branchResponseType.local.branch.BranchName, "master");
        }

        [TestMethod]
        public void TestDeserializeExportJson()
        {
            // GET http://localhost:8182/repos/repo/export.json?table=citytown&path=citytown&root=99d57538c21c00f89dad4c39b5f6824ce4e697be&interchange=True&format=gpkg HTTP/1.1
            string json = @"{""task"":{""id"":1,""status"":""RUNNING"",""description"":""Export to Geopackage database with geogig interchange format extension"",""href"":""http:\/\/ localhost:8182\/ tasks\/ 1.json""}}";
            TaskResponse responseObject = JsonConvert.DeserializeObject<TaskResponse>(json);
            Assert.AreEqual(responseObject.taskResponseType.status, "RUNNING");
        }

        [TestMethod]
        public void TestDeserializeLsTreeJson()
        {
            // GET http://localhost:8182/repos/repo/ls-tree?path=HEAD&output_format=json&onlyTrees=True HTTP/1.1
            string json = @"{""response"":{""success"":true,""node"":{""path"":""citytown""}}}";
            LsTreeResponse responseObject = JsonConvert.DeserializeObject<LsTreeResponse>(json);
            Assert.AreEqual(responseObject.lsTreeResponseType.node.PathName, "citytown");           
        }

        [TestMethod]
        public void TestDeserialiseRefParseJson()
        {
            // GET http://localhost:8182/repos/repo/refparse?output_format=json&name=master HTTP/1.1
            string json = @"{""response"":{""success"":true,""Ref"":{""name"":""refs\/heads\/master"",""objectId"":""99d57538c21c00f89dad4c39b5f6824ce4e697be""}}}";
            RefParseResponse responseObject = JsonConvert.DeserializeObject<RefParseResponse>(json);
            Assert.AreEqual(responseObject.refParseResponseType.refobj.objectId, "99d57538c21c00f89dad4c39b5f6824ce4e697be");
        }

        [TestMethod]
        public void TestDeserialiseLogJson()
        {
            //GET http://localhost:8182/repos/repo/log?countChanges=True&output_format=json&limit=1&page=0 HTTP/1.1
            string json = @"{""response"":{""success"":true,""commit"":{""id"":""99d57538c21c00f89dad4c39b5f6824ce4e697be"",""tree"":""310f126a85dbe8e916af66319e2326b21530570e"",""parents"":{""id"":""51a09561bc0700845b75746d8090478529c4ff8a""},""author"":{""name"":""Pete"",""email"":""pete @e-spatial.co.nz"",""timestamp"":1479152522539,""timeZoneOffset"":0},""committer"":{""name"":""Pete"",""email"":""pete @e-spatial.co.nz"",""timestamp"":1479152522539,""timeZoneOffset"":0},""adds"":0,""modifies"":0,""removes"":0,""message"":""another change""}}}";
            LogResponse responseObject = JsonConvert.DeserializeObject<LogResponse>(json);
            Assert.AreEqual(responseObject.logResponseType.commit.id, "99d57538c21c00f89dad4c39b5f6824ce4e697be");
            Assert.AreEqual(responseObject.logResponseType.commit.author.name, "Pete");
        }   

    }
}
