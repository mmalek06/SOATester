using SOATester.Entities;
using System.Collections.Generic;

namespace SOATester.Modules.ProjectsListModule.Repositories.Base {
    public interface IProjectsRepository {
        IEnumerable<Project> GetProjects();
    }
}
