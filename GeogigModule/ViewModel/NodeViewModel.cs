using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeogigModule
{
    class NodeViewModel : TreeViewItemViewModel
    {
        readonly Node _node;

        public NodeViewModel(Node node, BranchViewModel parentBranch)
            : base(parentBranch, true)
        {
            _node = node;
        }

        public string PathName
        {
            get { return _node.PathName; }
        }
       
    }
}
