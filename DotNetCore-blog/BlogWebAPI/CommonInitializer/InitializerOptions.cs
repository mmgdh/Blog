using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonInitializer
{
    public class InitializerOptions
    {
        public string? EventBusQueueName { get; set; }

        public Assembly? curAssembly { get; set; }
    }
}
