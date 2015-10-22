using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Modules.ProjectsListModule.ViewModels.Base;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ProjectViewModel : CollectionViewModel<TestSuiteViewModel> {

        #region constructors and destructors

        public ProjectViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Items = new ObservableCollection<TestSuiteViewModel>();
        }

        #endregion

    }
}
