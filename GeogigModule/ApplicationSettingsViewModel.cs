using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;


namespace GeogigModule
{
    internal class ApplicationSettingsViewModel : Page
    {
        public ApplicationSettingsViewModel() { }

        #region Properties

        private string _origUserNameSetting;

        /// <summary>
        /// Gets and sets the GIT user name setting
        /// </summary>
        /// <remarks>
        /// Use the base.IsModified flag to indicate when the page has changed
        /// </remarks>
        private string _UserNameSetting;
        public string UserNameSetting
        {
            get { return _UserNameSetting; }
            set
            {
                if (SetProperty(ref _UserNameSetting, value, () => UserNameSetting))
                    base.IsModified = true;
            }
        }

        private string _origUserEmailSetting;

        /// <summary>
        /// Gets and sets the GIT user email setting
        /// </summary>
        /// <remarks>
        /// Use the base.IsModified flag to indicate when the page has changed
        /// </remarks>
        private string _UserEmailSetting;
        public string UserEmailSetting
        {
            get { return _UserEmailSetting; }
            set
            {
                if (SetProperty(ref _UserEmailSetting, value, () => UserEmailSetting))
                    base.IsModified = true;
            }
        }

        private string _origRepositoriesSetting;

        /// <summary>
        /// Gets and sets the GIT user email setting
        /// </summary>
        /// <remarks>
        /// Use the base.IsModified flag to indicate when the page has changed
        /// </remarks>
        private string _RepositoriesSetting;
        public string RepositoriesSetting
        {
            get { return _RepositoriesSetting; }
            set
            {
                if (SetProperty(ref _RepositoriesSetting, value, () => RepositoriesSetting))
                    base.IsModified = true;
            }
        }
        
        private string _origTrackedLayersSetting;

        /// <summary>
        /// Gets and sets the TrackedLayersSetting setting
        /// </summary>
        /// <remarks>
        /// Use the base.IsModified flag to indicate when the page has changed
        /// </remarks>
        private string _TrackedLayersSetting;
        public string TrackedLayersSetting
        {
            get { return _TrackedLayersSetting; }
            set
            {
                if (SetProperty(ref _TrackedLayersSetting, value, () => TrackedLayersSetting))
                    base.IsModified = true;
            }
        }

        
        private string _origBaseDataFolderSetting;

        /// <summary>
        /// Gets and sets the TrackedLayersSetting setting
        /// </summary>
        /// <remarks>
        /// Use the base.IsModified flag to indicate when the page has changed
        /// </remarks>
        private string _BaseDataFolderSetting;
        public string BaseDataFolderSetting
        {
            get { return _BaseDataFolderSetting; }
            set
            {
                if (SetProperty(ref _BaseDataFolderSetting, value, () => BaseDataFolderSetting))
                    base.IsModified = true;
            }
        }

        #endregion

        #region Page Overrides

        /// <summary>
        /// Initializes the page using the settings.
        /// </summary>
        /// <returns>A Task that represnets the InitializeAsync method</returns>
        protected override Task InitializeAsync()
        {
            // get the default settings
            GeogigModule.Properties.Settings settings = GeogigModule.Properties.Settings.Default;

            // assign to the values binding to the controls
            _UserNameSetting = settings.UserNameSetting;
            _UserEmailSetting = settings.UserEmailSetting;
            _RepositoriesSetting = settings.RepositoriesSetting;
            _TrackedLayersSetting = settings.TrackedLayersSetting;
            _BaseDataFolderSetting = settings.BaseDataFolderSetting;

            //// keep track of the original values (used for comparison when saving)
            _origUserNameSetting = UserNameSetting;
            _origUserEmailSetting = UserEmailSetting;
            _origRepositoriesSetting = RepositoriesSetting;
            _origTrackedLayersSetting = TrackedLayersSetting;
            _origBaseDataFolderSetting = BaseDataFolderSetting;

            return Task.FromResult(0);
        }

        /// <summary>
        /// Perform special actions when the page is to be cancelled.
        /// </summary>
        /// <returns><A Task that represents CancelAsync/returns>
        protected override Task CancelAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Perform special actions when the page is to be committed.
        /// </summary>
        /// <remarks>
        /// Stores the current state of the settings.  Ensure that the project is set dirty if the settings have changed from the original values. 
        /// Setting the project dirty ensure that the application asks to "save changes" when the project is closed.  The settings will be
        /// saved when the project is saved.
        /// </remarks>
        /// <returns>A Task that represents CommitAsync</returns>
        protected override Task CommitAsync()
        {
            if (IsDirty())
            {
                // save the new settings
                GeogigModule.Properties.Settings settings = GeogigModule.Properties.Settings.Default;

                settings.UserNameSetting = UserNameSetting;
                settings.UserEmailSetting = UserEmailSetting;
                settings.RepositoriesSetting = RepositoriesSetting;
                settings.TrackedLayersSetting = TrackedLayersSetting;
                settings.BaseDataFolderSetting = BaseDataFolderSetting;

                settings.Save();
            }

            return Task.FromResult(0);
        }

        #endregion

        /// <summary>
        /// Determines if the current settings are different from the original.
        /// </summary>
        /// <returns>true if the current settings are different</returns>
        private bool IsDirty()
        {

            if (_origUserNameSetting != UserNameSetting)
                return true;

            if (_origUserEmailSetting != UserEmailSetting)
                return true;

            if (_origRepositoriesSetting != RepositoriesSetting)
                return true;

            if(_origTrackedLayersSetting != TrackedLayersSetting)
                return true;

            if(_origBaseDataFolderSetting != BaseDataFolderSetting)
                return true;

            return false;
        }
    }
}
