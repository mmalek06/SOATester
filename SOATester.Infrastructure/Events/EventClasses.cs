using Prism.Events;

namespace SOATester.Infrastructure.Events {
    public class BootingCompleted : PubSubEvent<bool> { }

    public class StartupEventEnd : PubSubEvent<StartupActivity> { }

    public class ItemOpenedEvent : PubSubEvent<ItemChosenEventDescriptor> { }

    public class ItemRunEvent : PubSubEvent<ItemRunEventDescriptor> { }
}
