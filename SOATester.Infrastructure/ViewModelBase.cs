using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace SOATester.Infrastructure {
    public abstract class ViewModelBase : BindableBase {
        #region fields

        protected IEventAggregator _eventAggregator;

        #endregion

        #region public properties

        public Dictionary<string, object> ViewProperties { get; set; }

        #endregion

        #region constructors and destructors

        public ViewModelBase(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;

            ViewProperties = new Dictionary<string, object>();

            _initCollections();
            _initEvents();
            _initCommands();
        }

        #endregion

        #region methods

        protected virtual void _initCollections() {}

        protected virtual void _initCommands() {}

        protected virtual void _initEvents() {}

        #endregion
    }
}
