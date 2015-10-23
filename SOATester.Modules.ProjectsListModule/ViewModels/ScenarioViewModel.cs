using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Modules.ProjectsListModule.ViewModels.Base;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ScenarioViewModel : CollectionViewModel<TestViewModel> {

        #region constructors and destructors

        public ScenarioViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Items = new ObservableCollection<TestViewModel>();
        }

        #endregion

    }
}
