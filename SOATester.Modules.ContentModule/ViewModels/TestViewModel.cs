using SOATester.Entities;
using SOATester.Infrastructure.Events;
using SOATester.Modules.ContentModule.Services;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class TestViewModel : PluggableViewModel {

        #region fields

        private Test test;
        private string name;
        private ITestsService testsService;

        #endregion

        #region properties

        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        protected override ChosenItemType MyType {
            get { return ChosenItemType.TEST; }
        }

        #endregion

        #region constructors and destructors

        public TestViewModel(ITestsService testsService) : base() {
            this.testsService = testsService;
        }

        #endregion

        #region methods

        protected override void SetItem(ItemChosenEventDescriptor descriptor) {
            test = testsService.Get(descriptor.Id);
            Identity = string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);
            Importance = 3;
            Id = test.Id;
            ParentId = test.ScenarioId;
            TopmostParentId = test.Scenario.ProjectId;
            Name = test.Name;
        }

        #endregion

    }
}
