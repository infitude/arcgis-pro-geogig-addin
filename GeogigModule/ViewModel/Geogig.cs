using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public static class Geogig
    {
        public static Server[] GetServers()
        {
            return new Server[]
            {
                new Server("Northeast"),
                new Server("Midwest")
            };
        }
    }
}
