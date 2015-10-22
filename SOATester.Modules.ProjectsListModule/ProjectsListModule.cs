using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

using Prism.Regions;

using SOATester.Infrastructure;
using SOATester.Infrastructure.ConfigurationEnums;

using SOATester.Modules.ProjectsListModule.Utils;
using SOATester.Modules.ProjectsListModule.Views;
using SOATester.Modules.ProjectsListModule.ViewModels;
using SOATester.Modules.ProjectsListModule.Repositories;
using SOATester.Modules.ProjectsListModule.Repositories.Mock;
using SOATester.Modules.ProjectsListModule.Repositories.Base;

namespace SOATester.Modules.ProjectsListModule {
    public class ProjectsListModule : ModuleBase {

        #region constructors and destructors

        public ProjectsListModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void _initializeRepositories() {
            if (_appMode == AppMode.RUN) {
                _container.RegisterType<IProjectsRepository, SOATester.Modules.ProjectsListModule.Repositories.ProjectsRepository>();
            } else if (_appMode == AppMode.TESTING) {
                _container.RegisterType<IProjectsRepository, SOATester.Modules.ProjectsListModule.Repositories.Mock.ProjectsRepository>();
            }
        }

        protected override void _initializeViews() {
            _container.RegisterType<ProjectsView>();
        }

        protected override void _initializeViewModels() {
            // register utility classes
            _container.RegisterType<HierarchicalViewModelBuilder>();

            // register view models
            _container.RegisterInstance<ProjectsViewModel>(_container.Resolve<HierarchicalViewModelBuilder>().Build());
            _container.RegisterType<ProjectViewModel>();
            _container.RegisterType<TestSuiteViewModel>();
            _container.RegisterType<StepViewModel>();
        }

        protected override void _initializeRegions() {
            IRegion projectsRegion = _regionManager.Regions[RegionNames.ProjectsRegion];

            projectsRegion.Add(_container.Resolve<ProjectsView>());
        }

        #endregion

    }
}
