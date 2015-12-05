using SOATester.DAL;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class StepsRepository : Repository<Step, Test> {

        #region methods

        protected override IQueryable<Step> GetEntityQuery(int id, SoaTesterContext ctx) {
            return from step in ctx.Steps
                   where step.Id == id
                   select step;
        }

        protected override IQueryable<Step> GetByParentEntityQuery(int parentId, SoaTesterContext ctx) {
            return from step in ctx.Steps
                   where step.TestId == parentId
                   select step;
        }

        protected override int GetId(Step entity) {
            return entity.Id;
        }

        protected override int GetParentId(Test parent) {
            return parent.Id;
        }

        protected override int GetParentId(Step entity) {
            return entity.TestId;
        }

        protected override void AddToContext(SoaTesterContext ctx, Step entity) {
            ctx.Steps.Add(entity);
        }

        #endregion

    }
}
