using Microsoft.Practices.Unity;
using Prism.Regions;
using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Services;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.Views;
using SOATester.Modules.ContentModule.Views.RegionBehaviors;
using SOATester.RestCommunication;
using SOATester.RestCommunication.Base;
using System.Collections.Generic;
using System;
using SOATester.Infrastructure.Prism;

namespace SOATester.Modules.ContentModule {
    public class ContentModule : ModuleBase {

        #region fields

        private IRegionBehaviorFactory regionBehaviorFactory;

        #endregion

        #region constructor

        public ContentModule(IUnityContainer container, IRegionManager regionManager, IRegionBehaviorFactory regionBehaviorFactory) : base(container, regionManager) {
            this.regionBehaviorFactory = regionBehaviorFactory;
        }

        #endregion

        #region public methods

        public override void Initialize() {
            base.Initialize();

            InitializeCommunication();
            InitializeBehaviors();
            InitializeContentLoader();
        }

        #endregion

        #region methods

        protected override void InitializeRepositories() {
            container.RegisterType<IProjectsService, ProjectsService>();
            container.RegisterType<IScenariosService, ScenariosService>();
            container.RegisterType<ITestsService, TestsService>();
            container.RegisterType<IStepsService, StepsService>();
        }

        protected override void InitializeViewModels() {
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
            container.RegisterType<PluginBuilder>();
            container.RegisterType<PluginBase, Aggregator>("Aggregator");
            container.RegisterType<PluginBase, Colorizer>("Colorizer");
            container.RegisterType<IEnumerable<PluginBase>, PluginBase[]>();
            container.RegisterInstance(new PluginRunner(container.Resolve<PluginBuilder>()));
        }

        protected override void InitializeViews() {
            container.RegisterType<MainMenuView>();
            container.RegisterType<ContentView>();
            container.RegisterType<object, ProjectView>(typeof(ProjectView).Name.ToString());
            container.RegisterType<object, ScenarioView>(typeof(ScenarioView).Name.ToString());
            container.RegisterType<object, StepView>(typeof(StepView).Name.ToString());
            container.RegisterType<object, TestView>(typeof(TestView).Name.ToString());
        }

        protected override void InitializeRegions() {
            IRegion menuRegion = regionManager.Regions[RegionNames.MainMenuRegion];
            IRegion contentRegion = regionManager.Regions[RegionNames.ContentRegion];

            menuRegion.Add(container.Resolve<MainMenuView>());
            contentRegion.Add(container.Resolve<ContentView>());
        }

        private void InitializeBehaviors() {
            regionBehaviorFactory.AddIfMissing(nameof(SortableRegionBehavior), typeof(SortableRegionBehavior));
        }

        private void InitializeContentLoader() {
            container.RegisterType<IRegionNavigationContentLoader, ExtendedUnityRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());
        }

        #endregion
    }
}
