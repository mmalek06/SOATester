using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOATester.Entities;

using SOATester.Modules.ContentModule.Repositories.Base;

namespace SOATester.Modules.ContentModule.Repositories {
    public class ProjectsRepository : IProjectsRepository {
        private List<Project> _projects = new List<Project> {
                new Project {
                    Id = 1,
                    Name = "Rest"
                },
                new Project {
                    Id = 2,
                    Name = "Soap"
                }
            };

        public Project GetProject(int id) {
            return _projects.FirstOrDefault(project => project.Id == id);
        }
    }
}
