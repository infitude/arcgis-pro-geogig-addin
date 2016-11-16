using System.Collections.ObjectModel;
using System.Linq;

namespace GeogigModule
{
    public class GeogigViewModel
    {
        readonly ReadOnlyCollection<ServerViewModel> _servers;

        public GeogigViewModel(Server[] servers)
        {
            _servers = new ReadOnlyCollection<ServerViewModel>(
                (from server in servers
                 select new ServerViewModel(server))
                .ToList());
        }

        public ReadOnlyCollection<ServerViewModel> Servers
        {
            get { return _servers; }
        }
    }
}
