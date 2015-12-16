using Prism.Commands;
using SOATester.Entities;
using SOATester.Infrastructure.Events;
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

        public Project Project {
            get { return project; }
            protected set { SetProperty(ref project, value); }
        }

        public override string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public override int Importance => 1;

        public override int Id => project.Id;

        public override int ParentId => project.Id;

        public override int TopmostParentId => project.Id;

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

        protected override ChosenItemType MyType {
            get { return ChosenItemType.PROJECT; }
        }

        #endregion

        #region commands

        public DelegateCommand<string> SaveAddress { get; private set; }

        #endregion

        #region constructors and destructors

        public ProjectViewModel(IProjectsService projectsService) : base() {
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

        protected override void SetItem(ItemChosenEventDescriptor descriptor) {
            Project = projectsService.Get(descriptor.Id);
        }

        #endregion

        #region event handlers

        private void OnSaveAddress(string address) {
            Address = new Uri(address);
        }

        #endregion

    }
}
