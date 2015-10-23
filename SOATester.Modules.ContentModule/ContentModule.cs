﻿using System.Collections.Generic;

using Microsoft.Practices.Unity;

using Prism.Regions;

using SOATester.Infrastructure;

using SOATester.Modules.ContentModule.Views;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.Views.Plugins.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Utils;
using SOATester.Modules.ContentModule.Views.Plugins.Classes;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Repositories.Base;

namespace SOATester.Modules.ContentModule {
    public class ContentModule : ModuleBase {
        
        #region constructors and destructors

        public ContentModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void _initializeRepositories() {
            if (_appMode == Infrastructure.ConfigurationEnums.AppMode.RUN) {
                _container.RegisterType<IProjectsRepository, Repositories.ProjectsRepository>();
                _container.RegisterType<ITestSuitesRepository, Repositories.TestSuitesRepository>();
                _container.RegisterType<IStepsRepository, Repositories.StepsRepository>();
            } else {
                _container.RegisterType<IProjectsRepository, Repositories.Mock.ProjectsRepository>();
                _container.RegisterType<ITestSuitesRepository, Repositories.Mock.TestSuitesRepository>();
                _container.RegisterType<IStepsRepository, Repositories.Mock.StepsRepository>();
            }
        }

        protected override void _initializeViewModels() {
            // register plugins
            // _container.RegisterType<PluginFactory>();
            // _container.RegisterType<IPlugin, TabAggregator>();
            // _container.RegisterType<IPlugin, TabColorizer>();

            // register view models
            _container.RegisterType<ICollectionViewModel, ContentViewModel>(
                new InjectionProperty("ProjectsRepository", _container.Resolve<IProjectsRepository>()),
                new InjectionProperty("TestSuitesRepository", _container.Resolve<ITestSuitesRepository>()),
                new InjectionProperty("StepsRepository", _container.Resolve<IStepsRepository>()));
            _container.RegisterType<ProjectViewModel>();
            _container.RegisterType<TestSuiteViewModel>();
            _container.RegisterType<StepViewModel>();
        }

        protected override void _initializePlugins() {
            _container.RegisterType<PluginFactory>();
            _container.RegisterType<IPlugin, TabAggregator>("Aggregator");
            _container.RegisterType<IPlugin, TabColorizer>("Colorizer");
            _container.RegisterType<IEnumerable<IPlugin>, IPlugin[]>();
        }

        protected override void _initializeViews() {
            _container.RegisterType<MainMenuView>();
            _container.RegisterType<ProjectView>();
            _container.RegisterType<ContentView>();
            _container.RegisterType<StepView>();
            _container.RegisterType<TestSuiteView>();
        }

        protected override void _initializeRegions() {
            IRegion menuRegion = _regionManager.Regions[RegionNames.MainMenuRegion];
            IRegion contentRegion = _regionManager.Regions[RegionNames.ContentRegion];

            menuRegion.Add(_container.Resolve<MainMenuView>());
            contentRegion.Add(_container.Resolve<ContentView>());
        }

        #endregion
    }
}