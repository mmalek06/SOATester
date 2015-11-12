using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SOATester.Modules.ProjectsListModule.Repositories {
    public class ProjectsRepository : IProjectsRepository {

        #region fields

        private IEnumerable<Project> _cache;

        #endregion

        #region public methods

        public IEnumerable<Project> GetProjects() {
            if (_cache == null) {
                _loadCache();
            }

            return _cache;
        }

        #endregion

        #region methods
        
        private void _loadCache() {
            using (var ctx = new SoaTesterContext()) {
#if DEBUG
                ctx.Database.Log = sql => Console.WriteLine(sql);
#endif

                var projects = ctx.Projects
                                  .Include(project => project.Scenarios
                                                             .Select(scenario => scenario.Tests
                                                                                         .Select(test => test.Steps)));

                _cache = projects.ToList();
            }
        }

        #endregion

    }
}
