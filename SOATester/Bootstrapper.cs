using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using SOATester.Infrastructure.Prism;
using System;
using System.Windows;

namespace SOATester {
    public enum Shells { MAIN_SHELL }

    public class Bootstrapper : UnityBootstrapper {

        #region constants

        private const string MAIN_SHELL_CATALOG_NAME = "ModCatalogMainShell.xaml";

        #endregion

        #region methods

        public override void Run(bool runWithDefaultConfiguration) {
            base.Run(false);
        }

        protected override DependencyObject CreateShell() {
            Container.RegisterType<MainShellViewModel>();

            return Container.Resolve<MainShell>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer() {
            base.ConfigureContainer();

            RegisterTypeIfMissing(typeof(IServiceLocator), typeof(UnityServiceLocatorAdapter), true);
            RegisterTypeIfMissing(typeof(IModuleInitializer), typeof(ModuleInitializer), true);
            RegisterTypeIfMissing(typeof(IModuleManager), typeof(ModuleManager), true);
            RegisterTypeIfMissing(typeof(RegionAdapterMappings), typeof(RegionAdapterMappings), true);
            RegisterTypeIfMissing(typeof(IRegionManager), typeof(RegionManager), true);
            RegisterTypeIfMissing(typeof(IEventAggregator), typeof(EventAggregator), true);
            RegisterTypeIfMissing(typeof(IRegionViewRegistry), typeof(RegionViewRegistry), true);
            RegisterTypeIfMissing(typeof(IRegionBehaviorFactory), typeof(RegionBehaviorFactory), true);
            RegisterTypeIfMissing(typeof(IRegionNavigationJournalEntry), typeof(RegionNavigationJournalEntry), false);
            RegisterTypeIfMissing(typeof(IRegionNavigationJournal), typeof(RegionNavigationJournal), false);
            RegisterTypeIfMissing(typeof(IRegionNavigationService), typeof(RegionNavigationService), false);
            RegisterTypeIfMissing(typeof(IRegionNavigationContentLoader), typeof(SoaTesterRegionNavigationContentLoader), true);
        }

        protected override IModuleCatalog CreateModuleCatalog() {
            return Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/SOATester;component/ModCatalogs/" + MAIN_SHELL_CATALOG_NAME, UriKind.Relative));
        }

        #endregion

    }
}
