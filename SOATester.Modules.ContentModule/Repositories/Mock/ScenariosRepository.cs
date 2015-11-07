using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class ScenariosRepository : MockRepository<Scenario>, IScenariosRepository {

        #region constructors and destructors

        public ScenariosRepository() {
            _dataFileName = "scenarios_data.json";
        }

        #endregion

        #region public methods

        public Scenario GetScenario(int id) {
            return cache.FirstOrDefault(scenario => scenario.Id == id);
        }

        public IEnumerable<Scenario> GetScenariosForProject(int projectId) {
            return cache.Where(scenario => scenario.ProjectId == projectId);
        }

        public IEnumerable<Scenario> GetScenariosForProject(Project project) {
            return GetScenariosForProject(project.Id);
        }

        #endregion

    }
}
