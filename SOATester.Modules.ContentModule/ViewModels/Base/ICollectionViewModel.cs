using System.Collections.ObjectModel;

namespace SOATester.Modules.ContentModule.ViewModels.Base {
    public interface ICollectionViewModel {
        ObservableCollection<object> Items { get; }
    }
}
