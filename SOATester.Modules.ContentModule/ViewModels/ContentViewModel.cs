using Prism.Events;
using SOATester.Infrastructure.Events;
using SOATester.Infrastructure.ViewModels;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ContentViewModel : ViewModelBase {

        #region constructors and destructors

        public ContentViewModel(IEventAggregator eventAggregator) : base(eventAggregator) { }

        #endregion

        #region methods

        protected override void InitEvents() {
            eventAggregator.GetEvent<ItemOpenedEvent>().Subscribe(ItemChosen);
        }

        #endregion

        #region event handlers

        private void ItemChosen(ItemChosenEventDescriptor evtDescriptor) {
            
        }

        #endregion

    }
}
