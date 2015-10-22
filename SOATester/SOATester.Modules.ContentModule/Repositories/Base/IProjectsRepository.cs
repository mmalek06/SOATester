using SOATester.Entities;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface IProjectsRepository {
        Project GetProject(int id);
    }
}
