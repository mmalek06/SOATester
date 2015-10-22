using System.Collections.Generic;

using SOATester.Entities;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface IStepsRepository {
        Step GetStep(int id);

        IEnumerable<Step> GetStepsForTestSuite(TestSuite testSuite);

        IEnumerable<Step> GetStepsForTestSuite(int testSuiteId);
    }
}
