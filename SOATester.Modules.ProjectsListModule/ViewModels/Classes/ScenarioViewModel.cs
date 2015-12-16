using Prism.Mvvm;
using SOATester.Entities;
using System.Collections.Generic;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class ScenarioViewModel : BindableBase, IHierarchicalViewModel, IIdentifiableViewModel {

        #region properties

        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<object> Items { get; set; }

        #endregion

        #region constructors and destructors

        public ScenarioViewModel(Scenario scenario) : base() {
            Id = scenario.Id;
            Name = scenario.Name;
            Items = new List<object>();
        }

        #endregion

    }
}
