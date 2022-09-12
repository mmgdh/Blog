using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomFilters.Class
{
    public class ServicesException : Exception
    {
        public override string Message { get; }
        public ServicesException(string message) : base(message)
        {
            this.Message = message;
        }
    }
}
