using Prism.Regions;

namespace SOATester.Infrastructure.ViewModels {
    public interface INavigableViewModel {
        void InitializeWithContext(NavigationContext navCtx);
    }
}
