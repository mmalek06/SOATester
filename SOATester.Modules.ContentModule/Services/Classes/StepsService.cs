using SOATester.Entities;
using SOATester.DAL.Repositories;

namespace SOATester.Modules.ContentModule.Services {
    internal class StepsService : IStepsService {

        #region fields

        private StepsRepository repository;

        #endregion

        #region constructor

        public StepsService(StepsRepository repository) {
            this.repository = repository;
        }

        #endregion

        #region public methods

        public bool Add(Step obj) {
            return repository.Add(obj);
        }

        public Step Get(int id) {
            return repository.GetEntity(id);
        }

        #endregion

    }
}
