using System.Collections.Generic;

using SOATester.Entities;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface ITestsRepository {
        Test GetTest(int id);

        IEnumerable<Test> GetTestsForScenario(Scenario scenario);

        IEnumerable<Test> GetTestsForScenario(int scenarioId);
    }
}
