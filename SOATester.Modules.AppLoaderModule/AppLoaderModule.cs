using Microsoft.Practices.Unity;
using Prism.Regions;
using SOATester.Infrastructure;
using SOATester.Modules.AppLoaderModule.ViewModels;
using SOATester.Modules.AppLoaderModule.Views;

namespace SOATester.Modules.AppLoaderModule {
    public class AppLoaderModule : ModuleBase {

        #region constructors and destructors

        public AppLoaderModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region methods

        protected override void InitializeViewModels() {
            container.RegisterType<AppLoaderViewModel>();
        }

        protected override void InitializeViews() {
            container.RegisterType<AppLoaderView>();
        }

        protected override void InitializeRegions() {
            IRegion menuRegion = regionManager.Regions[RegionNames.SplashScreenRegion];

            menuRegion.Add(container.Resolve<AppLoaderView>());
        }

        #endregion

    }
}
