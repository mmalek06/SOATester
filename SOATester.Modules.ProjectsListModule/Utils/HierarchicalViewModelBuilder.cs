using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

using SOATester.Entities;

using SOATester.Modules.ProjectsListModule.ViewModels;
using SOATester.Modules.ProjectsListModule.Repositories.Base;

namespace SOATester.Modules.ProjectsListModule.Utils {
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

            foreach (var projectEntity in entities) {
                // for property injection use: new PropertyOverride("Project", projectEntity)
                var projectVm = _container.Resolve<ProjectViewModel>(new PropertyOverride("Id", projectEntity.Id), 
                    new PropertyOverride("Name", projectEntity.Name));

                foreach (var testSuite in projectEntity.TestSuites) {
                    var testSuiteVm = _container.Resolve<TestSuiteViewModel>(new PropertyOverride("Id", testSuite.Id),
                        new PropertyOverride("Name", testSuite.Name));

                    foreach (var step in testSuite.Steps) {
                        var stepVm = _container.Resolve<StepViewModel>(new PropertyOverride("Id", step.Id),
                            new PropertyOverride("Name", step.Name));

                        testSuiteVm.Items.Add(stepVm);
                    }

                    projectVm.Items.Add(testSuiteVm);
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
