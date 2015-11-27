using Microsoft.Practices.Unity;
using Prism.Regions;
using SOATester.Infrastructure;
using SOATester.Infrastructure.ConfigurationEnums;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using SOATester.Modules.ProjectsListModule.ViewModels;
using SOATester.Modules.ProjectsListModule.ViewModels.Builders;
using SOATester.Modules.ProjectsListModule.Views;

namespace SOATester.Modules.ProjectsListModule {
    public class ProjectsListModule : ModuleBase {

        #region constructors and destructors

        public ProjectsListModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void InitializeRepositories() {
            if (appMode == AppMode.RUN) {
                container.RegisterType<IProjectsRepository, Repositories.ProjectsRepository>();
            } else if (appMode == AppMode.TESTING) {
                container.RegisterType<IProjectsRepository, Repositories.Mock.ProjectsRepository>();
            }
        }

        protected override void InitializeViews() {
            container.RegisterType<ProjectsView>();
        }

        protected override void InitializeViewModels() {
            // register utility classes
            container.RegisterType<HierarchicalViewModelBuilder>();

            // register view models
            container.RegisterInstance<ProjectsViewModel>(container.Resolve<HierarchicalViewModelBuilder>().Build());
            container.RegisterType<ScenarioViewModel>();
            container.RegisterType<ProjectViewModel>();
            container.RegisterType<TestViewModel>();
            container.RegisterType<StepViewModel>();
        }

        protected override void InitializeRegions() {
            IRegion projectsRegion = regionManager.Regions[RegionNames.ProjectsRegion];

            projectsRegion.Add(container.Resolve<ProjectsView>());
        }

        #endregion

    }
}
