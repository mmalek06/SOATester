using SOATester.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Modules.ProjectsListModule.Repositories {
    public interface IProjectsRepository {
        IEnumerable<Project> ProjectsCache { get; set; }
        Task LoadProjectsAsync();
    }
}
