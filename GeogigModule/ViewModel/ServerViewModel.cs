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
            foreach (Repository repository in Geogig.GetRepositories(_server))
                base.Children.Add(new RepositoryViewModel(repository, this));
        }
    }
}
