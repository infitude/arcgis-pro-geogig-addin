using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class Server
    {
        public Server(string serverName)
        {
            this.ServerName = serverName;
        }

        public string ServerName { get; private set; }

        //readonly List<State> _states = new List<State>();
        //public List<State> States
        //{
        //    get { return _states; }
        //}
    }
}
