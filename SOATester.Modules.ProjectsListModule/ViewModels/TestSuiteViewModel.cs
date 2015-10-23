using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Modules.ProjectsListModule.ViewModels.Base;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class TestViewModel : CollectionViewModel<StepViewModel> {
        
        #region constructors and destructors

        public TestViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Items = new ObservableCollection<StepViewModel>();
        }

        #endregion

    }
}
