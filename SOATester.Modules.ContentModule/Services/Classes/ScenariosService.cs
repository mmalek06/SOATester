using SOATester.Entities;
using SOATester.DAL.Repositories;

namespace SOATester.Modules.ContentModule.Services {
    internal class ScenariosService : IScenariosService {

        #region fields

        private ScenariosRepository repository;

        #endregion

        #region constructor

        public ScenariosService(ScenariosRepository repository) {
            this.repository = repository;
        }

        #endregion

        #region public methods

        public bool Add(Scenario obj) {
            return repository.Add(obj);
        }

        public Scenario Get(int id) {
            return repository.GetEntity(id);
        }

        #endregion

    }
}
