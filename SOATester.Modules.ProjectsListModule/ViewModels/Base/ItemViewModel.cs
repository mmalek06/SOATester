using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Infrastructure;

namespace SOATester.Modules.ProjectsListModule.ViewModels.Base {
    public abstract class ItemViewModel : ViewModelBase {

        #region fields

        protected string _name;

        #endregion

        #region properties

        [Dependency]
        public int Id { get; set; }

        [Dependency]
        public string Name {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion

        #region constructors and destructors

        public ItemViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {}

        #endregion

    }
}
