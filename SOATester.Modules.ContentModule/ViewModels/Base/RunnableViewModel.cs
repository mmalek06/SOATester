using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using SOATester.Infrastructure.ViewModels;
using SOATester.RestCommunication.Base;

namespace SOATester.Modules.ContentModule.ViewModels.Base {
    public abstract class RunnableViewModel<T> : ViewModelBase {

        #region fields

        protected IRunner<T> runner;
        protected IUnityContainer container;

        #endregion

        #region commands

        public DelegateCommand RunCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }
        public DelegateCommand PauseCommand { get; set; }

        #endregion

        #region constructors and destructors

        public RunnableViewModel(IEventAggregator eventAggregator, IUnityContainer container, IRunner<T> runner) : base(eventAggregator) {
            this.container = container;
            this.runner = runner;
        }

        #endregion

        #region methods

        protected override void InitCommands() {
            RunCommand = new DelegateCommand(Run);
            StopCommand = new DelegateCommand(Stop);
            PauseCommand = new DelegateCommand(Pause);
        }

        #endregion

        #region event handlers

        protected abstract void Run();
        protected abstract void Stop();
        protected abstract void Pause();

        #endregion

    }
}
