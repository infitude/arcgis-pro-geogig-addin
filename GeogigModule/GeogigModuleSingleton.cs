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

        internal static async void OnGeogigLayerSyncButtonClick()
        {
            var bCreated = await ExecuteAddFileGDB(@"c:\temp\test", @"MyNewFileGDB");
            if (bCreated) MessageBox.Show("File GDB Created");
        }


        private async static Task<bool> ExecuteAddFileGDB(string fileGdbPath, string fileGdbName)
        {
            try
            {
                return await ArcGIS.Desktop.Framework.Threading.Tasks.QueuedTask.Run(() =>
                {
                    var fGdbPath = fileGdbPath;
                    var fGdbName = fileGdbName;
                    var fGdbVersion = "Current";  // create the 'latest' version of file Geodatabase
                    System.Diagnostics.Debug.WriteLine($@"create {fGdbPath} {fGdbName}");
                    var parameters = Geoprocessing.MakeValueArray
                        (fGdbPath, fGdbName, fGdbVersion);
                    var cts = new CancellationTokenSource();
                    var results = Geoprocessing.ExecuteToolAsync("management.CreateFileGDB", parameters, null, cts.Token,
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

                    //            var table = layer.GetTable();
                    //            var dataStore = table.GetDatastore();
                    //            var workspaceNameDef = dataStore.GetConnectionString();
                    //            var workspaceName = workspaceNameDef.Split('=')[1];
                    //            var fullSpec = System.IO.Path.Combine(workspaceName, layerName);


                    //                    SQLiteConnectionPath sqLiteConnectionPath = new SQLiteConnectionPath(new Uri("C:\\temp\\geogig\\data\\test.sqlite"));
                    //                  Database database = new Database(sqLiteConnectionPath);

                   

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
