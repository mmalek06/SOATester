using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using Prism.Events;

namespace SOATester.Infrastructure {
    public abstract class ViewModelBase : BindableBase {
        #region fields

        protected IEventAggregator _eventAggregator;

        #endregion

        #region constructors and destructors

        public ViewModelBase(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;

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
