using SOATester.Entities;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface IStepsRepository {
        Step GetStep(int id);

        IEnumerable<Step> GetStepsForTest(Test test);

        IEnumerable<Step> GetStepsForTest(int testId);
    }
}
