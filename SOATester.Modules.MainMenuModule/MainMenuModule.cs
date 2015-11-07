
using Microsoft.Practices.Unity;
using Prism.Regions;

using SOATester.Infrastructure;

namespace SOATester.Modules.MainMenuModule {
    public class MainMenuModule : ModuleBase {

        #region constructors and destructors

        public MainMenuModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void _initializeRegions() {
            base._initializeRegions();
        }

        protected override void _initializeViews() {
            base._initializeViews();
        }

        protected override void _initializeViewModels() {
            base._initializeViewModels();
        }

        #endregion

    }
}
