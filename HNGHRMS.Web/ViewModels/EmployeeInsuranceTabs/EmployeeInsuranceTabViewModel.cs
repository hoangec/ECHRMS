using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Service.ViewModels;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeInsuranceTabViewModel
    {
        public IEnumerable<InsuranceGridView> InsuranceList { get; set; }
        public bool HasMandatoryInsurance { get; set; }

        public bool HasVoluntaryInsurance { get; set; }
    }
}