using Prism.Events;

namespace SOATester.Infrastructure.Events {
    public class BootingCompleted : PubSubEvent<bool> { }

    public class StartupEventBegin : PubSubEvent<StartupEventDescriptor> { }

    public class StartupEventEnd : PubSubEvent<StartupEventDescriptor> { }

    public class ItemOpenedEvent : PubSubEvent<ItemChosenEventDescriptor> { }

    public class ItemRunEvent : PubSubEvent<ItemRunEventDescriptor> { }
}
