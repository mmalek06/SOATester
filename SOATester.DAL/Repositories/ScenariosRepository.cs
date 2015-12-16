using SOATester.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.DAL.Repositories {
    public class ScenariosRepository : RepositoryBase<Scenario> {

        #region constructor

        public ScenariosRepository() {
            entities = dbContext.Scenarios;
        }

        #endregion

        #region public methods

        public override Scenario GetEntity(int id) {
            return dbContext.Scenarios.FirstOrDefault(scenario => scenario.Id == id);
        }

        public override IEnumerable<Test> GetRelatedEntities<Test>(int id) {
            return dbContext.Tests.Where(test => test.ScenarioId == id).ToArray() as IEnumerable<Test>;
        }

        #endregion

    }
}
