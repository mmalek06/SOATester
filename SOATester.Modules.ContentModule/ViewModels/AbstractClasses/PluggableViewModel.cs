using Prism.Regions;
using SOATester.Infrastructure.Events;
using SOATester.Infrastructure.ViewModels;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels {
    public abstract class PluggableViewModel : ViewModelBase, INavigationAware, INavigableViewModel {

        #region fields

        protected string identity;
        protected int importance;
        protected int id;
        protected int parentId;
        protected int topmostParentId;

        #endregion

        #region properties

        public virtual string Identity {
            get { return identity; }
            protected set { SetProperty(ref identity, value); }
        }

        public virtual int Importance {
            get { return importance; }
            protected set { SetProperty(ref importance, value); }
        }

        public virtual int Id {
            get { return id; }
            protected set { SetProperty(ref id, value); }
        }

        public virtual int ParentId {
            get { return parentId; }
            protected set { SetProperty(ref parentId, value); }
        }

        public virtual int TopmostParentId {
            get { return topmostParentId; }
            protected set { SetProperty(ref topmostParentId, value); }
        }

        public IDictionary<string, object> PluggableProperties { get; protected set; }

        protected abstract ChosenItemType MyType { get; }

        #endregion

        #region constructor

        public PluggableViewModel() : base() {
            PluggableProperties = new Dictionary<string, object>();

            PluggableProperties["Brush"] = null;
            PluggableProperties["Order"] = -1;
        }

        #endregion

        #region public methods

        public void InitializeWithContext(NavigationContext navCtx) {
            var descr = navCtx.Parameters["descriptor"] as ItemChosenEventDescriptor;

            if (descr.ItemType == MyType) {
                SetItem(descr);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) {
            var descr = navigationContext.Parameters["descriptor"] as ItemChosenEventDescriptor;

            if (descr.ItemType == MyType && descr.Id == Id) {
                return true;
            }

            return false;
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }

        #endregion

        #region methods

        protected abstract void SetItem(ItemChosenEventDescriptor descriptor);

        #endregion

    }
}
