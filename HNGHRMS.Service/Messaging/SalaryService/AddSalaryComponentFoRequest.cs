using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Service.Messaging
{
    public class AddSalaryComponentForEmployeeRequest
    {
        public int EmployeeId { get; set; }
        public string SalaryComponentName { get; set; }
        public bool IsMainSalary { get; set; }
        public Boolean IsExtra { get; set; }
        public Double Amount { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime EndApplyDate { get; set; }
        public SalaryPayFerequency SalaryPayFerequency { get; set; }
        public String Remark { get; set; }

    }
}
