using Prism.Events;
using Prism.Mvvm;
using System;

namespace SOATester.Infrastructure.ViewModels {
    public abstract class ViewModelBase : BindableBase {

        #region fields

        protected IEventAggregator eventAggregator;

        #endregion

        #region constructors and destructors

        public ViewModelBase(IEventAggregator eventAggregator) {
            this.eventAggregator = eventAggregator;

            InitEvents();
            InitCommands();
            InitCollections();
        }

        #endregion

        #region methods

        protected virtual void InitEvents() { }
        protected virtual void InitCommands() { }
        protected virtual void InitCollections() { }

        #endregion

    }
}
