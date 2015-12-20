using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity.Regions;
using SOATester.Infrastructure.ViewModels;
using System.Windows.Controls;

namespace SOATester.Infrastructure.Prism {
    public class SoaTesterUnityRegionNavigationContentLoader : UnityRegionNavigationContentLoader {

        public NavigationContext CurrentNavigationContext { get; set; }

        public SoaTesterUnityRegionNavigationContentLoader(IServiceLocator serviceLocator, IUnityContainer container) : base(serviceLocator, container) { }

        protected override object CreateNewRegionItem(string candidateTargetContract) {
            var view = base.CreateNewRegionItem(candidateTargetContract);

            if (view is UserControl) {
                var userControl = (UserControl)view;

                if (userControl.DataContext is INavigableViewModel) {
                    var navigableVm = (INavigableViewModel)userControl.DataContext;

                    navigableVm.InitializeWithContext(CurrentNavigationContext);
                }
            }

            return view;
        }

    }
}
