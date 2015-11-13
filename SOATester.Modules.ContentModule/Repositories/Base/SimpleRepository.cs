using SOATester.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public abstract class SimpleRepository<T> : ISimpleRepository<T> {

        #region fields

        protected IDictionary<int, T> _cache = new Dictionary<int, T>();

        #endregion

        #region public methods

        public T GetEntity(int id) {
            if (_cache.ContainsKey(id)) {
                return _cache[id];
            }

            using (var ctx = new SoaTesterContext()) {
                var entity = _getEntityQuery(id, ctx).FirstOrDefault();

                if (entity != null) {
                    _cache[id] = entity;
                }

                return entity;
            }
        }

        #endregion

        #region methods

        protected abstract IQueryable<T> _getEntityQuery(int id, SoaTesterContext ctx);

        #endregion

    }
}
