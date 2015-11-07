using Prism.Events;
using SOATester.Modules.ProjectsListModule.ViewModels.Base;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ScenarioViewModel : CollectionViewModel<TestViewModel> {

        #region constructors and destructors

        public ScenarioViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Items = new ObservableCollection<TestViewModel>();
        }

        #endregion

    }
}
