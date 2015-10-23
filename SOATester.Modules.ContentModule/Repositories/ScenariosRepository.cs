using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;

namespace SOATester.Modules.ContentModule.Repositories {
    public class ScenariosRepository : IScenariosRepository {
        private List<Scenario> _scenarios = new List<Scenario> {
            new Scenario {
                Id = 1,
                Name = "scenario1"
            },
            new Scenario {
                Id = 2,
                Name = "scenario2"
            },
            new Scenario {
                Id = 3,
                Name = "scenario3"
            }
        };

        public Scenario GetScenario(int id) {
            return _scenarios.FirstOrDefault(scenario => scenario.Id == id);
        }

        public IEnumerable<Scenario> GetScenariosForProject(int projectId) {
            throw new NotImplementedException();
        }

        public IEnumerable<Scenario> GetScenariosForProject(Project project) {
            throw new NotImplementedException();
        }
    }
}
