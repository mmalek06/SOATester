using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using SOATester.Entities;

using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class ProjectsRepository : MockRepository<Project>, IProjectsRepository {

        #region constructors and destructors

        public ProjectsRepository() {
            _dataFileName = "projects_data.json";
        }

        #endregion

        #region public methods

        public Project GetProject(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        #endregion

    }
}
