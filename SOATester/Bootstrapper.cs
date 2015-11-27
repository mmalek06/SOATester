using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Unity;
using SOATester.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace SOATester {
    public enum Shells { MAIN_SHELL, SPLASH_SCREEN_SHELL }

    public class Bootstrapper : UnityBootstrapper {

        #region constants

        private const string SPLASH_SHELL_CATALOG_NAME = "ModCatalogSplashScreenShell.xaml";
        private const string MAIN_SHELL_CATALOG_NAME = "ModCatalogMainShell.xaml";
        
        #endregion

        #region fields

        private Shells _chosenShell;
        private IEventAggregator _eventAggregator;
        private Dictionary<Type, bool> _subscribedEvents;

        #endregion

        public Bootstrapper() : base() {
            _subscribedEvents = new Dictionary<Type, bool>();
            _chosenShell = Shells.SPLASH_SCREEN_SHELL;
        }

        #region public methods

        public override void Run(bool runWithDefaultConfiguration) {
            base.Run(runWithDefaultConfiguration);

            if (_eventAggregator == null) {
                _eventAggregator = Container.Resolve<IEventAggregator>();
            }

            _setupEvents();
        }

        #endregion

        #region methods

        protected override DependencyObject CreateShell() {
            switch(_chosenShell) {
                case Shells.SPLASH_SCREEN_SHELL:
                    return Container.Resolve<SplashScreenShell>();
                case Shells.MAIN_SHELL:
                    return Container.Resolve<MainShell>();
                default:
                    return null;
            }
        }

        protected override void InitializeShell() {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog() {
            switch (_chosenShell) {
                case Shells.SPLASH_SCREEN_SHELL:
                    return Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/SOATester;component/ModCatalogs/" + SPLASH_SHELL_CATALOG_NAME, UriKind.Relative));
                case Shells.MAIN_SHELL:
                    return Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/SOATester;component/ModCatalogs/" + MAIN_SHELL_CATALOG_NAME, UriKind.Relative));
                default:
                    return null;
            }
        }

        private void _setupEvents() {
            if (!_subscribedEvents.ContainsKey(typeof(BootingCompleted))) {
                _eventAggregator.GetEvent<BootingCompleted>().Subscribe(_bootingCompleted);
            }
        }

        private void _bootingCompleted(bool success) {
            if (success) {
                _chosenShell = Shells.MAIN_SHELL;

                var startupWindow = App.Current.MainWindow;

                App.Current.MainWindow.Hide();
                Run();

                startupWindow.Close();
            }
        }

        #endregion

    }
}
