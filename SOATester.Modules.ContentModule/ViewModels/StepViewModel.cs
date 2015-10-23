using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Entities;
using SOATester.Infrastructure;

using SOATester.Modules.ContentModule.ViewModels.Base;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class StepViewModel : ViewModelBase, IViewModel {

        #region fields

        private Step _step;
        private string _name;

        #endregion

        #region properties
        
        public Step Step {
            get { return _step; }
            set {
                if (_step == null) {
                    SetProperty(ref _step, value);
                }
            }
        }

        public int Id {
            get { return _step.Id; }
        }

        public string Name {
            get { return _name ?? _step.Name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion

        #region constructors and destructors

        public StepViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

    }
}
