﻿using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SOATester.Infrastructure.ConfigurationEnums;
using System;
using System.Configuration;
using System.Linq;

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

            _setAppMode();
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

        #region methods

        protected void _setAppMode() {
            bool isUnderTests = AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.ToLowerInvariant().StartsWith("microsoft.visualstudio.qualitytools"));

            if (isUnderTests) {
                _appMode = AppMode.TESTING;
            } else {
                _appMode = AppMode.RUN;
            }
        }

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
