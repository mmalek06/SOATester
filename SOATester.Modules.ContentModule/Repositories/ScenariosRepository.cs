using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class ScenariosRepository : Repository<Scenario, Project> {

        #region methods

        protected override IQueryable<Scenario> _getEntityQuery(int id, SoaTesterContext ctx) {
            return from sc in ctx.Scenarios
                   where sc.Id == id
                   select sc;
        }

        protected override IQueryable<Scenario> _getByParentEntityQuery(int parentId, SoaTesterContext ctx) {
            return from sc in ctx.Scenarios
                   where sc.ProjectId == parentId
                   select sc;
        }

        protected override int _getId(Scenario entity) {
            return entity.Id;
        }

        protected override int _getParentId(Project parent) {
            return parent.Id;
        }

        protected override int _getParentId(Scenario entity) {
            return entity.ProjectId;
        }

        protected override void _addToContext(SoaTesterContext ctx, Scenario entity) {
            ctx.Scenarios.Add(entity);
        }

        #endregion

    }
}
