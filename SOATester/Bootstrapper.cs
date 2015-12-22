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

        protected override DependencyObject CreateShell() {
            Container.RegisterType<MainShellViewModel>();

            return Container.Resolve<MainShell>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog() {
            return Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/SOATester;component/ModCatalogs/" + MAIN_SHELL_CATALOG_NAME, UriKind.Relative));
        }

        #endregion

    }
}
