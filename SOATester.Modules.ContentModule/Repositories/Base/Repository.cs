using SOATester.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public abstract class Repository<T, K> : SimpleRepository<T>, IRepository<T, K> {

        #region fields

        protected IDictionary<int, IList<T>> _cacheByParent = new Dictionary<int, IList<T>>();

        #endregion

        #region public methods

        public IEnumerable<T> GetEntitiesByParent(K parent) {
            return GetEntitiesByParent(_getParentId(parent));
        }

        public IEnumerable<T> GetEntitiesByParent(int parentId) {
            if (_cacheByParent.ContainsKey(parentId)) {
                return _cacheByParent[parentId];
            }

            using (var ctx = new SoaTesterContext()) {
                var entities = _getByParentEntityQuery(parentId, ctx).ToArray();

                if (entities.Any()) {
                    _cacheByParent[parentId] = entities;
                }

                return entities;
            }
        }

        public void UpdateEntity(T entity) {
            using (var ctx = new SoaTesterContext()) {
                _addToContext(ctx, entity);
                ctx.SaveChanges();

                _cache[_getId(entity)] = entity;

                int parentId = _getParentId(entity);

                if (!_cacheByParent.ContainsKey(parentId)) {
                    _cacheByParent[parentId] = new List<T>();
                }

                _cacheByParent[parentId].Add(entity);
            }
        }

        #endregion

        #region methods

        protected abstract IQueryable<T> _getByParentEntityQuery(int parentId, SoaTesterContext ctx);

        protected abstract int _getId(T entity);

        protected abstract int _getParentId(K parent);

        protected abstract int _getParentId(T entity);

        protected abstract void _addToContext(SoaTesterContext ctx, T entity);

        #endregion

    }
}
