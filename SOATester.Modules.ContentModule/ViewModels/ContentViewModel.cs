using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Entities;
using SOATester.Infrastructure.Events;
using SOATester.Infrastructure.ViewModels;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ContentViewModel : ViewModelBase {

        #region fields

        private IPluggableViewModel selectedItem;
        private ObservableCollection<IPluggableViewModel> openedItems;
        private IUnityContainer container;
        private PluginFactory pluginFactory;
        private IEnumerable<IPlugin> Plugins;
        private bool hasAnyPlugins;

        #endregion

        #region properties

        public IPluggableViewModel SelectedItem {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public ObservableCollection<IPluggableViewModel> Items {
            get { return openedItems; }
            protected set { SetProperty(ref openedItems, value); }
        }

        #endregion

        #region commands

        public DelegateCommand<IPluggableViewModel> CloseItem { get; private set; }

        #endregion

        #region constructors and destructors

        public ContentViewModel(IEventAggregator eventAggregator, IUnityContainer container, PluginFactory pluginFactory) : base(eventAggregator) {
            Items = new ObservableCollection<IPluggableViewModel>();
            SelectedItem = null;
            this.container = container;
            this.pluginFactory = pluginFactory;

            Plugins = pluginFactory.GetActivePlugins();
            hasAnyPlugins = Plugins.Any();
        }

        #endregion

        #region methods

        protected override void InitEvents() {
            eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(OnProjectChosen);
            eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(OnScenarioChosen);
            eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(OnTestSuiteChosen);
            eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(OnStepChosen);
        }

        protected override void InitCommands() {
            CloseItem = new DelegateCommand<IPluggableViewModel>(OnItemClose);
        }

        #endregion

        #region event handlers

        private void OnItemClose(IPluggableViewModel vm) {
            OnItemRemoved(vm);
            Items.Remove(vm);
        }

        private void OnProjectChosen(ItemChosenEventDescriptor evtDescriptor) {
            if (evtDescriptor.ItemType == ChosenItemType.PROJECT) {
                var project = container.Resolve<ISimpleRepository<Project>>().GetEntity(evtDescriptor.Id);
                
                if (project != null) {
                    int itemIndex = GetItemIndex<ProjectViewModel>((vm) => { return vm != null && vm.Id == project.Id; });

                    if (itemIndex < 0) {
                        ProjectViewModel projectVm = container.Resolve<ProjectViewModel>();

                        projectVm.Project = project;

                        if (!Items.Any(openedProject => openedProject.Equals(projectVm))) {
                            OnItemAdded(projectVm);
                        }
                    }
                }
            }
        }

        private void OnScenarioChosen(ItemChosenEventDescriptor evtDescriptor) {
            if (evtDescriptor.ItemType == ChosenItemType.SCENARIO) {
                var scenario = container.Resolve<IRepository<Scenario, Project>>().GetEntity(evtDescriptor.Id);

                if (scenario != null) {
                    int itemIndex = GetItemIndex<ScenarioViewModel>(vm => vm != null && vm.Id == scenario.Id);

                    if (itemIndex < 0) {
                        ScenarioViewModel scenarioVm = container.Resolve<ScenarioViewModel>();

                        scenarioVm.Scenario = scenario;

                        if (!Items.Any(openedScenario => openedScenario.Equals(scenarioVm))) {
                            OnItemAdded(scenarioVm);
                        }
                    }
                }
            }
        }

        private void OnTestSuiteChosen(ItemChosenEventDescriptor evtDescriptor) {
            if (evtDescriptor.ItemType == ChosenItemType.TEST) {
                var testSuite = container.Resolve<IRepository<Test, Scenario>>().GetEntity(evtDescriptor.Id);
                
                if (testSuite != null) {
                    int itemIndex = GetItemIndex<TestViewModel>((vm) => { return vm != null && vm.Id == testSuite.Id; });

                    if (itemIndex < 0) {
                        TestViewModel testSuiteVm = container.Resolve<TestViewModel>();

                        testSuiteVm.Test = testSuite;

                        if (!Items.Any(openedProject => openedProject.Equals(testSuiteVm))) {
                            OnItemAdded(testSuiteVm);
                        }
                    }
                }
            }
        }

        private void OnStepChosen(ItemChosenEventDescriptor evtDescriptor) {
            if (evtDescriptor.ItemType == ChosenItemType.STEP) {
                var step = container.Resolve<IRepository<Step, Test>>().GetEntity(evtDescriptor.Id);

                if (step != null) {
                    int itemIndex = GetItemIndex<StepViewModel>((vm) => { return vm != null && vm.Id == step.Id; });

                    if (itemIndex < 0) {
                        StepViewModel stepVm = container.Resolve<StepViewModel>();

                        stepVm.Step = step;

                        if (!Items.Any(openedProject => openedProject.Equals(stepVm))) {
                            OnItemAdded(stepVm);
                        }
                    }
                }
            }
        }

        private void OnItemAdded(IPluggableViewModel newItem) {
            FillDefaultViewProperties(newItem);

            if (hasAnyPlugins) {
                RunPlugins(newItem);
            } else {
                Items.Add(newItem);
            }

            SelectItem(newItem);
        }

        private void OnItemRemoved(IPluggableViewModel oldItem) {
            var itemToRemove = Items.First(item => item.Equals(oldItem));

            UnselectItem(itemToRemove);
            Items.Remove(itemToRemove);
        }

        #endregion

        #region methods

        private void RunPlugins(IPluggableViewModel item) {
            var viewModels = new List<IPluggableViewModel>();

            IEnumerable<IPluggableViewModel> pluginExecutionResult = null;

            viewModels.AddRange(Items);
            viewModels.Add(item);

            foreach (var plugin in Plugins) {
                pluginExecutionResult = plugin.Execute(pluginExecutionResult == null ? viewModels : pluginExecutionResult);
            }

            Items.Clear();
            Items.AddRange(pluginExecutionResult == null ? viewModels : pluginExecutionResult);
        }

        private void SelectItem(IPluggableViewModel item) => SelectedItem = Items.FirstOrDefault(openedItem => openedItem.Equals(item));

        private void UnselectItem(IPluggableViewModel item) {
            if (item.Equals(SelectedItem)) {
                SelectedItem = null;
            }
        }

        private void FillDefaultViewProperties(IPluggableViewModel viewModel) {
            viewModel.ViewProperties["Brush"] = null;
        }

        private int GetItemIndex<T>(Func<T, bool> compareFunc) where T : ViewModelBase {
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
