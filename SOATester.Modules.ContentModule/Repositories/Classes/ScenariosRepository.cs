using SOATester.DAL;
using SOATester.Entities;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class ScenariosRepository : Repository<Scenario, Project> {

        #region methods

        protected override IQueryable<Scenario> GetEntityQuery(int id, SoaTesterContext ctx) {
            return from sc in ctx.Scenarios
                   where sc.Id == id
                   select sc;
        }

        protected override IQueryable<Scenario> GetByParentEntityQuery(int parentId, SoaTesterContext ctx) {
            return from sc in ctx.Scenarios
                   where sc.ProjectId == parentId
                   select sc;
        }

        protected override int GetId(Scenario entity) {
            return entity.Id;
        }

        protected override int GetParentId(Project parent) {
            return parent.Id;
        }

        protected override int GetParentId(Scenario entity) {
            return entity.ProjectId;
        }

        protected override void AddToContext(SoaTesterContext ctx, Scenario entity) {
            ctx.Scenarios.Add(entity);
        }

        #endregion

    }
}
