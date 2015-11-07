using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public class TestsRepository : ITestsRepository {
        private List<Test> _tests = new List<Test> {
            new Test {
                Id = 1,
                Name = "Basic tests"
            },
            new Test {
                Id = 2,
                Name = "Advanced tests"
            },
            new Test {
                Id = 3,
                Name = "Very basic tests"
            }
        };

        public Test GetTest(int id) {
            return _tests.FirstOrDefault(suite => suite.Id == id);
        }

        public IEnumerable<Test> GetTestsForScenario(int projectId) {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> GetTestsForScenario(Scenario project) {
            throw new NotImplementedException();
        }
    }
}
