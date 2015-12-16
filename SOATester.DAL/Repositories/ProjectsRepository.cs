using SOATester.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.DAL.Repositories {
    public class ProjectsRepository : RepositoryBase<Project> {

        #region constructor

        public ProjectsRepository() {
            entities = dbContext.Projects;
        }

        #endregion

        #region public methods

        public IEnumerable<Project> GetProjects() {
            return dbContext.Projects.ToList();
        }

        public override Project GetEntity(int id) {
            return dbContext.Projects.FirstOrDefault(project => project.Id == id);
        }

        public override IEnumerable<Scenario> GetRelatedEntities<Scenario>(int id) {
            return dbContext.Scenarios.Where(scenario => scenario.ProjectId == id).ToArray() as IEnumerable<Scenario>;
        }

        #endregion

    }
}
