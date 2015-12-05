using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class TestsRepository : Repository<Test, Scenario> {

        #region methods

        protected override IQueryable<Test> GetEntityQuery(int id, SoaTesterContext ctx) {
            return from ts in ctx.Tests
                   where ts.Id == id
                   select ts;
        }

        protected override IQueryable<Test> GetByParentEntityQuery(int parentId, SoaTesterContext ctx) {
            return from ts in ctx.Tests
                   where ts.ScenarioId == parentId
                   select ts;
        }

        protected override int GetId(Test entity) {
            return entity.Id;
        }

        protected override int GetParentId(Scenario parent) {
            return parent.Id;
        }

        protected override int GetParentId(Test entity) {
            return entity.ScenarioId;
        }

        protected override void AddToContext(SoaTesterContext ctx, Test entity) {
            ctx.Tests.Add(entity);
        }

        #endregion

    }
}
