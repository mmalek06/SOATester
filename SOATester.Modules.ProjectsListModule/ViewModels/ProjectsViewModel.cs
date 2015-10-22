using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Events;
using Microsoft.Practices.Unity;

using SOATester.Infrastructure;
using SOATester.Infrastructure.Events.Enums;
using SOATester.Infrastructure.Events.Descriptors;
using SOATester.Infrastructure.Events.EventClasses;
using SOATester.Modules.ProjectsListModule.Repositories.Base;

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
        public DelegateCommand<TestSuiteViewModel> ChooseTestSuite { get; private set; }
        public DelegateCommand<StepViewModel> ChooseStep { get; private set; }
        
        #endregion

        #region constructors and destructors

        public ProjectsViewModel(IProjectsRepository repository, IEventAggregator eventAggregator, IUnityContainer container) : base(eventAggregator) {
            _repository = repository;
            _container = container;
        }

        #endregion

        #region private methods

        protected override void _initCollections() {
            Projects = new ObservableCollection<ProjectViewModel>();
        }

        protected override void _initCommands() {
            ChooseProject = new DelegateCommand<ProjectViewModel>(OnProjectChosen);
            ChooseTestSuite = new DelegateCommand<TestSuiteViewModel>(OnTestSuiteChosen);
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

        private void OnTestSuiteChosen(TestSuiteViewModel testSuite) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = testSuite.Id,
                ItemType = ChosenItemType.TEST_SUITE
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
