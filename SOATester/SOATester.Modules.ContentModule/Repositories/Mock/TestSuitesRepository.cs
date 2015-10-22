using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOATester.Entities;

using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class TestSuitesRepository : MockRepository<TestSuite>, ITestSuitesRepository {

        #region constructors and destructors

        public TestSuitesRepository() {
            _dataFileName = "test_suites_data.json";
        }

        #endregion

        #region public methods

        public TestSuite GetTestSuite(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<TestSuite> GetTestSuitesForProject(int projectId) {
            return cache.Where(testSuite => testSuite.ProjectId == projectId);
        }

        public IEnumerable<TestSuite> GetTestSuitesForProject(Project project) {
            return GetTestSuitesForProject(project.Id);
        }

        #endregion

    }
}
