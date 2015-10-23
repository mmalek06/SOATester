using System.Collections.Generic;

using SOATester.Entities;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface ITestSuitesRepository {
        TestSuite GetTestSuite(int id);

        IEnumerable<TestSuite> GetTestSuitesForProject(Project project);

        IEnumerable<TestSuite> GetTestSuitesForProject(int projectId);
    }
}
