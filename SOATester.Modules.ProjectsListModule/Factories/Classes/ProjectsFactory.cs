using Microsoft.Practices.Unity;
using SOATester.DAL.Repositories;
using SOATester.Entities;
using SOATester.Infrastructure.IOC;
using SOATester.Modules.ProjectsListModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ProjectsListModule.Factories {
    public class ProjectsFactory : IProjectsFactory {

        #region fields

        private IList<Type[]> hierarchy;
        private ProjectsRepository projectsRepository;
        private IUnityContainer container;

        #endregion

        #region constructor

        public ProjectsFactory(IUnityContainer container, ProjectsRepository projectsRepository) {
            this.container = container;
            this.projectsRepository = projectsRepository;
            hierarchy = new List<Type[]>(new[] {
                new[] { typeof(ProjectViewModel), typeof(Project) },
                new[] { typeof(ScenarioViewModel), typeof(Scenario) },
                new[] { typeof(TestViewModel), typeof(Test) },
                new[] { typeof(StepViewModel), typeof(Step) }
            });
        }

        #endregion

        #region public methods

        public IEnumerable<IIdentifiableViewModel> CreateTreeStructure() {
            var projects = projectsRepository.GetProjects();
            var vms = CreateViewModels(projects);
            
            return vms;
        }

        #endregion

        #region methods

        private IEnumerable<IIdentifiableViewModel> CreateViewModels(IEnumerable<object> entities, int level = 0) {
            var vms = new List<IIdentifiableViewModel>();
            Type[] pair = hierarchy[level];

            if (hierarchy.Any()) {
                foreach (var entity in entities) {
                    var vm = container.Resolve(pair[0], new TypedParametersOverride(entity, pair[1]));
                    bool entityContainsChildren = entity is IParentEntity;
                    bool isViewModelHierarchical = vm is IHierarchicalViewModel;

                    vms.Add(vm as IIdentifiableViewModel);

                    if (entityContainsChildren && isViewModelHierarchical) {
                        var tmpVms = CreateViewModels((entity as IParentEntity).Children, level + 1);

                        (vm as IHierarchicalViewModel).Items.AddRange(tmpVms);
                    }
                }
            }

            return vms;
        }

        #endregion

    }
}
