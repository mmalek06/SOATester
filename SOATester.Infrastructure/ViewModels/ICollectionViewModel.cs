using System.Collections.ObjectModel;

namespace SOATester.Infrastructure.ViewModels {
    public interface ICollectionViewModel {
        ObservableCollection<object> Items { get; }
    }
}
