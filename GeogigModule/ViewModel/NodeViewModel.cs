using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Dialogs;
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

        public Node node
        {
            get { return _node; }
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
            // export from local geogig
            var result = await ExportCommand.ExecuteExport(_node);
            // copy features to project geodb to allow editing
            var projGDBPath = Project.Current.DefaultGeodatabasePath;
            var bCopied = await GeogigModuleSingleton.CopyFeaturesFromGeopackage(@"C:\Temp\largefile.gpkg", "main.citytown", projGDBPath, "citytown");
            if (bCopied) MessageBox.Show("Features exported from geogig and copied to project geodb.  Add/edit features and then sync back to Geogig...");
      
            _isExporting = false;
            return;
        }
    }
}
