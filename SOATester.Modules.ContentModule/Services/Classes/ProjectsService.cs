using SOATester.Entities;
using SOATester.DAL.Repositories;

namespace SOATester.Modules.ContentModule.Services {
    internal class ProjectsService : IProjectsService {

        #region fields

        private ProjectsRepository repository;

        #endregion

        #region constructor

        public ProjectsService(ProjectsRepository repository) {
            this.repository = repository;
        }

        #endregion

        #region public methods

        public bool Add(Project obj) {
            return repository.Add(obj);
        }

        public Project Get(int id) {
            return repository.GetEntity(id);
        }

        #endregion

    }
}
