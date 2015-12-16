﻿using SOATester.Entities;
using SOATester.Infrastructure.Events;
using SOATester.Modules.ContentModule.Services;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class StepViewModel : PluggableViewModel {

        #region fields

        private Step step;
        private string name;
        private IStepsService stepsService;

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

        public override string Identity => string.Format("{0}.{1}.{2}.{3}", Importance, Id, ParentId, TopmostParentId);

        public override int Importance => 4;

        public override int Id => step.Id;

        public override int ParentId => step.TestId;

        public override int TopmostParentId => step.Test.Scenario.ProjectId;

        public string Name {
            get { return name ?? step.Name; }
            set { SetProperty(ref name, value); }
        }

        protected override ChosenItemType MyType {
            get { return ChosenItemType.STEP; }
        }

        #endregion

        #region constructors and destructors

        public StepViewModel(IStepsService stepsService) : base() {
            this.stepsService = stepsService;
        }

        #endregion

        #region methods

        protected override void SetItem(ItemChosenEventDescriptor descriptor) {
            Step = stepsService.Get(descriptor.Id);
        }

        #endregion

    }
}
