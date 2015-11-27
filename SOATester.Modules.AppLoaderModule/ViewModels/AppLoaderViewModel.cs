using Prism.Events;
using SOATester.Infrastructure;
using SOATester.Infrastructure.Events.EventClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOATester.Modules.AppLoaderModule.ViewModels {
    public class AppLoaderViewModel : ViewModelBase {

        #region fields

        private int _progress;
        private ObservableCollection<string> _runningOperations;

        #endregion

        #region properties

        public int Progress {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        public ObservableCollection<string> RunningOperations {
            get { return _runningOperations; }
            set { SetProperty(ref _runningOperations, value); }
        }

        #endregion

        #region constructors and destructors

        public AppLoaderViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Progress = 0;
            RunningOperations = new ObservableCollection<string>();

            _initWorkspaceAsync();
        }

        #endregion

        #region methods

        private async void _initWorkspaceAsync() {
            const string Activity = "Initializing workspace";

            RunningOperations.Add(Activity);

            await Task.Run(() => {
                for (int i = 0; i < 5; i++) {
                    Thread.Sleep(1000);

                    Progress += 25;
                }
            });

            RunningOperations.Remove(Activity);

            _eventAggregator.GetEvent<BootingCompleted>().Publish(true);
        }

        #endregion

    }
}
