using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels.Base {
    public interface IPluggableViewModel {
        int Importance { get; }
        int Id { get; }
        int ParentId { get; }
        int TopmostParentId { get; }
        IDictionary<string, object> ViewProperties { get; }
    }
}
