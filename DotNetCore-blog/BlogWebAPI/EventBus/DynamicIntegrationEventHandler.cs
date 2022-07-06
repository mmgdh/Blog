using Dynamic.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public abstract class DynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        public Task Handle(string EventName, string eventData)
        {
            dynamic dynamicEventData = DJson.Parse(eventData);
            return HandleDynamic(EventName, dynamicEventData);
        }

        public abstract Task HandleDynamic(string eventName, dynamic eventData);
    }
}
