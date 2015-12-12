using Prism.Events;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ScenarioViewModel : HierarchicalViewModel<TestViewModel> {

        #region constructors and destructors

        public ScenarioViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

    }
}
