using SOATester.DAL;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public abstract class Repository<T, K> : SimpleRepository<T>, IRepository<T, K> {

        #region fields

        protected IDictionary<int, IList<T>> cacheByParent = new Dictionary<int, IList<T>>();

        #endregion

        #region public methods

        public IEnumerable<T> GetEntitiesByParent(K parent) {
            return GetEntitiesByParent(GetParentId(parent));
        }

        public IEnumerable<T> GetEntitiesByParent(int parentId) {
            if (cacheByParent.ContainsKey(parentId)) {
                return cacheByParent[parentId];
            }

            using (var ctx = new SoaTesterContext()) {
                var entities = GetByParentEntityQuery(parentId, ctx).ToArray();

                if (entities.Any()) {
                    cacheByParent[parentId] = entities;
                }

                return entities;
            }
        }

        public void UpdateEntity(T entity) {
            using (var ctx = new SoaTesterContext()) {
                AddToContext(ctx, entity);
                ctx.SaveChanges();

                cache[GetId(entity)] = entity;

                int parentId = GetParentId(entity);

                if (!cacheByParent.ContainsKey(parentId)) {
                    cacheByParent[parentId] = new List<T>();
                }

                cacheByParent[parentId].Add(entity);
            }
        }

        #endregion

        #region methods

        protected abstract IQueryable<T> GetByParentEntityQuery(int parentId, SoaTesterContext ctx);

        protected abstract int GetId(T entity);

        protected abstract int GetParentId(K parent);

        protected abstract int GetParentId(T entity);

        protected abstract void AddToContext(SoaTesterContext ctx, T entity);

        #endregion

    }
}
