using System.Collections.Generic;
using System.Data.Entity;

namespace SOATester.DAL.Repositories {
    public abstract class RepositoryBase<T> where T : class {

        #region fields

        protected DbSet<T> entities;
        protected static SoaTesterContext dbContext;

        #endregion

        #region constructor

        static RepositoryBase() {
            dbContext = new SoaTesterContext();
        }

        #endregion

        #region public methods

        public bool Add(T entity) {
            entities.Add(entity);

            return dbContext.SaveChanges() > 0;
        }

        public abstract T GetEntity(int id);

        public abstract IEnumerable<K> GetRelatedEntities<K>(int id);

        #endregion

    }
}
