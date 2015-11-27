
using Microsoft.Practices.Unity;
using Prism.Regions;

using SOATester.Infrastructure;

namespace SOATester.Modules.MainMenuModule {
    public class MainMenuModule : ModuleBase {

        #region constructors and destructors

        public MainMenuModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void InitializeRegions() {
            base.InitializeRegions();
        }

        protected override void InitializeViews() {
            base.InitializeViews();
        }

        protected override void InitializeViewModels() {
            base.InitializeViewModels();
        }

        #endregion

    }
}
