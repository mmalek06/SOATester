using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.Repositories {
    public interface IRepository<T> : ISimpleRepository<T> {
        IEnumerable<T> GetEntitiesByParent(int parentId);

        void UpdateEntity(T entity);
    }
}
