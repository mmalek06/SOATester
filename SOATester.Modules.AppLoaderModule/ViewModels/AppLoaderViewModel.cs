using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Infrastructure;
using SOATester.Infrastructure.Events;
using SOATester.Repositories;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Windows;

namespace SOATester.Modules.AppLoaderModule.ViewModels {
    public class AppLoaderViewModel : ViewModelBase {

        #region fields

        private int _progress;
        private ObservableCollection<string> runningOperations;
        private IUnityContainer container;

        #endregion

        #region properties

        public int Progress {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        public ObservableCollection<string> RunningOperations {
            get { return runningOperations; }
            set { SetProperty(ref runningOperations, value); }
        }

        #endregion

        #region constructors and destructors

        public AppLoaderViewModel(IEventAggregator eventAggregator, IUnityContainer container) : base(eventAggregator) {
            Progress = 0;
            RunningOperations = new ObservableCollection<string>();
            this.container = container;

            InitializeApplication();
        }

        #endregion

        #region methods

        public void InitializeApplication() {
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
        }

        #endregion

    }
}
