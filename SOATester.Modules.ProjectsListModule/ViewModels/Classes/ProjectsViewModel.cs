using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Infrastructure.Events;
using SOATester.Infrastructure.ViewModels;
using SOATester.Modules.ProjectsListModule.Repositories;
using SOATester.Modules.ProjectsListModule.ViewModels.Builders;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ProjectsViewModel : ViewModelBase {

        #region fields

        private static object lockObj = new object();
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

        public ProjectsViewModel(IEventAggregator eventAggregator, IUnityContainer container) : base(eventAggregator) {
            this.container = container;

            InitComponent();
        }

        #endregion

        #region methods
        
        protected override void InitCollections() {
            Projects = new ObservableCollection<ProjectViewModel>();
        }

        protected override void InitCommands() {
            ChooseProject = new DelegateCommand<ProjectViewModel>((project) => { OnItemChosen(project.Id, ChosenItemType.PROJECT); });
            ChooseScenario = new DelegateCommand<ScenarioViewModel>((scenario) => { OnItemChosen(scenario.Id, ChosenItemType.SCENARIO); });
            ChooseTestSuite = new DelegateCommand<TestViewModel>((test) => { OnItemChosen(test.Id, ChosenItemType.TEST); });
            ChooseStep = new DelegateCommand<StepViewModel>((step) => { OnItemChosen(step.Id, ChosenItemType.STEP); });
        }

        private void InitComponent() {
            lock (lockObj) {
                NotifyOfStart();

                var task = InitProjectsAsync();

                NotifyOfEnd(task);
            }
        }

        private void NotifyOfStart() {
            const string Message = "Initializing project list";
            var startDescriptor = new StartupEventDescriptor {
                Message = Message,
                Activity = StartupActivity.PROJECTS_INIT
            };

            eventAggregator.GetEvent<StartupEventBegin>().Publish(startDescriptor);
        }

        private void NotifyOfEnd(Task task) {
            var endDescriptor = new StartupEventDescriptor {
                Activity = StartupActivity.PROJECTS_INIT
            };

            task.ContinueWith((result) => {
                Application.Current.Dispatcher.Invoke(() => {
                    eventAggregator.GetEvent<StartupEventEnd>().Publish(endDescriptor);
                });
            });
        }

        private async Task InitProjectsAsync() {
            var repository = container.Resolve<IProjectsRepository>();
            var viewModels = new List<ProjectViewModel>();
            
            await repository.LoadProjectsAsync();
            await Task.Run(() => {
                var vmBuilder = container.Resolve<HierarchicalViewModelBuilder>();

                viewModels.AddRange(vmBuilder.Build());
            });

            Projects.AddRange(viewModels);
        }

        #endregion

        #region event handlers

        private void OnItemChosen(int id, ChosenItemType itemType) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = id,
                ItemType = itemType
            };

            eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        #endregion

    }
}
