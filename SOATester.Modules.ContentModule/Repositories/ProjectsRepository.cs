using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class ProjectsRepository : SimpleRepository<Project> {

        #region methods

        protected override IQueryable<Project> _getEntityQuery(int id, SoaTesterContext ctx) {
            return from proj in ctx.Projects
                   where proj.Id == id
                   select proj;
        }

        #endregion

    }
}
