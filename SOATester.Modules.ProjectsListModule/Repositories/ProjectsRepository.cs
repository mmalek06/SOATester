using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SOATester.Modules.ProjectsListModule.Repositories {
    public class ProjectsRepository : IProjectsRepository {

        #region properties

        public IEnumerable<Project> ProjectsCache { get; set; }

        #endregion

        #region public methods

        public async Task LoadProjectsAsync() {
            await Task.Run(() => {
                using (var ctx = new SoaTesterContext()) {
                    var projects = ctx.Projects
                                      .Include(project => project.Scenarios.Select(scenario => scenario.Tests.Select(test => test.Steps)))
                                      .ToList();

                    ProjectsCache = projects;
                }
            });
        }

        #endregion

    }
}
