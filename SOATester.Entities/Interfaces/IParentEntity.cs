using System.Collections.Generic;

namespace SOATester.Entities {
    public interface IParentEntity {
        IEnumerable<object> Children { get; }
    }
}
