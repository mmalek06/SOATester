using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Infrastructure.ViewModels;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public abstract class HierarchicalViewModel<T> : ViewModelBase {

        #region fields

        protected string name;
        protected ObservableCollection<T> items;

        #endregion

        #region properties

        [Dependency]
        public int Id { get; set; }

        [Dependency]
        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public ObservableCollection<T> Items {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        #endregion

        #region constructors and destructors

        public HierarchicalViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Items = new ObservableCollection<T>();
        }

        #endregion

    }
}
