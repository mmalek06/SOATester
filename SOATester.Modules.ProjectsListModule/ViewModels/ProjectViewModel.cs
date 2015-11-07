using Prism.Events;
using SOATester.Modules.ProjectsListModule.ViewModels.Base;
using System.Collections.ObjectModel;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ProjectViewModel : CollectionViewModel<ScenarioViewModel> {

        #region constructors and destructors

        public ProjectViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {
            Items = new ObservableCollection<ScenarioViewModel>();
        }

        #endregion

    }
}
