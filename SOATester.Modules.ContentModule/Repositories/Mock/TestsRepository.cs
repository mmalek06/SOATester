using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class TestsRepository : MockRepository<Test> {

        #region constructors and destructors

        public TestsRepository() {
            dataFileName = "tests_data.json";
        }

        #endregion

        #region public methods

        public Test GetTest(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Test> GetTestsForScenario(int scenarioId) {
            return cache.Where(test => test.Scenario.Id == scenarioId);
        }

        public IEnumerable<Test> GetTestsForScenario(Scenario scenario) {
            return cache.Where(test => test.Scenario.Equals(scenario));
        }

        #endregion

    }
}
