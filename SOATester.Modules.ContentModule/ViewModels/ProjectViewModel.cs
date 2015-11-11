using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Entities;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.RestCommunication.Base;
using System;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ProjectViewModel : RunnableViewModel<Project> {

        #region fields

        private Project _project;
        private string _name;
        private Uri _address;
        private ObservableCollection<RequestHeader> _parameters;

        #endregion

        #region properties

        public Project Project {
            get { return _project; }
            set {
                if (_project == null) {
                    SetProperty(ref _project, value);
                }
            }
        }

        public int Id => _project.Id;

        public string Name {
            get { return _name ?? Project.Name; }
            set { SetProperty(ref _name, value); }
        }

        public Uri Address {
            get { return _address == null ? new Uri(_project.Address) : _address; }
            set { SetProperty(ref _address, value); }
        }

        public ObservableCollection<RequestHeader> Parameters {
            get { return _parameters; }
            set { SetProperty(ref _parameters, value); }
        }
        
        #endregion

        #region commands

        public DelegateCommand<string> SaveAddress { get; private set; }

        #endregion

        #region constructors and destructors

        public ProjectViewModel(IEventAggregator eventAggregator, IUnityContainer container, IProjectsRunner runner) : base(eventAggregator, container, runner) {}

        #endregion

        #region public methods

        #endregion

        #region methods

        protected override void _initCollections() {
            Parameters = new ObservableCollection<RequestHeader>();
        }

        protected override void _initCommands() {
            base._initCommands();

            SaveAddress = new DelegateCommand<string>(OnSaveAddress);
        }

        #endregion

        #region event handlers

        private void OnSaveAddress(string address) {
            Address = new Uri(address);
        }

        protected async override void _run() {
            var result = await _runner.RunAsync(Project);
        }

        protected async override void _stop() {
            await _runner.StopAsync(Project);
        }

        protected async override void _pause() {
            await _runner.PauseAsync(Project);
        }

        #endregion

    }
}
