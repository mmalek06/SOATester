using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Communication;
using SOATester.Entities;
using SOATester.Infrastructure.Enums;
using SOATester.Modules.ContentModule.ViewModels.Base;
using System;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ScenarioViewModel : RunnableViewModel<Scenario> {

        #region fields

        private Scenario _scenario;
        private string _name;
        private Uri _address;
        private Protocol _protocol;
                
        #endregion

        #region properties

        public Scenario Scenario {
            get { return _scenario; }
            set {
                if (_scenario == null) {
                    SetProperty(ref _scenario, value);
                }
            }
        }

        public int Id {
            get { return _scenario.Id; }
        }

        public string Name {
            get { return _name ?? _scenario.Name; }
            set { SetProperty(ref _name, value); }
        }

        public Uri Address {
            get { return _address ?? _scenario.Address; }
            set { SetProperty(ref _address, value); }
        }

        public Protocol Protocol {
            get { return _scenario.Protocol; }
            set { SetProperty(ref _protocol, value); }
        }

        #endregion

        #region constructors and destructors

        public ScenarioViewModel(IEventAggregator eventAggregator, IUnityContainer container, IScenariosRunner runner) : base(eventAggregator, container, runner) {}

        #endregion

        #region event handlers

        protected override void _run() {
            _runner.RunAsync(Scenario);
        }

        protected override void _stop() {
            _runner.StopAsync(Scenario);
        }

        protected override void _pause() {
            _runner.PauseAsync(Scenario);
        }

        #endregion

    }
}
