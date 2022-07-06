using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IIntegrationEventHandler
    {
        Task Handle(string EventName, string eventData);
    }
}
