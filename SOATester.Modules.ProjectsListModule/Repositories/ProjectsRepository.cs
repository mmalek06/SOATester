using SOATester.Entities;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using System.Collections.Generic;

namespace SOATester.Modules.ProjectsListModule.Repositories {
    public class ProjectsRepository : IProjectsRepository {

        #region fields

        private SOATester.Repositories.ProjectsRepository _projectsRepository;

        #endregion

        #region constructors and destructors

        public ProjectsRepository(SOATester.Repositories.ProjectsRepository repository) {
            _projectsRepository = repository;
        }

        #endregion

        #region public methods

        public IEnumerable<Project> GetProjects() {
            return _projectsRepository.ProjectsCache;
        }

        #endregion

    }
}
