using Microsoft.Practices.Unity;
using Prism.Regions;
using SOATester.Entities;
using SOATester.Infrastructure;
using SOATester.Infrastructure.ViewModels;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.Views;
using SOATester.RestCommunication;
using SOATester.RestCommunication.Base;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule {
    public class ContentModule : ModuleBase {
        
        #region constructors and destructors

        public ContentModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void InitializeRepositories() {
            if (AppMode == Infrastructure.ConfigurationEnums.AppMode.RUN) {
                container.RegisterType<ISimpleRepository<Project>, Repositories.ProjectsRepository>();
                container.RegisterType<IRepository<Scenario, Project>, Repositories.ScenariosRepository>();
                container.RegisterType<IRepository<Test, Scenario>, Repositories.TestsRepository>();
                container.RegisterType<IRepository<Step, Test>, Repositories.StepsRepository>();
            } else {
                /*container.RegisterType<ISimpleRepository<Project>, Repositories.Mock.ProjectsRepository>();
                container.RegisterType<IRepository<Scenario, Project>, Repositories.Mock.ScenariosRepository>();
                container.RegisterType<IRepository<Test, Scenario>, Repositories.Mock.TestsRepository>();
                container.RegisterType<IRepository<Step, Test>, Repositories.Mock.StepsRepository>();*/
            }
        }

        protected override void InitializeViewModels() {
            // register plugins
            // container.RegisterType<PluginFactory>();
            // container.RegisterType<IPlugin, TabAggregator>();
            // container.RegisterType<IPlugin, TabColorizer>();

            InitializeCommunication();

            // register view models
            //container.RegisterType<ICollectionViewModel, ContentViewModel>();
            container.RegisterType<ContentViewModel>();
            container.RegisterType<ProjectViewModel>();
            container.RegisterType<ScenarioViewModel>();
            container.RegisterType<TestViewModel>();
            container.RegisterType<StepViewModel>();
        }

        private void InitializeCommunication() {
            container.RegisterType<IProjectsRunner, ProjectsRunner>();
            container.RegisterType<IScenariosRunner, ScenariosRunner>();
            container.RegisterType<ITestsRunner, TestsRunner>();
            container.RegisterType<IStepsRunner, StepsRunner>();
        }

        protected override void InitializePlugins() {
            container.RegisterType<PluginFactory>();
            container.RegisterType<IPlugin, Aggregator>("Aggregator");
            container.RegisterType<IPlugin, Colorizer>("Colorizer");
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
