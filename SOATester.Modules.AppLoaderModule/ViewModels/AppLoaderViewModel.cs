using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Infrastructure.Events;
using SOATester.Infrastructure.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.AppLoaderModule.ViewModels {
    public class AppLoaderViewModel : ViewModelBase {

        #region fields

        private int progress;
        private int totalOperationsToRun;
        private int percentPerOperation;
        private ObservableCollection<string> runningOperations;
        private IList<StartupEventDescriptor> runningActivities;
        private IUnityContainer container;

        #endregion

        #region properties

        public int Progress {
            get { return progress; }
            set { SetProperty(ref progress, value); }
        }

        public ObservableCollection<string> RunningOperations {
            get { return runningOperations; }
            set { SetProperty(ref runningOperations, value); }
        }

        #endregion

        #region constructors and destructors

        public AppLoaderViewModel(IEventAggregator eventAggregator, IUnityContainer container) : base(eventAggregator) {
            Progress = 0;
            totalOperationsToRun = 2;
            percentPerOperation = 100 / totalOperationsToRun;
            runningActivities = new List<StartupEventDescriptor>();
            RunningOperations = new ObservableCollection<string>();
            this.container = container;
        }

        #endregion

        #region methods

        protected override void InitEvents() {
            eventAggregator.GetEvent<StartupEventBegin>().Subscribe(OnComponentStart);
            eventAggregator.GetEvent<StartupEventEnd>().Subscribe(OnComponentEnd);
        }

        #endregion

        #region event handlers

        private void OnComponentStart(StartupEventDescriptor descriptor) {
            runningActivities.Add(descriptor);
            RunningOperations.Add(descriptor.Message);
        }

        private void OnComponentEnd(StartupEventDescriptor descriptor) {
            var activity = runningActivities.FirstOrDefault(act => act.Activity == descriptor.Activity);

            if (activity != null) {
                RunningOperations.Remove(activity.Message);
                runningActivities.Remove(activity);
                Progress += percentPerOperation;

                if (!runningActivities.Any()) {
                    eventAggregator.GetEvent<BootingCompleted>().Publish(true);
                }
            }
        }

        #endregion

        /*public void InitializeApplication() {
            var tasks = new Task[2];

            tasks[0] = InitWorkspaceAsync();
            tasks[1] = InitProjectsAsync();
                
            Task.WhenAll(tasks).ContinueWith((result) => {
                Application.Current.Dispatcher.Invoke(() => {
                    eventAggregator.GetEvent<BootingCompleted>().Publish(true);
                });
            });
        }

        private async Task InitProjectsAsync() {
            const string Activity = "Initializing project list";

            RunningOperations.Add(Activity);

            var repository = container.Resolve<ProjectsRepository>();

            await repository.LoadProjectsAsync();

            RunningOperations.Remove(Activity);
            eventAggregator.GetEvent<StartupEventEnd>().Publish(StartupActivity.PROJECTS_INIT);
        }

        private async Task InitWorkspaceAsync() {
            const string Activity = "Initializing user workspace";

            RunningOperations.Add(Activity);

            await Task.Run(() => {
                //Thread.Sleep(5000);
            });

            RunningOperations.Remove(Activity);
            eventAggregator.GetEvent<StartupEventEnd>().Publish(StartupActivity.WORKSPACE_INIT);
        }*/

    }
}
