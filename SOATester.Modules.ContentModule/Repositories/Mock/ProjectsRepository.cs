using SOATester.Entities;
using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class ProjectsRepository : MockRepository<Project> {

        #region constructors and destructors

        public ProjectsRepository() {
            dataFileName = "projects_data.json";
        }

        #endregion

        #region public methods

        public Project GetProject(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        #endregion

    }
}
