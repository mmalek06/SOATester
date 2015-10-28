using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

using Prism.Events;

using SOATester.Entities;

using SOATester.Communication;

using SOATester.Infrastructure;

using SOATester.Modules.ContentModule.ViewModels.Base;

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

        public int Id {
            get { return _test.Id; }
        }

        public string Name {
            get { return _name ?? _test.Name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion

        #region constructors and destructors

        public TestViewModel(IEventAggregator eventAggregator, IUnityContainer container, ITestsRunner runner) : base(eventAggregator, container, runner) { }

        #endregion

        #region event handlers

        protected override void _run() {
            _runner.Run(Test);
        }

        protected override void _stop() {
            _runner.Stop(Test);
        }

        protected override void _pause() {
            _runner.Pause(Test);
        }

        #endregion

    }
}
