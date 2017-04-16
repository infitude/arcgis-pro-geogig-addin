using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;
using System.Xml;
using ArcGIS.Desktop.Core.Events;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Threading.Tasks;

using ArcGIS.Desktop.Internal.Core;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Core.Data;
using System.IO;
using System.Threading;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Dialogs;

namespace GeogigModule
{
    internal class GeogigModuleSingleton : Module
    {
        private static GeogigModuleSingleton _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static GeogigModuleSingleton Current
        {
            get
            {
                return _this ?? (_this = (GeogigModuleSingleton)FrameworkApplication.FindModule("GeogigModule_Module"));
            }
        }

        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //return false to ~cancel~ Application close
            return true;
        }

        private bool _isImporting = false;

        internal static async void OnGeogigLayerSyncButtonClick()
        {
            if (GeogigModuleSingleton.Current._isImporting)
            {
                return;
            }
            GeogigModuleSingleton.Current._isImporting = true;
            // copy from geodb to geopackage adds, edits and deletes
            var projGDBPath = Project.Current.DefaultGeodatabasePath;
            var bCopied = await CopyFeaturesFromGeopackage(projGDBPath, "citytown", @"C:\Temp\largefile.gpkg", "main.citytown");

            // import back into local geogig - find node in view model for this featureset...
            GeogigDockpaneViewModel vm = FrameworkApplication.DockPaneManager.Find(GeogigDockpaneViewModel.DockPaneID) as GeogigDockpaneViewModel;
            if (vm == null)
                return;
            Node node = vm.FindNode("citytown");
            Repository repo = node.branch.repository;
            string transactionId = TransactionCommand.BeginTransaction(repo);
            var x = await ImportCommand.ExecuteImport(node, transactionId);
            TransactionCommand.EndTransaction(repo, transactionId);
            GeogigModuleSingleton.Current._isImporting = false;
            if (bCopied) MessageBox.Show("Features sync back to Geogig");
        }


        // hints on coding from https://geonet.esri.com/thread/173588
        public async static Task<bool> CopyFeaturesFromGeopackage(string geopackagePath, string geopackageName, string fileGdbPath, string fileGdbName)
        {
            try
            {
                return await ArcGIS.Desktop.Framework.Threading.Tasks.QueuedTask.Run(() =>
                {
                    var fGdbFeatureClass = Path.Combine( fileGdbPath, fileGdbName);
                    var GeoPkgFeatureClass = Path.Combine(geopackagePath, geopackageName);
                    var fGdbName = fileGdbName;
                    System.Diagnostics.Debug.WriteLine($@"copyFeatures {GeoPkgFeatureClass} {fGdbFeatureClass}");
                    var parameters = Geoprocessing.MakeValueArray(GeoPkgFeatureClass, fGdbFeatureClass);
                    var cts = new CancellationTokenSource();
                    var results = Geoprocessing.ExecuteToolAsync("CopyFeatures_management", parameters, null, cts.Token,
                        (eventName, o) =>
                        {
                            System.Diagnostics.Debug.WriteLine($@"GP event: {eventName}");
                        });
                    return true;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //private async static Task<bool> ExecuteAddFileGDB(string fileGdbPath, string fileGdbName)
        //{
        //    try
        //    {
        //        return await ArcGIS.Desktop.Framework.Threading.Tasks.QueuedTask.Run(() =>
        //        {
        //            var fGdbPath = fileGdbPath;
        //            var fGdbName = fileGdbName;
        //            var fGdbVersion = "Current";  // create the 'latest' version of file Geodatabase
        //            System.Diagnostics.Debug.WriteLine($@"create {fGdbPath} {fGdbName}");
        //            var parameters = Geoprocessing.MakeValueArray
        //                (fGdbPath, fGdbName, fGdbVersion);
        //            var cts = new CancellationTokenSource();
        //            var results = Geoprocessing.ExecuteToolAsync("CreateFileGDB_management", parameters, null, cts.Token,
        //                (eventName, o) =>
        //                {
        //                    System.Diagnostics.Debug.WriteLine($@"GP event: {eventName}");
        //                });
        //            return true;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false;
        //    }
        //}

        internal static async void OnGeogigLayerSyncButtonClickNot()
        {
            // get layername
            string layerName = "unknown";
            if (MapView.Active != null)
            {
                // get toc highlighted layers
                var selLayers = MapView.Active.GetSelectedLayers();
                // retrieve the first one - hope its a BasicFeatureLayer
                BasicFeatureLayer layer = (BasicFeatureLayer)selLayers.FirstOrDefault();
                if (layer != null)
                {
                    layerName = layer.Name;
                   
                }
                
            }
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(layerName);
            


            // find nodes for this layer
            GeogigDockpaneViewModel vm = FrameworkApplication.DockPaneManager.Find(GeogigDockpaneViewModel.DockPaneID) as GeogigDockpaneViewModel;
            if (vm == null)
                return;

            //foreach (var server in vm.Servers)
            //{
            //    foreach (var repo in server.Children)
            //    {
            //        foreach (var branch in repo.Children)
            //        {
            //            foreach (var node in branch.Children)
            //            {
            //                    NodeViewModel nvm = (NodeViewModel)node;
            //                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(nvm.PathName);
            //            }
            //        }
            //    }
            //}

            Server server = new Server("Test");
            Repository repo = new Repository("repo");
            Branch branch = new Branch("branch");
            Node node = new Node();

            repo.Url = "http://localhost.fiddler:8182/repos/repo.json";
            server.Url = "http://localhost.fiddler:8182";
            node.branch = branch;
            branch.repository = repo;
            repo.server = server;

            string transactionId = TransactionCommand.BeginTransaction(repo);
            var x = await ImportCommand.ExecuteImport(node, transactionId);
            TransactionCommand.EndTransaction(repo, transactionId);
        }
    }
}
