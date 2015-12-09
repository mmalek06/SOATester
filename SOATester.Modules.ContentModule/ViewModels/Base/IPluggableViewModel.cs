using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels.Base {
    public interface IPluggableViewModel {
        string Identity { get; }
        int Importance { get; }
        int Id { get; }
        int ParentId { get; }
        int TopmostParentId { get; }
        IDictionary<string, object> ViewProperties { get; }
    }
}
