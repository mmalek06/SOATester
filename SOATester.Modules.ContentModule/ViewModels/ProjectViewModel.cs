using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Entities;
using SOATester.RestCommunication.Base;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ProjectViewModel : RunnableViewModel<Project>, IPluggableViewModel {

        #region fields

        private Project project;
        private string name;
        private Uri address;
        private ObservableCollection<RequestHeader> parameters;
        private Dictionary<string, object> viewProperties;

        #endregion

        #region properties

        public Project Project {
            get { return project; }
            set {
                if (project == null) {
                    SetProperty(ref project, value);
                }
            }
        }

        public string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public int Importance => 1;

        public int Id => project.Id;

        public int ParentId => project.Id;

        public int TopmostParentId => project.Id;

        public IDictionary<string, object> ViewProperties => viewProperties;

        public string Name {
            get { return name ?? Project.Name; }
            set { SetProperty(ref name, value); }
        }

        public Uri Address {
            get {
                if (address == null) {
                    if (project.Address != null) {
                        return new Uri(project.Address);
                    }

                    return null;
                }

                return address;
            }
            set { SetProperty(ref address, value); }
        }

        public ObservableCollection<RequestHeader> Parameters {
            get { return parameters; }
            set { SetProperty(ref parameters, value); }
        }
        
        #endregion

        #region commands

        public DelegateCommand<string> SaveAddress { get; private set; }

        #endregion

        #region constructors and destructors

        public ProjectViewModel(IEventAggregator eventAggregator, IUnityContainer container, IProjectsRunner runner) : base(eventAggregator, container, runner) {
            viewProperties = new Dictionary<string, object>();
        }

        #endregion

        #region methods

        protected override void InitCollections() {
            Parameters = new ObservableCollection<RequestHeader>();
        }

        protected override void InitCommands() {
            base.InitCommands();

            SaveAddress = new DelegateCommand<string>(OnSaveAddress);
        }

        #endregion

        #region event handlers

        private void OnSaveAddress(string address) {
            Address = new Uri(address);
        }

        protected async override void Run() {
            var result = await runner.RunAsync(Project);
        }

        protected async override void Stop() {
            await runner.StopAsync(Project);
        }

        protected async override void Pause() {
            await runner.PauseAsync(Project);
        }

        #endregion

    }
}
