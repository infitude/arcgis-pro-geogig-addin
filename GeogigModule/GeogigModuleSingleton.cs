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

        internal static void OnGeogigLayerSyncButtonClick()
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Hello");
        }

        internal static bool CanOnGeogigLayerSyncButtonClick
        {
            get { return true;  }
        }
    }
}
