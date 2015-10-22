using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;

namespace SOATester.Modules.ContentModule.Repositories {
    public class TestSuitesRepository : ITestSuitesRepository {
        private List<TestSuite> _suites = new List<TestSuite> {
                new TestSuite {
                    Id = 1,
                    Name = "Basic tests"
                },
                new TestSuite {
                    Id = 2,
                    Name = "Advanced tests"
                },
                new TestSuite {
                    Id = 3,
                    Name = "Very basic tests"
                }
            };

        public TestSuite GetTestSuite(int id) {
            return _suites.FirstOrDefault(suite => suite.Id == id);
        }

        public IEnumerable<TestSuite> GetTestSuitesForProject(int projectId) {
            throw new NotImplementedException();
        }

        public IEnumerable<TestSuite> GetTestSuitesForProject(Project project) {
            throw new NotImplementedException();
        }
    }
}
