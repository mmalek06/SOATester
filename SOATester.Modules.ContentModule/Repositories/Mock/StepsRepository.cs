using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class StepsRepository : MockRepository<Step>, IStepsRepository {

        #region constructors and destructors

        public StepsRepository() {
            _dataFileName = "steps_data.json";
        }

        #endregion
        
        #region public methods

        public Step GetStep(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Step> GetStepsForTest(int testSuiteId) {
            return cache.Where(step => step.TestId == testSuiteId);
        }

        public IEnumerable<Step> GetStepsForTest(Test testSuite) {
            return GetStepsForTest(testSuite.Id);
        }

        #endregion

    }
}
