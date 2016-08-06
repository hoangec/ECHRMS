using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Service.Messaging
{
    public class DeleteExperienceByIdResponse
    {
        public bool Status { get; set; }
        public string  Message { get; set; }

        public int EmployeeId { get; set; }
        public DeleteExperienceByIdResponse() 
        {
            this.Status = false;
        }
    }
}
