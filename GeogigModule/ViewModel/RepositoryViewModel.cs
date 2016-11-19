using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    class RepositoryViewModel : TreeViewItemViewModel
    {
        readonly Repository _repository;

        public RepositoryViewModel(Repository repository, ServerViewModel parentServer)
            : base(parentServer, true)
        {
            _repository = repository;
        }

        public string RepositoryName
        {
            get { return _repository.RepositoryName; }
        }

        protected override void LoadChildren()
        {
            foreach (Branch branch in Geogig.GetBranches(_repository))
                base.Children.Add(new BranchViewModel(branch, this));
        }
    }
}
