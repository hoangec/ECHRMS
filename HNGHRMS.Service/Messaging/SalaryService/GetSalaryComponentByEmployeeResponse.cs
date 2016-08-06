using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Service.ViewModels;
namespace HNGHRMS.Service.Messaging
{
    public class GetSalaryComponentByEmployeeResponse
    {
        public List<EmployeeSalaryComponentViewModel> EmployeeSalaryList { get; set; }

        public GetSalaryComponentByEmployeeResponse() {
            EmployeeSalaryList = new List<EmployeeSalaryComponentViewModel>();
        }
    }
}
