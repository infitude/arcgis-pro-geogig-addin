using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GeogigModule
{
    /// <summary>
    /// Interaction logic for GeogigDockpaneView.xaml
    /// </summary>
    public partial class GeogigDockpaneView : UserControl
    {
        public GeogigDockpaneView()
        {
            InitializeComponent();

            //Server[] servers = Geogig.GetServers();
            //GeogigViewModel viewModel = new GeogigViewModel(servers);
            //base.DataContext = viewModel;
        }
    }
}
