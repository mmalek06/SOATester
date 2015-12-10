using Prism.Events;
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
