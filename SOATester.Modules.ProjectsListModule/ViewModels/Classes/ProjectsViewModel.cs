using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using SOATester.Infrastructure;
using SOATester.Infrastructure.Events;
using SOATester.Infrastructure.ViewModels;
using SOATester.Modules.ProjectsListModule.Factories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ProjectsViewModel : ViewModelBase {

        #region fields

        private static object lockObj = new object();
        private ObservableCollection<IIdentifiableViewModel> tree;
        private IProjectsFactory projectsFactory;
        private IRegionManager regionManager;

        #endregion

        #region properties

        public ObservableCollection<IIdentifiableViewModel> Tree {
            get { return tree; }
            private set { SetProperty(ref tree, value); }
        }

        #endregion

        #region commands

        public DelegateCommand<ProjectViewModel> ChooseProject { get; private set; }
        public DelegateCommand<ScenarioViewModel> ChooseScenario { get; private set; }
        public DelegateCommand<TestViewModel> ChooseTestSuite { get; private set; }
        public DelegateCommand<StepViewModel> ChooseStep { get; private set; }
        
        #endregion

        #region constructors and destructors

        public ProjectsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IProjectsFactory projectsFactory) : base(eventAggregator) {
            this.projectsFactory = projectsFactory;
            this.regionManager = regionManager;

            InitComponent();
        }

        #endregion

        #region methods
        
        protected override void InitCollections() {
            Tree = new ObservableCollection<IIdentifiableViewModel>();
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
            var viewModels = new List<IIdentifiableViewModel>();
            
            await Task.Run(() => {
                var vmHierarchy = projectsFactory.CreateTreeStructure();

                viewModels.AddRange(vmHierarchy);
            });

            Tree.AddRange(viewModels);
        }

        #endregion

        #region event handlers

        private void OnItemChosen(int id, ChosenItemType itemType) {
            var evtDescriptor = new ItemChosenEventDescriptor {
                Id = id,
                ItemType = itemType
            };

            var navPath = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(evtDescriptor.ItemType.ToString().ToLower()) + "View";
            var navParameters = new NavigationParameters();

            navParameters.Add("descriptor", evtDescriptor);
            regionManager.RequestNavigate(RegionNames.ContentTabsRegion, navPath, navParameters);

            //eventAggregator.GetEvent<ItemOpenedEvent>().Publish(evtDescriptor);
        }

        #endregion

    }
}
