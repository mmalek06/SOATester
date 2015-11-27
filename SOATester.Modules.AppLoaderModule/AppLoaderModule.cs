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

        protected override void _initializeViewModels() {
            _container.RegisterType<AppLoaderViewModel>();
        }

        protected override void _initializeViews() {
            _container.RegisterType<AppLoaderView>();
        }

        protected override void _initializeRegions() {
            IRegion menuRegion = _regionManager.Regions[RegionNames.SplashScreenRegion];

            menuRegion.Add(_container.Resolve<AppLoaderView>());
        }

        #endregion

    }
}
