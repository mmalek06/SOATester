using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Infrastructure;
using Microsoft.Practices.Unity;

namespace SOATester.Modules.ProjectsListModule.ViewModels.Base {
    public abstract class ItemViewModel : ViewModelBase {

        #region fields

        protected string _name;

        #endregion

        #region properties

        [Dependency]
        public int Id { get; set; }

        [Dependency]
        public string Name {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion

        #region constructors and destructors

        public ItemViewModel(IEventAggregator eventAggregator) : base(eventAggregator) {}

        #endregion

    }
}
