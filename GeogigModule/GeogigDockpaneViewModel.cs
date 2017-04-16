using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GeogigModule
{
    internal class GeogigDockpaneViewModel : DockPane
    {
        private const string _dockPaneID = "GeogigModule_GeogigDockpane";
        private const string _menuID = "GeogigModule_GeogigDockpane_Menu";

        public static string DockPaneID { get { return _dockPaneID;  } }

        readonly ReadOnlyCollection<ServerViewModel> _servers;

        protected GeogigDockpaneViewModel() {

            Server[] servers = Geogig.GetServers();
            _servers = new ReadOnlyCollection<ServerViewModel>(
                (from server in servers
                 select new ServerViewModel(server))
                .ToList());
        }

        public ReadOnlyCollection<ServerViewModel> Servers
        {
            get { return _servers; }
        }

        public Node FindNode(string pathName)
        {
            foreach (var server in _servers)
            {
                foreach (var repo in server.Children)
                {
                    foreach (var branch in repo.Children)
                    {
                        foreach (var node in branch.Children)
                        {
                            NodeViewModel nvm = (NodeViewModel)node;
                            if (nvm.PathName == pathName)
                                return nvm.node;
                            //ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(nvm.PathName);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (pane == null)
                return;

            pane.Activate();
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "Geogig";
        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }


        #region Burger Button

        /// <summary>
        /// Tooltip shown when hovering over the burger button.
        /// </summary>
        public string BurgerButtonTooltip
        {
            get { return "Options"; }
        }

        /// <summary>
        /// Menu shown when burger button is clicked.
        /// </summary>
        public System.Windows.Controls.ContextMenu BurgerButtonMenu
        {
            get { return FrameworkApplication.CreateContextMenu(_menuID); }
        }
        #endregion
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class GeogigDockpane_ShowButton : Button
    {
        protected override void OnClick()
        {
            GeogigDockpaneViewModel.Show();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class GeogigDockpane_AddServerButton : Button
    {
        protected override void OnClick()
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Add server");
        }
    }

    /// <summary>
    /// Button implementation for the button on the menu of the burger button.
    /// </summary>
    internal class GeogigDockpane_MenuButton : Button
    {
        protected override void OnClick()
        {
            GeogigDockpaneViewModel vm = FrameworkApplication.DockPaneManager.Find(GeogigDockpaneViewModel.DockPaneID) as GeogigDockpaneViewModel;
            if (vm == null)
                return;

            TreeViewItemViewModel serverTVI = vm.Servers.FirstOrDefault<TreeViewItemViewModel>(i => i.IsSelected);

            foreach (var server in vm.Servers)
            {
                if (server.Children != null)
                {
                    foreach (var repo in server.Children)
                    {
                        if (repo.Children != null)
                        {
                            foreach (var branch in repo.Children)
                            {
                                if (branch.Children != null)
                                {
                                    foreach (var node in branch.Children)
                                    {
                                        if (node.IsSelected)
                                        {
                                            NodeViewModel selectedNVM = (NodeViewModel)node;
                                            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(selectedNVM.PathName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
