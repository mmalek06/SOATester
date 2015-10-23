using System.Collections.Generic;

using SOATester.Entities;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface IScenariosRepository {
        Scenario GetScenario(int id);

        IEnumerable<Scenario> GetScenariosForProject(Project project);

        IEnumerable<Scenario> GetScenariosForProject(int projectId);
    }
}
