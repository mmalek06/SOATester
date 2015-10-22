﻿using System.Collections.Generic;

using Prism.Events;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Utils;
using SOATester.Modules.ContentModule.Views.Plugins.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Classes;

namespace Tests.ContentModuletests.Base {
    public abstract class BaseTest {
        protected UnityContainer _container = new UnityContainer();
        protected List<IViewModel> _viewModels = new List<IViewModel>();
        protected IEventAggregator _eventAggregator;
        protected IProjectsRepository _projectsRepo;
        protected ITestSuitesRepository _testSuitesRepo;
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

            var project1TestSuites = _testSuitesRepo.GetTestSuitesForProject(projectViewModel1.Project);
            var project2TestSuites = _testSuitesRepo.GetTestSuitesForProject(projectViewModel2.Project);
            TestSuiteViewModel testSuiteViewModel_1 = null;

            foreach (var ts in project1TestSuites) {
                var testSuiteViewModel = _container.Resolve<TestSuiteViewModel>();

                testSuiteViewModel.TestSuite = ts;

                _viewModels.Add(testSuiteViewModel);

                if (testSuiteViewModel_1 == null) {
                    testSuiteViewModel_1 = testSuiteViewModel;
                }
            }
            foreach (var ts in project2TestSuites) {
                var testSuiteViewModel = _container.Resolve<TestSuiteViewModel>();

                testSuiteViewModel.TestSuite = ts;

                _viewModels.Add(testSuiteViewModel);
            }

            var testSuite1Steps = _stepsRepo.GetStepsForTestSuite(testSuiteViewModel_1.TestSuite);

            foreach (var s in testSuite1Steps) {
                var stepViewModel = _container.Resolve<StepViewModel>();

                stepViewModel.Step = s;

                _viewModels.Add(stepViewModel);
            }
        }

        protected void _initializeRepos() {
            _projectsRepo = _container.Resolve<IProjectsRepository>();
            _testSuitesRepo = _container.Resolve<ITestSuitesRepository>();
            _stepsRepo = _container.Resolve<IStepsRepository>();
        }

        protected void _initializeContainer() {
            _container.RegisterType<IEventAggregator, EventAggregator>();
            _container.RegisterInstance<IUnityContainer>(_container);

            _eventAggregator = _container.Resolve<IEventAggregator>();

            // register repositories
            _container.RegisterType<IProjectsRepository, ProjectsRepository>();
            _container.RegisterType<ITestSuitesRepository, TestSuitesRepository>();
            _container.RegisterType<IStepsRepository, StepsRepository>();

            // register plugins
            _container.RegisterType<PluginFactory>();
            _container.RegisterType<IPlugin, TabAggregator>("Aggregator");
            _container.RegisterType<IPlugin, TabColorizer>("Colorizer");
            _container.RegisterType<IEnumerable<IPlugin>, IPlugin[]>();

            // register viewmodels
            _container.RegisterType<ProjectViewModel>();
            _container.RegisterType<TestSuiteViewModel>();
            _container.RegisterType<StepViewModel>();
        }
    }
}
