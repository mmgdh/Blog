using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IEventBus
    {
        void publish(string eventName, object? eventData);

        void Subscribe(string eventName, Type handlerType);

        void Unsubscribe(string eventName, Type handlerType);
    }
}
