using SOATester.Entities;
using SOATester.DAL.Repositories;

namespace SOATester.Modules.ContentModule.Services {
    internal class TestsService : ITestsService {

        #region fields

        private TestsRepository repository;

        #endregion

        #region constructor

        public TestsService(TestsRepository repository) {
            this.repository = repository;
        }

        #endregion

        #region public methods

        public bool Add(Test obj) {
            return repository.Add(obj);
        }

        public Test Get(int id) {
            return repository.GetEntity(id);
        }

        #endregion

    }
}
