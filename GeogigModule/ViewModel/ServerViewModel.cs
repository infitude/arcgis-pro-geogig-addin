using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    public class ServerViewModel : TreeViewItemViewModel
    {
        readonly Server _server;

        public ServerViewModel(Server server) 
            : base(null, true)
        {
            _server = server;
        }

        public string ServerName
        {
            get { return _server.ServerName; }
        }

        protected override void LoadChildren()
        {
            //foreach (State state in Database.GetStates(_region))
            //    base.Children.Add(new StateViewModel(state, this));
        }
    }
}
