using SOATester.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOATester.DAL.Repositories {
    public class TestsRepository : RepositoryBase<Test> {

        #region constructor

        public TestsRepository() {
            entities = dbContext.Tests;
        }

        #endregion

        #region public methods

        public override Test GetEntity(int id) {
            return dbContext.Tests.FirstOrDefault(test => test.Id == id);
        }

        public override IEnumerable<Step> GetRelatedEntities<Step>(int id) {
            return dbContext.Steps.Where(step => step.TestId == id).ToArray() as IEnumerable<Step>;
        }

        #endregion

    }
}
