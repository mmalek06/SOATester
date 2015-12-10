using SOATester.DAL;
using SOATester.Entities;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class ProjectsRepository : SimpleRepository<Project> {

        #region methods

        protected override IQueryable<Project> GetEntityQuery(int id, SoaTesterContext ctx) {
            return from proj in ctx.Projects
                   where proj.Id == id
                   select proj;
        }

        #endregion

    }
}
