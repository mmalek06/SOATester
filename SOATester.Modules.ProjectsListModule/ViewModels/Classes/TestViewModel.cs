using Prism.Events;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class TestViewModel : HierarchicalViewModel<StepViewModel> {
        
        #region constructors and destructors

        public TestViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

    }
}
