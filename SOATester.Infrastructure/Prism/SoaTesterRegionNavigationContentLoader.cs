using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace SOATester.Infrastructure.Prism {
    public class SoaTesterRegionNavigationContentLoader : IRegionNavigationContentLoader {

        private SoaTesterUnityRegionNavigationContentLoader contentLoader;

        public SoaTesterRegionNavigationContentLoader(IServiceLocator serviceLocator, IUnityContainer container) {
            contentLoader = new SoaTesterUnityRegionNavigationContentLoader(serviceLocator, container);
        }

        public object LoadContent(IRegion region, NavigationContext navigationContext) {
            contentLoader.CurrentNavigationContext = navigationContext;
            var view = contentLoader.LoadContent(region, navigationContext);
            contentLoader.CurrentNavigationContext = null;

            return view;
        }

    }
}
