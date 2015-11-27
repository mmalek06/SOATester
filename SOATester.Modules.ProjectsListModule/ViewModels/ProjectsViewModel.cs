using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Infrastructure;
using SOATester.Infrastructure.Events;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using System.Collections.ObjectModel;
using System;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ProjectsViewModel : ViewModelBase {

        #region fields

        private IProjectsRepository repository;
        private ObservableCollection<ProjectViewModel> projects;
        private IUnityContainer container;

        #endregion

        #region properties

        public ObservableCollection<ProjectViewModel> Projects {
            get {
                return projects;
            }
            private set {
                SetProperty(ref projects, value);
            }
        }

        #endregion

        #region commands

        public DelegateCommand<ProjectViewModel> ChooseProject { get; private set; }
        public DelegateCommand<ScenarioViewModel> ChooseScenario { get; private set; }
        public DelegateCommand<TestViewModel> ChooseTestSuite { get; private set; }
        public DelegateCommand<StepViewModel> ChooseStep { get; private set; }
        
        #endregion

        #region constructors and destructors

        public ProjectsViewModel(IProjectsRepository repository, IEventAggregator eventAggregator, IUnityContainer container) : base(eventAggregator) {
            this.repository = repository;
            this.container = container;
        }

        #endregion

        #region methods

        protected override void InitCollections() {
            Projects = new ObservableCollection<ProjectViewModel>();
        }

        protected override void InitCommands() {
            ChooseProject = new DelegateCommand<ProjectViewModel>(OnProjectChosen);
            ChooseScenario = new DelegateCommand<ScenarioViewModel>(OnScenarioChosen);
            ChooseTestSuite = new DelegateCommand<TestViewModel>(OnTestSuiteChosen);
            ChooseStep = new DelegateCommand<StepViewModel>(OnStepChosen);
        }

        protected override void InitEvents() {
            eventAggregator.GetEvent<StartupEventEnd>().Subscribe(OnStartupCompleted);
        }

        #endregion

        #region event handlers

        private void OnProjectChosen(ProjectViewModel project) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = project.Id,
                ItemType = ChosenItemType.PROJECT
            };

            eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        private void OnScenarioChosen(ScenarioViewModel scenario) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = scenario.Id,
                ItemType = ChosenItemType.SCENARIO
            };

            eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        private void OnTestSuiteChosen(TestViewModel test) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = test.Id,
                ItemType = ChosenItemType.TEST
            };

            eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        private void OnStepChosen(StepViewModel step) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = step.Id,
                ItemType = ChosenItemType.STEP
            };

            eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        private void OnStartupCompleted(StartupActivity activity) {
            if (activity == StartupActivity.PROJECTS_INIT) {

            }
        }

        #endregion

    }
}
