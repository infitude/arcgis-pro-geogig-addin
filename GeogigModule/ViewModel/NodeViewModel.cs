using ArcGIS.Desktop.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeogigModule
{
    class NodeViewModel : TreeViewItemViewModel
    {
        readonly Node _node;

        public NodeViewModel(Node node, BranchViewModel parentBranch)
            : base(parentBranch, true)
        {
            _executeExportCommand = new RelayCommand(() => ExecuteExport(), () => true);
            _node = node;
        }

        public string PathName
        {
            get { return _node.PathName; }
        }

        private ICommand _executeExportCommand;
        public ICommand ExecuteExportCommand
        {
            get { return _executeExportCommand; }
        }
        private bool _isExporting = false;

        private async Task ExecuteExport()
        {
            if (_isExporting)
            {
                return;
            }
            _isExporting = true;
            var result = await ExportCommand.ExecuteExport(_node);            
            _isExporting = false;
            return;
        }
    }
}
