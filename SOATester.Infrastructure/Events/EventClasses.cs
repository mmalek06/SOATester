using Prism.Events;

using SOATester.Infrastructure.Events.Descriptors;

namespace SOATester.Infrastructure.Events.EventClasses {
    public class ItemOpenedEvent : PubSubEvent<ItemChosenEventDescriptor> { }

    public class ItemRunEvent : PubSubEvent<ItemRunEventDescriptor> { }
}
