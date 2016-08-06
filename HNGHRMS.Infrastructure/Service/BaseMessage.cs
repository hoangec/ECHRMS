using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Infrastructure.Service
{
    public abstract class BaseMessage
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
