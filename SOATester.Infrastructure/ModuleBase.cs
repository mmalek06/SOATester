using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SOATester.Infrastructure.ConfigurationEnums;
using System;
using System.Linq;

namespace SOATester.Infrastructure {
    public abstract class ModuleBase : IModule {

        #region fields

        protected IUnityContainer _container;
        protected IRegionManager _regionManager;
        protected static AppMode _appMode;

        #endregion

        #region constructors and destructors

        static ModuleBase() {
            bool isUnderTests = AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.ToLowerInvariant().StartsWith("microsoft.visualstudio.qualitytools"));

            if (isUnderTests) {
                _appMode = AppMode.TESTING;
            } else {
                _appMode = AppMode.RUN;
            }
        }

        public ModuleBase(IUnityContainer container, IRegionManager regionManager) {
            _container = container;
            _regionManager = regionManager;
        }

        #endregion

        #region public methods

        public void Initialize() {
            _initializeRepositories();
            _initializeViewModels();
            _initializePlugins();
            _initializeViews();
            _initializeRegions();
        }

        #endregion

        #region methods

        protected virtual void _initializeRegions() { }

        protected virtual void _initializePlugins() { }

        protected virtual void _initializeViews() { }

        protected virtual void _initializeViewModels() { }

        protected virtual void _initializeRepositories() { }

        #endregion
    }
}
