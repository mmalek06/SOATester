using Prism.Regions;
using SOATester.Entities;
using SOATester.Infrastructure.Enums;
using SOATester.Infrastructure.Events;
using SOATester.Modules.ContentModule.Services;
using System;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ScenarioViewModel : PluggableViewModel, INavigationAware {

        #region fields

        private Scenario scenario;
        private string name;
        private Uri address;
        private Protocol? protocol;
        private IScenariosService scenariosService;

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

        public override string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public override int Importance => 2;

        public override int Id => scenario.Id;

        public override int ParentId => scenario.ProjectId;

        public override int TopmostParentId => scenario.ProjectId;

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

        protected override ChosenItemType MyType {
            get { return ChosenItemType.SCENARIO; }
        }

        #endregion

        #region constructors and destructors

        public ScenarioViewModel(IScenariosService scenariosService) : base() {
            this.scenariosService = scenariosService;
        }

        #endregion

        #region methods

        protected override void SetItem(ItemChosenEventDescriptor descriptor) {
            Scenario = scenariosService.Get(descriptor.Id);
        }

        #endregion

    }
}
