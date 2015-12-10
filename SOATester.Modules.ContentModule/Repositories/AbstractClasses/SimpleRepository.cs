using SOATester.DAL;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories {
    public abstract class SimpleRepository<T> : ISimpleRepository<T> {

        #region fields

        protected IDictionary<int, T> cache = new Dictionary<int, T>();

        #endregion

        #region public methods

        public T GetEntity(int id) {
            if (cache.ContainsKey(id)) {
                return cache[id];
            }

            using (var ctx = new SoaTesterContext()) {
                var entity = GetEntityQuery(id, ctx).FirstOrDefault();

                if (entity != null) {
                    cache[id] = entity;
                }

                return entity;
            }
        }

        #endregion

        #region methods

        protected abstract IQueryable<T> GetEntityQuery(int id, SoaTesterContext ctx);

        #endregion

    }
}
