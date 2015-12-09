using Microsoft.Practices.Unity;
using Prism.Events;
using SOATester.Entities;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.RestCommunication.Base;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class StepViewModel : RunnableViewModel<Step>, IPluggableViewModel {

        #region fields

        private Step step;
        private string name;
        private Dictionary<string, object> viewProperties;

        #endregion

        #region properties
        
        public Step Step {
            get { return step; }
            set {
                if (step == null) {
                    SetProperty(ref step, value);
                }
            }
        }

        public string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public int Importance => 4;

        public int Id => step.Id;

        public int ParentId => step.TestId;

        public int TopmostParentId => step.Test.Scenario.ProjectId;

        public IDictionary<string, object> ViewProperties => viewProperties;

        public string Name {
            get { return name ?? step.Name; }
            set { SetProperty(ref name, value); }
        }

        #endregion

        #region constructors and destructors

        public StepViewModel(IEventAggregator eventAggregator, IUnityContainer container, IStepsRunner runner) : base(eventAggregator, container, runner) {
            viewProperties = new Dictionary<string, object>();
        }

        #endregion

        #region event handlers 

        protected override void Run() {
            runner.RunAsync(Step);
        }

        protected override void Stop() {
            runner.StopAsync(Step);
        }

        protected override void Pause() {
            runner.PauseAsync(Step);
        }

        #endregion

    }
}
