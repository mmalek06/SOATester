using Prism.Regions;
using SOATester.Infrastructure.Events;
using SOATester.Infrastructure.ViewModels;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels {
    public abstract class PluggableViewModel : ViewModelBase, INavigationAware {

        #region properties

        public virtual string Identity { get; }
        public virtual int Importance { get; }
        public virtual int Id { get; }
        public virtual int ParentId { get; }
        public virtual int TopmostParentId { get; }
        public IDictionary<string, object> ViewProperties { get; protected set; }
        protected abstract ChosenItemType MyType { get; }

        #endregion

        #region constructor

        public PluggableViewModel() : base() {
            ViewProperties = new Dictionary<string, object>();

            ViewProperties["Brush"] = null;
            ViewProperties["Order"] = -1;
        }

        #endregion

        #region public methods

        public void OnNavigatedTo(NavigationContext navigationContext) {
            var descr = navigationContext.Parameters["descriptor"] as ItemChosenEventDescriptor;

            if (descr.ItemType == MyType) {
                SetItem(descr);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) {
            var descr = navigationContext.Parameters["descriptor"] as ItemChosenEventDescriptor;

            if (descr.ItemType == MyType) {
                return true;
            }

            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        #endregion

        #region methods

        protected abstract void SetItem(ItemChosenEventDescriptor descriptor);

        #endregion

    }
}
