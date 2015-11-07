using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class TestsRepository : MockRepository<Test>, ITestsRepository {

        #region constructors and destructors

        public TestsRepository() {
            _dataFileName = "tests_data.json";
        }

        #endregion

        #region public methods

        public Test GetTest(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Test> GetTestsForScenario(int scenarioId) {
            return cache.Where(test => test.ScenarioId == scenarioId);
        }

        public IEnumerable<Test> GetTestsForScenario(Scenario scenario) {
            return GetTestsForScenario(scenario.Id);
        }

        #endregion

    }
}
