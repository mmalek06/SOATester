using Microsoft.Practices.Unity;
using Prism.Regions;
using SOATester.RestCommunication;
using SOATester.RestCommunication.Base;
using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Plugins.Base;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Views;
using System.Collections.Generic;
using SOATester.Entities;

namespace SOATester.Modules.ContentModule {
    public class ContentModule : ModuleBase {
        
        #region constructors and destructors

        public ContentModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void InitializeRepositories() {
            if (appMode == Infrastructure.ConfigurationEnums.AppMode.RUN) {
                container.RegisterType<ISimpleRepository<Project>, Repositories.ProjectsRepository>();
                container.RegisterType<IRepository<Scenario, Project>, Repositories.ScenariosRepository>();
                container.RegisterType<IRepository<Test, Scenario>, Repositories.TestsRepository>();
                container.RegisterType<IRepository<Step, Test>, Repositories.StepsRepository>();
            } else {
                /*_container.RegisterType<ISimpleRepository<Project>, Repositories.Mock.ProjectsRepository>();
                _container.RegisterType<IRepository<Scenario, Project>, Repositories.Mock.ScenariosRepository>();
                _container.RegisterType<IRepository<Test, Scenario>, Repositories.Mock.TestsRepository>();
                _container.RegisterType<IRepository<Step, Test>, Repositories.Mock.StepsRepository>();*/
            }
        }

        protected override void InitializeViewModels() {
            // register plugins
            // _container.RegisterType<PluginFactory>();
            // _container.RegisterType<IPlugin, TabAggregator>();
            // _container.RegisterType<IPlugin, TabColorizer>();

            _initializeCommunication();

            // register view models
            container.RegisterType<ICollectionViewModel, ContentViewModel>(
                new InjectionProperty("ProjectsRepository", container.Resolve<ISimpleRepository<Project>>()),
                new InjectionProperty("ScenariosRepository", container.Resolve<IRepository<Scenario, Project>>()),
                new InjectionProperty("TestsRepository", container.Resolve<IRepository<Test, Scenario>>()),
                new InjectionProperty("StepsRepository", container.Resolve<IRepository<Step, Test>>()));
            container.RegisterType<ProjectViewModel>();
            container.RegisterType<ScenarioViewModel>();
            container.RegisterType<TestViewModel>();
            container.RegisterType<StepViewModel>();
        }

        private void _initializeCommunication() {
            container.RegisterType<IProjectsRunner, ProjectsRunner>();
            container.RegisterType<IScenariosRunner, ScenariosRunner>();
            container.RegisterType<ITestsRunner, TestsRunner>();
            container.RegisterType<IStepsRunner, StepsRunner>();
        }

        protected override void InitializePlugins() {
            container.RegisterType<PluginFactory>();
            container.RegisterType<IPlugin, AggregatorPlugin>("Aggregator");
            container.RegisterType<IPlugin, ColorizerPlugin>("Colorizer");
            container.RegisterType<IEnumerable<IPlugin>, IPlugin[]>();
        }

        protected override void InitializeViews() {
            container.RegisterType<MainMenuView>();
            container.RegisterType<ProjectView>();
            container.RegisterType<ContentView>();
            container.RegisterType<ScenarioView>();
            container.RegisterType<StepView>();
            container.RegisterType<TestView>();
        }

        protected override void InitializeRegions() {
            IRegion menuRegion = regionManager.Regions[RegionNames.MainMenuRegion];
            IRegion contentRegion = regionManager.Regions[RegionNames.ContentRegion];

            menuRegion.Add(container.Resolve<MainMenuView>());
            contentRegion.Add(container.Resolve<ContentView>());
        }

        #endregion
    }
}
