using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Entities;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.RestCommunication.Base;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class TestViewModel : RunnableViewModel<Test>, IPluggableViewModel, IIdentityViewModel {

        #region fields

        private Test test;
        private string name;
        private Dictionary<string, object> viewProperties;

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

        public string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public int Importance => 3;

        public int Id => test.Id;

        public int ParentId => test.ScenarioId;

        public int TopmostParentId => test.Scenario.ProjectId;

        public IDictionary<string, object> ViewProperties => viewProperties;

        public string Name {
            get { return name ?? test.Name; }
            set { SetProperty(ref name, value); }
        }

        #endregion

        #region constructors and destructors

        public TestViewModel(IEventAggregator eventAggregator, IUnityContainer container, ITestsRunner runner) : base(eventAggregator, container, runner) {
            viewProperties = new Dictionary<string, object>();
        }

        #endregion

        #region event handlers

        protected override void Run() => runner.RunAsync(Test);
        protected override void Stop() => runner.StopAsync(Test);
        protected override void Pause() => runner.PauseAsync(Test);

        #endregion

    }
}
