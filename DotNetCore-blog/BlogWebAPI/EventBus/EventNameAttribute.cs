using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class EventNameAttribute :Attribute
    {
        public string Name { get; set; }
        public EventNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
