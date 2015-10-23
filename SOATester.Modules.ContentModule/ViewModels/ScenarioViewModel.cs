using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Infrastructure;

using SOATester.Entities;

using SOATester.Modules.ContentModule.ViewModels.Base;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ScenarioViewModel : ViewModelBase, IViewModel {

        #region fields

        private Scenario _scenario;
        private string _name;

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

        #endregion

        #region constructors and destructors

        public ScenarioViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

    }
}
