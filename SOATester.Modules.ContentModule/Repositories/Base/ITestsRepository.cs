using SOATester.Entities;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface ITestsRepository {
        Test GetTest(int id);

        IEnumerable<Test> GetTestsForScenario(Scenario scenario);

        IEnumerable<Test> GetTestsForScenario(int scenarioId);
    }
}
