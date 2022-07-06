using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventBus
{
    public abstract class JsonIntegrationEventHandler<T> : IIntegrationEventHandler
    {
        public Task Handle(string EventName, string json)
        {
            T? eventData = JsonSerializer.Deserialize<T>(json);
            return HandleJson(EventName, eventData);
        }

        public abstract Task HandleJson(string eventName, T? eventData);
    }
}
