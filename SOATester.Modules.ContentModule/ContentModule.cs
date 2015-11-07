using Microsoft.Practices.Unity;
using Prism.Regions;
using SOATester.Communication;
using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Plugins.Base;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Views;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule {
    public class ContentModule : ModuleBase {
        
        #region constructors and destructors

        public ContentModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void _initializeRepositories() {
            if (_appMode == Infrastructure.ConfigurationEnums.AppMode.RUN) {
                _container.RegisterType<IProjectsRepository, Repositories.ProjectsRepository>();
                _container.RegisterType<IScenariosRepository, Repositories.ScenariosRepository>();
                _container.RegisterType<ITestsRepository, Repositories.TestsRepository>();
                _container.RegisterType<IStepsRepository, Repositories.StepsRepository>();
            } else {
                _container.RegisterType<IProjectsRepository, Repositories.Mock.ProjectsRepository>();
                _container.RegisterType<IScenariosRepository, Repositories.Mock.ScenariosRepository>();
                _container.RegisterType<ITestsRepository, Repositories.Mock.TestsRepository>();
                _container.RegisterType<IStepsRepository, Repositories.Mock.StepsRepository>();
            }
        }

        protected override void _initializeViewModels() {
            // register plugins
            // _container.RegisterType<PluginFactory>();
            // _container.RegisterType<IPlugin, TabAggregator>();
            // _container.RegisterType<IPlugin, TabColorizer>();

            _initializeCommunication();

            // register view models
            _container.RegisterType<ICollectionViewModel, ContentViewModel>(
                new InjectionProperty("ProjectsRepository", _container.Resolve<IProjectsRepository>()),
                new InjectionProperty("ScenariosRepository", _container.Resolve<IScenariosRepository>()),
                new InjectionProperty("TestSuitesRepository", _container.Resolve<ITestsRepository>()),
                new InjectionProperty("StepsRepository", _container.Resolve<IStepsRepository>()));
            _container.RegisterType<ProjectViewModel>();
            _container.RegisterType<ScenarioViewModel>();
            _container.RegisterType<TestViewModel>();
            _container.RegisterType<StepViewModel>();
        }

        private void _initializeCommunication() {
            _container.RegisterType<IProjectsRunner, ProjectsRunner>();
            _container.RegisterType<IScenariosRunner, ScenariosRunner>();
            _container.RegisterType<ITestsRunner, TestsRunner>();
            _container.RegisterType<IStepsRunner, StepsRunner>();
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
            _container.RegisterType<ScenarioView>();
            _container.RegisterType<StepView>();
            _container.RegisterType<TestView>();
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
