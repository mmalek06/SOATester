using SOATester.DAL;
using SOATester.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SOATester.DAL.Repositories {
    public class ProjectsRepository1 {

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
