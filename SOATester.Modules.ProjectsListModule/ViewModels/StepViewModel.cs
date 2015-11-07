using Prism.Events;
using SOATester.Modules.ProjectsListModule.ViewModels.Base;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class StepViewModel : ItemViewModel {

        #region constructors and destructors

        public StepViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {}

        #endregion

    }
}
