using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Collections.ObjectModel;

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

            //string uri = ArcGIS.Desktop.Core.Project.Current.URI;
            //ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format("Project uri {0}", uri));
        }
    }
}
