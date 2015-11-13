using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.Repositories.Base {
    public interface IRepository<T, K> : ISimpleRepository<T> {
        IEnumerable<T> GetEntitiesByParent(K parent);

        IEnumerable<T> GetEntitiesByParent(int parentId);

        void UpdateEntity(T entity);
    }
}
