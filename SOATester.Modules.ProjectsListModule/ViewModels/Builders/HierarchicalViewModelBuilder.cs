using Microsoft.Practices.Unity;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using SOATester.Modules.ProjectsListModule.ViewModels;
using System.Collections.Generic;

namespace SOATester.Modules.ProjectsListModule.ViewModels.Builders {
    public class HierarchicalViewModelBuilder {

        #region fields

        private IUnityContainer _container;
        private IProjectsRepository _repository;

        #endregion

        public HierarchicalViewModelBuilder(IUnityContainer container, IProjectsRepository repository) {
            _container = container;
            _repository = repository;
        }

        public ProjectsViewModel Build() {
            var entities = _repository.GetProjects();
            var mainVm = _container.Resolve<ProjectsViewModel>();
            var vms = new List<ProjectViewModel>();

            return mainVm;

            foreach (var project in entities) {
                // for property injection use: new PropertyOverride("Project", projectEntity)
                var projectVm = _container.Resolve<ProjectViewModel>(new PropertyOverride("Id", project.Id), 
                    new PropertyOverride("Name", project.Name));

                foreach (var scenario in project.Scenarios) {
                    var scenarioVm = _container.Resolve<ScenarioViewModel>(new PropertyOverride("Id", scenario.Id),
                        new PropertyOverride("Name", scenario.Name));

                    foreach (var test in scenario.Tests) {
                        var testVm = _container.Resolve<TestViewModel>(new PropertyOverride("Id", test.Id),
                            new PropertyOverride("Name", test.Name));

                        foreach (var step in test.Steps) {
                            var stepVm = _container.Resolve<StepViewModel>(new PropertyOverride("Id", step.Id),
                                new PropertyOverride("Name", step.Name));

                            testVm.Items.Add(stepVm);
                        }

                        scenarioVm.Items.Add(testVm);
                    }

                    projectVm.Items.Add(scenarioVm);
                }

                vms.Add(projectVm);
            }

            foreach (var vm in vms) {
                mainVm.Projects.Add(vm);
            }

            return mainVm;
        }
    }
}
