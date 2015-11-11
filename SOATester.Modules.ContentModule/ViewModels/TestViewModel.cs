using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Entities;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.RestCommunication.Base;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class TestViewModel : RunnableViewModel<Test> {

        #region fields

        private Test _test;
        private string _name;

        #endregion

        #region properties

        public Test Test {
            get { return _test; }
            set {
                if (_test == null) {
                    SetProperty(ref _test, value);
                }
            }
        }

        public int Id => _test.Id;

        public string Name {
            get { return _name ?? _test.Name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion

        #region constructors and destructors

        public TestViewModel(IEventAggregator eventAggregator, IUnityContainer container, ITestsRunner runner) : base(eventAggregator, container, runner) { }

        #endregion

        #region event handlers

        protected override void _run() => _runner.RunAsync(Test);
        protected override void _stop() => _runner.StopAsync(Test);
        protected override void _pause() => _runner.PauseAsync(Test);

        #endregion

    }
}
