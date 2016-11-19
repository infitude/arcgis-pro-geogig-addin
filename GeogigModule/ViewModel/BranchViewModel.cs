using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    class BranchViewModel : TreeViewItemViewModel
    {
        readonly Branch _branch;

        public BranchViewModel(Branch branch, RepositoryViewModel parentRepository)
            : base(parentRepository, true)
        {
            _branch = branch;
        }

        public string BranchName
        {
            get { return _branch.BranchName; }
        }

        protected override void LoadChildren()
        {
            foreach (Node node in Geogig.GetNodes( _branch))
                base.Children.Add(new NodeViewModel(node, this));
        }
    }
}
