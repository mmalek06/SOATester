using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOATester.Entities;

namespace SOATester.Modules.ProjectsListModule.Repositories.Base {
    public interface IProjectsRepository {
        IEnumerable<Project> GetProjects();
    }
}
