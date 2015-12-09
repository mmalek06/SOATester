using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Entities;
using SOATester.Infrastructure.Enums;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.RestCommunication.Base;
using System;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ScenarioViewModel : RunnableViewModel<Scenario>, IPluggableViewModel {

        #region fields

        private Scenario scenario;
        private string name;
        private Uri address;
        private Protocol? protocol;
        private Dictionary<string, object> viewProperties;
                
        #endregion

        #region properties

        public Scenario Scenario {
            get { return scenario; }
            set {
                if (scenario == null) {
                    SetProperty(ref scenario, value);
                }
            }
        }

        public string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public int Importance => 2;

        public int Id => scenario.Id;

        public int ParentId => scenario.ProjectId;

        public int TopmostParentId => scenario.ProjectId;

        public IDictionary<string, object> ViewProperties => viewProperties;

        public string Name {
            get { return name ?? scenario.Name; }
            set { SetProperty(ref name, value); }
        }

        public Uri Address {
            get {
                if (address == null && scenario.Address == null) {
                    return null;
                }

                return address == null ? new Uri(scenario.Address) : address;
            }
            set { SetProperty(ref address, value); }
        }

        public Protocol? Protocol {
            get { return string.IsNullOrEmpty(scenario.Protocol) ? (Protocol?)null : (Protocol)Enum.Parse(typeof(Protocol), scenario.Protocol); }
            set { SetProperty(ref protocol, value); }
        }

        #endregion

        #region constructors and destructors

        public ScenarioViewModel(IEventAggregator eventAggregator, IUnityContainer container, IScenariosRunner runner) : base(eventAggregator, container, runner) {
            viewProperties = new Dictionary<string, object>();
        }

        #endregion

        #region event handlers

        protected override void Run() {
            runner.RunAsync(Scenario);
        }

        protected override void Stop() {
            runner.StopAsync(Scenario);
        }

        protected override void Pause() {
            runner.PauseAsync(Scenario);
        }

        #endregion

    }
}
