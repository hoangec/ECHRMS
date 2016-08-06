using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Infrastructure.Service;
namespace HNGHRMS.Service.Messaging
{
    public class DeleteEmpSalaryComponentResponse : BaseMessage
    {
        public int EmployeeId { get; set; }
    }
}
