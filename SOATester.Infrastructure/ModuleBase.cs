﻿using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SOATester.Infrastructure.ConfigurationEnums;
using System;
using System.Linq;

namespace SOATester.Infrastructure {
    public abstract class ModuleBase : IModule {

        #region fields

        protected IUnityContainer container;
        protected IRegionManager regionManager;

        #endregion

        public static AppMode AppMode { get; set; }

        #region constructors and destructors

        static ModuleBase() {
            bool isUnderTests = AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.ToLowerInvariant().StartsWith("microsoft.visualstudio.qualitytools"));

            if (isUnderTests) {
                AppMode = AppMode.TESTING;
            } else {
                AppMode = AppMode.RUN;
            }
        }

        public ModuleBase(IUnityContainer container, IRegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }

        #endregion

        #region public methods

        public virtual void Initialize() {
            InitializeRepositories();
            InitializeViewModels();
            InitializePlugins();
            InitializeViews();
            InitializeRegions();
        }

        #endregion

        #region methods

        protected virtual void InitializeRegions() { }

        protected virtual void InitializePlugins() { }

        protected virtual void InitializeViews() { }

        protected virtual void InitializeViewModels() { }

        protected virtual void InitializeRepositories() { }

        #endregion
    }
}
