using Microsoft.Practices.Unity;
using SOATester.Modules.ProjectsListModule.Repositories;
using System.Collections.Generic;

namespace SOATester.Modules.ProjectsListModule.ViewModels.Builders {
    public class HierarchicalViewModelBuilder {

        #region fields

        private IUnityContainer container;
        private IProjectsRepository repository;

        #endregion

        public HierarchicalViewModelBuilder(IUnityContainer container, IProjectsRepository repository) {
            this.container = container;
            this.repository = repository;
        }

        public IEnumerable<ProjectViewModel> Build() {
            var entities = repository.ProjectsCache;
            var vms = new List<ProjectViewModel>();

            foreach (var project in entities) {
                // for property injection use: new PropertyOverride("Project", projectEntity)
                var projectVm = container.Resolve<ProjectViewModel>(new PropertyOverride("Id", project.Id), 
                    new PropertyOverride("Name", project.Name));

                foreach (var scenario in project.Scenarios) {
                    var scenarioVm = container.Resolve<ScenarioViewModel>(new PropertyOverride("Id", scenario.Id),
                        new PropertyOverride("Name", scenario.Name));

                    foreach (var test in scenario.Tests) {
                        var testVm = container.Resolve<TestViewModel>(new PropertyOverride("Id", test.Id),
                            new PropertyOverride("Name", test.Name));

                        foreach (var step in test.Steps) {
                            var stepVm = container.Resolve<StepViewModel>(new PropertyOverride("Id", step.Id),
                                new PropertyOverride("Name", step.Name));

                            testVm.Items.Add(stepVm);
                        }

                        scenarioVm.Items.Add(testVm);
                    }

                    projectVm.Items.Add(scenarioVm);
                }

                vms.Add(projectVm);
            }

            return vms;
        }
    }
}
