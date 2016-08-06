using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeFunctionTabViewModel
    {
        public int EmployeeId { get; set; }
        public EmployeeContactTabViewModel EmployeeContactTabViewModel { get; set; }
        public EmployeeJobTabViewModel EmployeeJobTabViewModel { get; set; }
        public EmployeeContractTabViewModel EmployeeContractTabViewModel { get; set; }

        public EmployeeSalaryTabViewModel EmployeeSalaryTabViewModel { get; set; }
        public EmployeeInsuranceTabViewModel EmployeeInsuranceTabViewModel { get; set; }

        public EmployeeExperienceTabViewModel EmployeeExperienceTabViewModel { get; set; }

        public EmployeeDetailReport report1 { get; set; }
        public bool IsEnabled { get; set; }
    }
}