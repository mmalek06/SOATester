using SOATester.Modules.ProjectsListModule.ViewModels;
using System.Collections.Generic;

namespace SOATester.Modules.ProjectsListModule.Factories {
    public interface IProjectsFactory {
        IEnumerable<IIdentifiableViewModel> CreateTreeStructure();
    }
}
