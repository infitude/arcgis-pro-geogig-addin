using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public static class LsTreeCommand
    {
        public static string GetRequest(string repositoryName, Branch branch)
        {
            return ""; // "http://localhost:8182/repos/repo/ls-tree?path=master&output_format=json&onlyTrees=True";
        }
    }
}
