using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Microsoft.Practices.Unity;

using Prism.Modularity;
using Prism.Regions;

using SOATester.Infrastructure.ConfigurationEnums;

namespace SOATester.Infrastructure {
    public abstract class ModuleBase : IModule {

        #region fields

        protected IUnityContainer _container;
        protected IRegionManager _regionManager;
        protected AppMode _appMode;

        #endregion

        #region constructors and destructors

        public ModuleBase(IUnityContainer container, IRegionManager regionManager) {
            _container = container;
            _regionManager = regionManager;
        }

        #endregion

        #region public methods

        public void Initialize() {
            _getConfiguration();
            _initializeRepositories();
            _initializeViewModels();
            _initializePlugins();
            _initializeViews();
            _initializeRegions();
        }

        #endregion

        #region private methods

        protected virtual void _getConfiguration() {
            var mode = ConfigurationManager.AppSettings["mode"];

            Enum.TryParse(mode, out _appMode);
        }


        protected virtual void _initializeRegions() { }

        protected virtual void _initializePlugins() { }

        protected virtual void _initializeViews() { }

        protected virtual void _initializeViewModels() { }

        protected virtual void _initializeRepositories() { }

        #endregion
    }
}
