using SOATester.DAL;
using SOATester.Entities;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class ScenariosRepository : Repository<Scenario> {

        #region methods

        protected override IQueryable<Scenario> GetEntityQuery(int id, SoaTesterContext ctx) {
            return from scenario in ctx.Scenarios
                   where scenario.Id == id
                   select scenario;
        }

        protected override IQueryable<Scenario> GetByParentEntityQuery(int parentId, SoaTesterContext ctx) {
            return from scenario in ctx.Scenarios
                   where scenario.ProjectId == parentId
                   select scenario;
        }

        protected override int GetId(Scenario entity) {
            return entity.Id;
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
