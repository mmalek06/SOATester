using Prism.Commands;
using Prism.Regions;
using SOATester.Entities;
using SOATester.Infrastructure.Events;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Services;
using System;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ProjectViewModel : PluggableViewModel {

        #region fields

        private Project project;
        private string name;
        private Uri address;
        private ObservableCollection<RequestHeader> parameters;
        private IProjectsService projectsService;

        #endregion

        #region properties

        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public Uri Address {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        public ObservableCollection<RequestHeader> Parameters {
            get { return parameters; }
            set { SetProperty(ref parameters, value); }
        }

        protected override ChosenItemType MyType {
            get { return ChosenItemType.PROJECT; }
        }

        #endregion

        #region commands

        public DelegateCommand<string> SaveAddress { get; private set; }

        #endregion

        #region constructors and destructors

        public ProjectViewModel(IProjectsService projectsService, PluginRunner runner) : base(runner) {
            this.projectsService = projectsService;
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

        protected override void BeforeNavigation(NavigationContext context) {
            int chosenId = (int)context.Parameters["id"];

            project = projectsService.Get(chosenId);
            Id = project.Id;
            ParentId = Id;
            TopmostParentId = Id;
            Name = project.Name;
            importance = 1;
            Identity = string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);
        }

        #endregion

        #region event handlers

        private void OnSaveAddress(string address) {
            Address = new Uri(address);
        }

        #endregion

    }
}
