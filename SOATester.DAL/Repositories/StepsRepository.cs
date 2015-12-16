using SOATester.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SOATester.DAL.Repositories {
    public class StepsRepository : RepositoryBase<Step> {

        #region constructor

        public StepsRepository() {
            entities = dbContext.Steps;
        }

        #endregion

        #region public methods

        public override Step GetEntity(int id) {
            return dbContext.Steps.FirstOrDefault(step => step.Id == id);
        }

        public override IEnumerable<K> GetRelatedEntities<K>(int id) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
