using SOATester.Entities;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface IScenariosRepository {
        Scenario GetScenario(int id);

        IEnumerable<Scenario> GetScenariosForProject(Project project);

        IEnumerable<Scenario> GetScenariosForProject(int projectId);
    }
}
