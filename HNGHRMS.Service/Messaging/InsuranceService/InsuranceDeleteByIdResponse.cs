using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Service.ViewModels;
namespace HNGHRMS.Service.Messaging
{
    public class InsuranceDeleteByIdResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int EmployeeId { get; set; }
        public GetInsuranceByEmployeeIdResponse InsuranceByEmployee { get; set; }
    }
}
