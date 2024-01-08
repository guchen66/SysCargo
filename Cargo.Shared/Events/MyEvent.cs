using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.Events
{
    internal class MyEvent
    {
    }
    public class ProcessEvent : PubSubEvent
    {
    }
    public class OptionTrayNumberEvent : PubSubEvent
    {
    }

    public class CacheEvent : PubSubEvent
    {
    }
    public class WorkPlaceDeletedEvent : PubSubEvent<int>
    {
    }

    public class WorkStationDelEvent : PubSubEvent
    {
    }
}
