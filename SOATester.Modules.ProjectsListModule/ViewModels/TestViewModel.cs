﻿using Prism.Events;
using SOATester.Modules.ProjectsListModule.ViewModels.Base;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class TestViewModel : CollectionViewModel<StepViewModel> {
        
        #region constructors and destructors

        public TestViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Items = new ObservableCollection<StepViewModel>();
        }

        #endregion

    }
}
