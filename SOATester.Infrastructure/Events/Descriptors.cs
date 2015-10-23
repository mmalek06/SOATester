using SOATester.Infrastructure.Events.Enums;

namespace SOATester.Infrastructure.Events.Descriptors {
    public class ItemChosenEventDescriptor {

        #region public properties

        public int Id { get; set; }

        public ChosenItemType ItemType { get; set; }

        #endregion

    }
}
