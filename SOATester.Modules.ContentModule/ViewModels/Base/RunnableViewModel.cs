using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;
using Prism.Commands;

using SOATester.Infrastructure;

using SOATester.Modules.ContentModule.Models;

namespace SOATester.Modules.ContentModule.ViewModels.Base {
    public abstract class RunnableViewModel<T, K> : ViewModelBase, IViewModel {

        #region fields

        protected RunnableModel<T, K> _runnableModel;

        #endregion

        #region commands

        public DelegateCommand Run { get; set; }
        public DelegateCommand Stop { get; set; }
        public DelegateCommand Pause { get; set; }

        #endregion

        #region constructors and destructors

        public RunnableViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

        #region methods

        protected override void _initCommands() {
            Run = new DelegateCommand(_run);
            Stop = new DelegateCommand(_stop);
            Pause = new DelegateCommand(_pause);
        }

        #endregion

        #region event handlers

        protected abstract void _pause();
        protected abstract void _stop();
        protected abstract void _run();

        #endregion

    }
}
