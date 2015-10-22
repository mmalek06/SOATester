using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Practices.Unity;
using Prism.Unity;
using Prism.Modularity;

using SOATester.Modules.ContentModule;

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
