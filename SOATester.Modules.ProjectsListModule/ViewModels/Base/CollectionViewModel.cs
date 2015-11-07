﻿using Prism.Events;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels.Base {
    public abstract class CollectionViewModel<T> : ItemViewModel {

        #region fields

        protected ObservableCollection<T> _items;
        
        #endregion

        #region properties

        public ObservableCollection<T> Items {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        #endregion

        #region constructors and destructors

        public CollectionViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

    }
}
