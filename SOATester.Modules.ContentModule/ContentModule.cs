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

namespace SOATester.Modules.ContentModule {
    public class ContentModule : ModuleBase {

        #region fields

        private IRegionBehaviorFactory regionBehaviorFactory;

        #endregion

        #region constructors and destructors

        public ContentModule(IUnityContainer container, IRegionManager regionManager, IRegionBehaviorFactory regionBehaviorFactory) : base(container, regionManager) {
            this.regionBehaviorFactory = regionBehaviorFactory;
        }

        #endregion

        #region public methods

        public override void Initialize() {
            base.Initialize();

            InitializeCommunication();
            InitializeBehaviors();
        }

        #endregion

        #region non public methods

        protected override void InitializeRepositories() {
            container.RegisterType<IProjectsService, ProjectsService>();
            container.RegisterType<IScenariosService, ScenariosService>();
            container.RegisterType<ITestsService, TestsService>();
            container.RegisterType<IStepsService, StepsService>();
        }

        protected override void InitializeViewModels() {
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
            regionBehaviorFactory.AddIfMissing(nameof(TabControlRegionBehavior), typeof(TabControlRegionBehavior));
        }

        #endregion
    }
}
