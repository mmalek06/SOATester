using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class StepsRepository : MockRepository<Step> {

        #region constructors and destructors

        public StepsRepository() {
            _dataFileName = "steps_data.json";
        }

        #endregion
        
        #region public methods

        public Step GetStep(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Step> GetStepsForTest(int testId) {
            return cache.Where(step => step.Test.Id == testId);
        }

        public IEnumerable<Step> GetStepsForTest(Test test) {
            return cache.Where(step => step.Test.Equals(test));
        }

        #endregion

    }
}
