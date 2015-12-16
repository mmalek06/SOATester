using Prism.Mvvm;
using SOATester.Entities;
using System.Collections.Generic;

namespace SOATester.Modules.ProjectsListModule.ViewModels {
    public class StepViewModel : BindableBase, IIdentifiableViewModel {

        #region properties

        public int Id { get; private set; }
        public string Name { get; private set; }

        #endregion

        #region constructors and destructors

        public StepViewModel(Step step) : base() {
            Id = step.Id;
            Name = step.Name;
        }

        #endregion

    }
}
