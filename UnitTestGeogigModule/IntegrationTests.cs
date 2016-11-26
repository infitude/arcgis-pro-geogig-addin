using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using GeogigModule;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace UnitTestGeogigModule
{
    [TestClass]
    public class IntegrationTests
    {
        //http://localhost:8182/repos/repo.json
        [TestMethod]
        public async Task TestImportCommand()
        {
            Server server = new Server("Test");
            Repository repo = new Repository("repo");
            Branch branch = new Branch("branch");
            Node node = new Node();

            repo.Url = "http://localhost.fiddler:8182/repos/repo.json";
            server.Url = "http://localhost.fiddler:8182";
            node.branch = branch;
            branch.repository = repo;
            repo.server = server;

            string transactionId = TransactionCommand.BeginTransaction(repo);

            var x = await ImportCommand.ExecuteImport(node, transactionId);

            TransactionCommand.EndTransaction(repo, transactionId);
        }
    }
}
