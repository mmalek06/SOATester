using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace SOATester.Infrastructure {
    public abstract class ViewModelBase : BindableBase {
        #region fields

        protected IEventAggregator eventAggregator;

        #endregion

        #region public properties

        public Dictionary<string, object> ViewProperties { get; set; }

        #endregion

        #region constructors and destructors

        public ViewModelBase(IEventAggregator eventAggregator) {
            this.eventAggregator = eventAggregator;

            ViewProperties = new Dictionary<string, object>();

            InitCollections();
            InitEvents();
            InitCommands();
        }

        #endregion

        #region methods

        protected virtual void InitCollections() {}

        protected virtual void InitCommands() {}

        protected virtual void InitEvents() {}

        #endregion
    }
}
