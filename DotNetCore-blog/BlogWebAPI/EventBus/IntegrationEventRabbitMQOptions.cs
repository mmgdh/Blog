using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class IntegrationEventRabbitMQOptions
    {
        public string HostName { get; set; }
        public string ExchangeName { get; set; }
    }
}
