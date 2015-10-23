using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;

namespace SOATester.Modules.ContentModule.Repositories {
    public class StepsRepository : IStepsRepository {
        private List<Step> _steps = new List<Step> {
                new Step { Id = 1, Name = "Step1" },
                new Step { Id = 2, Name = "Step2" },
                new Step { Id = 3, Name = "Step3" },
                new Step { Id = 4, Name = "Only one step" },
                new Step { Id = 5, Name = "OtherStep1" },
                new Step { Id = 6, Name = "OtherStep2" },
                new Step { Id = 7, Name = "OtherStep3" },
                new Step { Id = 8, Name = "OtherStep4" }
            };

        public Step GetStep(int id) {
            return _steps.FirstOrDefault(step => step.Id == id);
        }

        public IEnumerable<Step> GetStepsForTestSuite(int testSuiteId) {
            return null;
        }

        public IEnumerable<Step> GetStepsForTestSuite(TestSuite testSuite) {
            return GetStepsForTestSuite(testSuite.Id);
        }
    }
}
