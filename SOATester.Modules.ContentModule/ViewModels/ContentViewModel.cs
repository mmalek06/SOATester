using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

using Prism.Events;
using Prism.Commands;

using SOATester.Infrastructure;
using SOATester.Infrastructure.Events.Enums;
using SOATester.Infrastructure.Events.EventClasses;
using SOATester.Infrastructure.Events.Descriptors;

using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Repositories.Base;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ContentViewModel : ViewModelBase, ICollectionViewModel {

        #region fields

        private ObservableCollection<object> _openedItems;
        private IUnityContainer _container;
        private IProjectsRepository _projectsRepository;
        private ITestSuitesRepository _testSuitesRepository;
        private IStepsRepository _stepsRepository;
        private object _activeItem;
        
        #endregion

        #region properties

        public ObservableCollection<object> Items {
            get { return _openedItems; }
            protected set { SetProperty(ref _openedItems, value); }
        }

        public object ActiveItem {
            get { return _activeItem; }
            private set { SetProperty(ref _activeItem, value); }
        }
        
        [Dependency]
        public IProjectsRepository ProjectsRepository {
            set { SetProperty(ref _projectsRepository, value); }
        }

        [Dependency]
        public ITestSuitesRepository TestSuitesRepository {
            set { SetProperty(ref _testSuitesRepository, value); }
        }

        [Dependency]
        public IStepsRepository StepsRepository {
            set { SetProperty(ref _stepsRepository, value); }
        }

        #endregion

        #region commands

        public DelegateCommand<object> CloseItem { get; private set; }

        #endregion

        #region constructors and destructors

        public ContentViewModel(IEventAggregator eventAggregator, IUnityContainer container) : base(eventAggregator) {
            Items = new ObservableCollection<object>();
            _container = container;
        }

        #endregion

        #region public methods
        
        #endregion

        #region private methods

        protected override void _initEvents() {
            _eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(OnProjectChosen);
            _eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(OnTestSuiteChosen);
            _eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(OnStepChosen);
        }
        
        protected override void _initCommands() {
            CloseItem = new DelegateCommand<object>(OnItemClose);
        }

        #endregion

        #region event handlers

        private void OnItemClose(object item) {
            ActiveItem = null;

            Items.Remove(item);

            if (Items.Any()) {
                ActiveItem = Items[0];
            }
        }

        private void OnProjectChosen(ItemChosenEventDescriptor evtDescriptor) {
            if (evtDescriptor.ItemType == ChosenItemType.PROJECT) {
                var project = _projectsRepository.GetProject(evtDescriptor.Id);
                
                if (project != null) {
                    ProjectViewModel projectVm;
                    int itemIndex = _getItemIndex<ProjectViewModel>((vm) => { return vm != null && vm.Id == project.Id; });

                    if (itemIndex < 0) {
                        projectVm = _container.Resolve<ProjectViewModel>();

                        projectVm.Project = project;

                        if (Items.FirstOrDefault(openedProject => openedProject.Equals(projectVm)) == null) {
                            Items.Add(projectVm);
                        }
                    } else {
                        projectVm = Items[itemIndex] as ProjectViewModel;
                    }

                    ActiveItem = projectVm;
                }
            }
        }

        private void OnTestSuiteChosen(ItemChosenEventDescriptor evtDescriptor) {
            if (evtDescriptor.ItemType == ChosenItemType.TEST_SUITE) {
                var testSuite = _testSuitesRepository.GetTestSuite(evtDescriptor.Id);
                
                if (testSuite != null) {
                    TestSuiteViewModel testSuiteVm;
                    int itemIndex = _getItemIndex<TestSuiteViewModel>((vm) => { return vm != null && vm.Id == testSuite.Id; });

                    if (itemIndex < 0) {
                        testSuiteVm = _container.Resolve<TestSuiteViewModel>();

                        testSuiteVm.TestSuite = testSuite;

                        if (Items.FirstOrDefault(openedProject => openedProject.Equals(testSuiteVm)) == null) {
                            Items.Add(testSuiteVm);
                        }
                    } else {
                        testSuiteVm = Items[itemIndex] as TestSuiteViewModel;
                    }

                    ActiveItem = testSuiteVm;
                }
            }
        }

        private void OnStepChosen(ItemChosenEventDescriptor evtDescriptor) {
            if (evtDescriptor.ItemType == ChosenItemType.STEP) {
                var step = _stepsRepository.GetStep(evtDescriptor.Id);

                if (step != null) {
                    StepViewModel stepVm;
                    int itemIndex = _getItemIndex<StepViewModel>((vm) => { return vm != null && vm.Id == step.Id; });

                    if (itemIndex < 0) {
                        stepVm = _container.Resolve<StepViewModel>();

                        stepVm.Step = step;

                        if (Items.FirstOrDefault(openedProject => openedProject.Equals(stepVm)) == null) {
                            Items.Add(stepVm);
                        }
                    } else {
                        stepVm = Items[itemIndex] as StepViewModel;
                    }

                    ActiveItem = stepVm;
                }
            }
        }

        private int _getItemIndex<T>(Func<T, bool> compareFunc) where T : class, IViewModel {
            for (int i = 0; i < Items.Count; i++) {
                var vm = Items[i] as T;

                if (compareFunc(vm)) { 
                    return i;
                }
            }

            return -1;
        }

        #endregion

    }
}
