using Prism.Regions;
using SOATester.Entities;
using SOATester.Infrastructure.Enums;
using SOATester.Infrastructure.Events;
using SOATester.Modules.ContentModule.Services;
using System;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ScenarioViewModel : PluggableViewModel {

        #region fields

        private Scenario scenario;
        private string name;
        private Uri address;
        private Protocol? protocol;
        private IScenariosService scenariosService;

        #endregion

        #region properties

        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public Uri Address {
            get { return address; }
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
            scenario = scenariosService.Get(descriptor.Id);
            Id = scenario.Id;
            ParentId = scenario.ProjectId;
            TopmostParentId = scenario.ProjectId;
            Name = scenario.Name;
            importance = 2;
            Identity = string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);
        }

        #endregion

    }
}
