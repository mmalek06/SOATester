using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;
using Prism.Commands;

using SOATester.Entities;

using SOATester.Infrastructure;

using SOATester.Modules.ContentModule.ViewModels.Base;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ProjectViewModel : ViewModelBase, IViewModel {

        #region fields

        private Project _project;
        private string _name;
        private Uri _address;
        private ObservableCollection<Parameter> _parameters;

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

        public int Id {
            get { return _project.Id; }
        }

        public string Name {
            get { return _name ?? Project.Name; }
            set { SetProperty(ref _name, value); }
        }

        public Uri Address {
            get { return _address ?? _project.Address; }
            set { SetProperty(ref _address, value); }
        }

        public ObservableCollection<Parameter> Parameters {
            get { return _parameters; }
            set { SetProperty(ref _parameters, value); }
        }
        
        #endregion

        #region commands

        public DelegateCommand<string> SaveAddress { get; private set; }

        #endregion

        #region constructors and destructors

        public ProjectViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {}

        #endregion

        #region public methods

        #endregion

        #region methods

        protected override void _initCollections() {
            Parameters = new ObservableCollection<Parameter>();
        }

        protected override void _initCommands() {
            SaveAddress = new DelegateCommand<string>(OnSaveAddress);
        }

        #endregion

        #region event handlers

        private void OnSaveAddress(string address) {
            Address = new Uri(address);
        }

        #endregion

    }
}
