using System.Collections.Generic;

using Prism.Events;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Plugins.Enums;
using SOATester.Modules.ContentModule.Plugins.Base;
using SOATester.Modules.ContentModule.Plugins;

namespace Tests.ContentModuletests.Base {
    public abstract class BaseTest {
        protected UnityContainer _container = new UnityContainer();
        protected List<ViewModelBase> _viewModels = new List<ViewModelBase>();
        protected IEventAggregator _eventAggregator;
        protected IProjectsRepository _projectsRepo;
        protected ITestsRepository _testSuitesRepo;
        protected IStepsRepository _stepsRepo;

        [TestInitialize()]
        public void Initialize() {
            _initializeContainer();
            _initializeRepos();
            _initializeViewModels();
        }

        protected void _initializeViewModels() {
            var projectViewModel1 = _container.Resolve<ProjectViewModel>();
            var projectViewModel2 = _container.Resolve<ProjectViewModel>();

            projectViewModel1.Project = _projectsRepo.GetProject(1);
            projectViewModel2.Project = _projectsRepo.GetProject(2);

            _viewModels.Add(projectViewModel1);
            _viewModels.Add(projectViewModel2);

            /*var project1TestSuites = _testSuitesRepo.GetTestsForScenario(projectViewModel1.Project);
            var project2TestSuites = _testSuitesRepo.GetTestsForScenario(projectViewModel2.Project);
            TestViewModel testSuiteViewModel_1 = null;

            foreach (var ts in project1TestSuites) {
                var testSuiteViewModel = _container.Resolve<TestViewModel>();

                testSuiteViewModel.Test = ts;

                _viewModels.Add(testSuiteViewModel);

                if (testSuiteViewModel_1 == null) {
                    testSuiteViewModel_1 = testSuiteViewModel;
                }
            }
            foreach (var ts in project2TestSuites) {
                var testSuiteViewModel = _container.Resolve<TestViewModel>();

                testSuiteViewModel.Test = ts;

                _viewModels.Add(testSuiteViewModel);
            }

            var testSuite1Steps = _stepsRepo.GetStepsForTest(testSuiteViewModel_1.Test);

            foreach (var s in testSuite1Steps) {
                var stepViewModel = _container.Resolve<StepViewModel>();

                stepViewModel.Step = s;

                _viewModels.Add(stepViewModel);
            }*/
        }

        protected void _initializeRepos() {
            _projectsRepo = _container.Resolve<IProjectsRepository>();
            _testSuitesRepo = _container.Resolve<ITestsRepository>();
            _stepsRepo = _container.Resolve<IStepsRepository>();
        }

        protected void _initializeContainer() {
            _container.RegisterType<IEventAggregator, EventAggregator>();
            _container.RegisterInstance<IUnityContainer>(_container);

            _eventAggregator = _container.Resolve<IEventAggregator>();

            // register repositories
            _container.RegisterType<IProjectsRepository, ProjectsRepository>();
            _container.RegisterType<ITestsRepository, TestsRepository>();
            _container.RegisterType<IStepsRepository, StepsRepository>();

            // register plugins
            _container.RegisterType<PluginFactory>();
            _container.RegisterType<IPlugin, AggregatorPlugin>("Aggregator");
            _container.RegisterType<IPlugin, ColorizerPlugin>("Colorizer");
            _container.RegisterType<IEnumerable<IPlugin>, IPlugin[]>();

            // register viewmodels
            _container.RegisterType<ProjectViewModel>();
            _container.RegisterType<TestViewModel>();
            _container.RegisterType<StepViewModel>();
        }
    }
}
