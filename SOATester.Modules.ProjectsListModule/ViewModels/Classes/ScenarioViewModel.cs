using Prism.Events;
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
