using Prism.Regions;
using SOATester.Entities;
using SOATester.Infrastructure.Events;
using SOATester.Modules.ContentModule.Services;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class TestViewModel : PluggableViewModel, INavigationAware {

        #region fields

        private Test test;
        private string name;
        private ITestsService testsService;

        #endregion

        #region properties

        public Test Test {
            get { return test; }
            set {
                if (test == null) {
                    SetProperty(ref test, value);
                }
            }
        }

        public override string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public override int Importance => 3;

        public override int Id => test.Id;

        public override int ParentId => test.ScenarioId;

        public override int TopmostParentId => test.Scenario.ProjectId;

        public string Name {
            get { return name ?? test.Name; }
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
            Test = testsService.Get(descriptor.Id);
        }

        #endregion

    }
}
