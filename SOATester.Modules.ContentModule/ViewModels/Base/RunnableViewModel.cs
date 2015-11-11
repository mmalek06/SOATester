using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Infrastructure;
using SOATester.RestCommunication.Base;

namespace SOATester.Modules.ContentModule.ViewModels.Base {
    public abstract class RunnableViewModel<T> : ViewModelBase {

        #region fields

        protected IRunner<T> _runner;
        protected IUnityContainer _container;

        #endregion

        #region commands

        public DelegateCommand Run { get; set; }
        public DelegateCommand Stop { get; set; }
        public DelegateCommand Pause { get; set; }

        #endregion

        #region constructors and destructors

        public RunnableViewModel(IEventAggregator eventAggregator, IUnityContainer container, IRunner<T> runner) : base(eventAggregator) {
            _container = container;
            _runner = runner;
        }

        #endregion

        #region methods

        protected override void _initCommands() {
            Run = new DelegateCommand(_run);
            Stop = new DelegateCommand(_stop);
            Pause = new DelegateCommand(_pause);
        }

        #endregion

        #region event handlers

        protected abstract void _run();
        protected abstract void _stop();
        protected abstract void _pause();

        #endregion

    }
}
