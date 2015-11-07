using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Windows;

namespace SOATester {
    public class Bootstrapper : UnityBootstrapper {
        protected override DependencyObject CreateShell() {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog() {
            return Prism.Modularity.ModuleCatalog.CreateFromXaml(
                new Uri("/SOATester;component/ModCatalog.xaml", UriKind.Relative));
        }
    }
}
