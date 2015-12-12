using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Infrastructure.ViewModels;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class StepViewModel : HierarchicalViewModel<object> {

        #region constructors and destructors

        public StepViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {}

        #endregion

    }
}
