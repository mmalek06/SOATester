using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class StepsRepository : Repository<Step, Test> {

        #region methods

        protected override IQueryable<Step> _getEntityQuery(int id, SoaTesterContext ctx) {
            return from step in ctx.Steps
                   where step.Id == id
                   select step;
        }

        protected override IQueryable<Step> _getByParentEntityQuery(int parentId, SoaTesterContext ctx) {
            return from step in ctx.Steps
                   where step.TestId == parentId
                   select step;
        }

        protected override int _getId(Step entity) {
            return entity.Id;
        }

        protected override int _getParentId(Test parent) {
            return parent.Id;
        }

        protected override int _getParentId(Step entity) {
            return entity.TestId;
        }

        protected override void _addToContext(SoaTesterContext ctx, Step entity) {
            ctx.Steps.Add(entity);
        }

        #endregion

    }
}
