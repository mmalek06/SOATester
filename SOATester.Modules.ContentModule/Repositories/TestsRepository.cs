using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class TestsRepository : Repository<Test, Scenario> {

        #region methods

        protected override IQueryable<Test> _getEntityQuery(int id, SoaTesterContext ctx) {
            return from ts in ctx.Tests
                   where ts.Id == id
                   select ts;
        }

        protected override IQueryable<Test> _getByParentEntityQuery(int parentId, SoaTesterContext ctx) {
            return from ts in ctx.Tests
                   where ts.ScenarioId == parentId
                   select ts;
        }

        protected override int _getId(Test entity) {
            return entity.Id;
        }

        protected override int _getParentId(Scenario parent) {
            return parent.Id;
        }

        protected override int _getParentId(Test entity) {
            return entity.ScenarioId;
        }

        protected override void _addToContext(SoaTesterContext ctx, Test entity) {
            ctx.Tests.Add(entity);
        }

        #endregion

    }
}
