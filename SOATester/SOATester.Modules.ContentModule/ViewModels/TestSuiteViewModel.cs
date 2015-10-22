using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Entities;
using SOATester.Infrastructure;

using SOATester.Modules.ContentModule.ViewModels.Base;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class TestSuiteViewModel : ViewModelBase, IViewModel {
        #region fields

        private TestSuite _testSuite;
        private string _name;
        private ObservableCollection<StepViewModel> _steps;

        #endregion

        #region properties

        public TestSuite TestSuite {
            get { return _testSuite; }
            set {
                if (_testSuite == null) {
                    SetProperty(ref _testSuite, value);
                }
            }
        }

        public int Id {
            get { return _testSuite.Id; }
        }

        public string Name {
            get { return _name ?? _testSuite.Name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion

        #region constructors and destructors

        public TestSuiteViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

    }
}
