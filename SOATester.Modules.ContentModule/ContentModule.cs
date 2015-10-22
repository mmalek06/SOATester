using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

using SOATester.Infrastructure;

namespace SOATester.Modules.ContentModule {
    public class ContentModule : ModuleBase {

        #region constructors and destructors

        public ContentModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        #endregion

        #region non public methods

        protected override void _initializeRepositories() {
            
        }

        protected override void _initializeViewModels() {
            
        }

        protected override void _initializeViews() {
            
        }

        protected override void _initializeRegions() {
            
        }

        #endregion

    }
}
