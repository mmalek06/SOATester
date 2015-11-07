using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Infrastructure;
using SOATester.Infrastructure.Events.Descriptors;
using SOATester.Infrastructure.Events.Enums;
using SOATester.Infrastructure.Events.EventClasses;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ProjectsViewModel : ViewModelBase {

        #region fields

        private IProjectsRepository _repository;
        private ObservableCollection<ProjectViewModel> _projects;
        private IUnityContainer _container;

        #endregion

        #region properties

        public ObservableCollection<ProjectViewModel> Projects {
            get {
                return _projects;
            }
            private set {
                SetProperty(ref _projects, value);
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
            _repository = repository;
            _container = container;
        }

        #endregion

        #region methods

        protected override void _initCollections() {
            Projects = new ObservableCollection<ProjectViewModel>();
        }

        protected override void _initCommands() {
            ChooseProject = new DelegateCommand<ProjectViewModel>(OnProjectChosen);
            ChooseScenario = new DelegateCommand<ScenarioViewModel>(OnScenarioChosen);
            ChooseTestSuite = new DelegateCommand<TestViewModel>(OnTestSuiteChosen);
            ChooseStep = new DelegateCommand<StepViewModel>(OnStepChosen);
        }

        #endregion

        #region event handlers

        private void OnProjectChosen(ProjectViewModel project) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = project.Id,
                ItemType = ChosenItemType.PROJECT
            };

            _eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        private void OnScenarioChosen(ScenarioViewModel scenario) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = scenario.Id,
                ItemType = ChosenItemType.SCENARIO
            };

            _eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        private void OnTestSuiteChosen(TestViewModel test) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = test.Id,
                ItemType = ChosenItemType.TEST
            };

            _eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        private void OnStepChosen(StepViewModel step) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = step.Id,
                ItemType = ChosenItemType.STEP
            };

            _eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        #endregion

    }
}
