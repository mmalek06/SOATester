﻿using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using SOATester.Infrastructure.ViewModels;
using SOATester.Modules.ContentModule.ViewModels;
using System.Collections.Generic;

namespace Tests.ContentModuletests.Base {
    public abstract class BaseTest {
        protected UnityContainer container = new UnityContainer();
        protected List<ViewModelBase> viewModels = new List<ViewModelBase>();
        protected IEventAggregator eventAggregator;
        /*protected IProjectsRepository _projectsRepo;
        protected ITestsRepository _testSuitesRepo;
        protected IStepsRepository _stepsRepo;*/

        [TestInitialize()]
        public void Initialize() {
            InitializeContainer();
            InitializeRepos();
            InitializeViewModels();
        }

        protected void InitializeViewModels() {
            var projectViewModel1 = container.Resolve<ProjectViewModel>();
            var projectViewModel2 = container.Resolve<ProjectViewModel>();

            /*projectViewModel1.Project = _projectsRepo.GetProject(1);
            projectViewModel2.Project = _projectsRepo.GetProject(2);

            _viewModels.Add(projectViewModel1);
            _viewModels.Add(projectViewModel2);

            var project1TestSuites = _testSuitesRepo.GetTestsForScenario(projectViewModel1.Project);
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

        protected void InitializeRepos() {
            /*_projectsRepo = _container.Resolve<IProjectsRepository>();
            _testSuitesRepo = _container.Resolve<ITestsRepository>();
            _stepsRepo = _container.Resolve<IStepsRepository>();*/
        }

        protected void InitializeContainer() {
            /*_container.RegisterType<IEventAggregator, EventAggregator>();
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
            _container.RegisterType<StepViewModel>();*/
        }
    }
}
