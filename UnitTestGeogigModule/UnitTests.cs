using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using GeogigModule;
using Newtonsoft.Json.Linq;

namespace UnitTestGeogigModule
{
    /// <summary>
    /// These tests cover the web api responses from the Geogig API.   
    /// </summary>
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

        /// <summary>
        /// {"task":
        ///     {   "id":3,
        ///         "status":"FINISHED",
        ///         "transactionId":"ab52b2e9-aecc-4607-a435-18952de3e04f",
        ///         "description":"Importing GeoPackage database file.",
        ///         "href":"http:\/\/localhost:8182\/tasks\/3.json",
        ///         "result":{
        ///                     "newCommit":{
    ///                             "id":"43d5699ab8c03c4ae4422567c84b627aab2ab903",
    ///                             "tree":"310f126a85dbe8e916af66319e2326b21530570e",
    ///                             "parents":{
    ///                                 "id":"85fa30d101837b22f1e5d375966b3a63bc32248b"},
    ///                             "author":{
    ///                                 "name":"Pete",
    ///                                 "email":"pete@example.com",
    ///                                 "timestamp":1479790440629,
    ///                                 "timeZoneOffset":0},
    ///                             "committer":{
    ///                                 "name":"Pete",
    ///                                 "email":"pete@example.com",
    ///                                 "timestamp":1479790440629,
    ///                                 "timeZoneOffset":0},
    ///                             "message":"moved another"},
        ///                      "importCommit":{
        ///                         "id":"43d5699ab8c03c4ae4422567c84b627aab2ab903",
        ///                         "tree":"310f126a85dbe8e916af66319e2326b21530570e",
        ///                         "parents":{
        ///                             "id":"85fa30d101837b22f1e5d375966b3a63bc32248b"},
        ///                         "author":{
        ///                             "name":"Pete",
        ///                             "email":"pete@example.com",
        ///                             "timestamp":1479790440629,
        ///                             "timeZoneOffset":0},
        ///                         "committer":{
        ///                             "name":"Pete",
        ///                             "email":"pete@example.com",
        ///                             "timestamp":1479790440629,
        ///                             "timeZoneOffset":0},
        ///                         "message":"moved another"},
    ///                         "NewFeatures":{
    ///                             "type":{
    ///                             "@name":"citytown"}}}}}
        /// </summary>
        [TestMethod]
        public void TestDeserializeExport2Json()
        {
            string json = @"{""task"":{""id"":3,""status"":""FINISHED"",""transactionId"":""ab52b2e9-aecc-4607-a435-18952de3e04f"",""description"":""Importing GeoPackage database file."",""href"":""http:\/\/ localhost:8182\/ tasks\/ 3.json"",""result"":{""newCommit"":{""id"":""43d5699ab8c03c4ae4422567c84b627aab2ab903"",""tree"":""310f126a85dbe8e916af66319e2326b21530570e"",""parents"":{""id"":""85fa30d101837b22f1e5d375966b3a63bc32248b""},""author"":{""name"":""Pete"",""email"":""pete @example.com"",""timestamp"":1479790440629,""timeZoneOffset"":0},""committer"":{""name"":""Pete"",""email"":""pete @example.com"",""timestamp"":1479790440629,""timeZoneOffset"":0},""message"":""moved another""},""importCommit"":{""id"":""43d5699ab8c03c4ae4422567c84b627aab2ab903"",""tree"":""310f126a85dbe8e916af66319e2326b21530570e"",""parents"":{""id"":""85fa30d101837b22f1e5d375966b3a63bc32248b""},""author"":{""name"":""Pete"",""email"":""pete @example.com"",""timestamp"":1479790440629,""timeZoneOffset"":0},""committer"":{""name"":""Pete"",""email"":""pete @example.com"",""timestamp"":1479790440629,""timeZoneOffset"":0},""message"":""moved another""},""NewFeatures"":{""type"":{""@name"":""citytown""}}}}}";
            TaskResponse responseObject = JsonConvert.DeserializeObject<TaskResponse>(json);
            Assert.AreEqual(responseObject.taskResponseType.status, "FINISHED");
            Assert.AreEqual(responseObject.taskResponseType.result.newFeatures.type.name, "citytown");
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
            string json = @"{""response"":{""success"":true,""commit"":{""id"":""99d57538c21c00f89dad4c39b5f6824ce4e697be"",""tree"":""310f126a85dbe8e916af66319e2326b21530570e"",""parents"":{""id"":""51a09561bc0700845b75746d8090478529c4ff8a""},""author"":{""name"":""Pete"",""email"":""pete @example.com"",""timestamp"":1479152522539,""timeZoneOffset"":0},""committer"":{""name"":""Pete"",""email"":""pete @example.com"",""timestamp"":1479152522539,""timeZoneOffset"":0},""adds"":0,""modifies"":0,""removes"":0,""message"":""another change""}}}";
            LogResponse responseObject = JsonConvert.DeserializeObject<LogResponse>(json);
            Assert.AreEqual(responseObject.logResponseType.commit.id, "99d57538c21c00f89dad4c39b5f6824ce4e697be");
            Assert.AreEqual(responseObject.logResponseType.commit.author.name, "Pete");

            //GET http://localhost:8182/repos/repo/log?countChanges=True&output_format=json&limit=1&page=1 HTTP/1.1
            json = @"{""response"":{""success"":true}}";
            responseObject = JsonConvert.DeserializeObject<LogResponse>(json);
            Assert.AreEqual(responseObject.logResponseType.success, true);
            Assert.AreEqual(responseObject.logResponseType.commit, null);
        }

        [TestMethod]
        public void TestDeserialiseBeginTransaction()
        {
            //GET http://localhost:8182/repos/repo/beginTransaction?output_format=json HTTP/1.1
            string json = @"{""response"":{""success"":true,""Transaction"":{""ID"":""ab52b2e9-aecc-4607-a435-18952de3e04f""}}}";
            TransactionResponse responseObject = JsonConvert.DeserializeObject<TransactionResponse>(json);
            Assert.AreEqual(responseObject.transactionResponseType.transaction.id, "ab52b2e9-aecc-4607-a435-18952de3e04f");
        }

        [TestMethod]
        public void TestDeserialiseEndTransaction()
        {
            //GET http://localhost:8182/repos/repo/endTransaction?transactionId=ab52b2e9-aecc-4607-a435-18952de3e04f HTTP/1.1
            string json = @"{""response"":{""success"":true,""Transaction"":{""ID"":""ab52b2e9-aecc-4607-a435-18952de3e04f""}}}";
            TransactionResponse responseObject = JsonConvert.DeserializeObject<TransactionResponse>(json);
            Assert.AreEqual(responseObject.transactionResponseType.transaction.id, "ab52b2e9-aecc-4607-a435-18952de3e04f");
        }

        [TestMethod]
        public void TestDeseriliseCheckout()
        {
            //GET http://localhost:8182/repos/repo/checkout?transactionId=ab52b2e9-aecc-4607-a435-18952de3e04f&branch=master HTTP/1.1
            string json = @"{""response"":{""success"":true,""OldTarget"":""refs\/heads\/master"",""NewTarget"":""master""}}";
            CheckoutResponse responseObject = JsonConvert.DeserializeObject<CheckoutResponse>(json);
            Assert.AreEqual(responseObject.checkoutResponseType.newTarget, "master");
        }



        // TODO:

        // POST http://localhost:8182/repos/repo/import.json?interchange=True&format=gpkg&destPath=citytown&authorEmail=pete%40example.com&authorName=Pete&transactionId=ab52b2e9-aecc-4607-a435-18952de3e04f&message=moved+another HTTP/1.1

        // GET http://localhost:8182/repos/repo/export-diff.json?oldRef=43d5699ab8c03c4ae4422567c84b627aab2ab903&newRef=43d5699ab8c03c4ae4422567c84b627aab2ab903&format=gpkg HTTP/1.1

    }
}
