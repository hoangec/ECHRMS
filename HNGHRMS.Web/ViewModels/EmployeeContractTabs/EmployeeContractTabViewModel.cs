using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeContractTabViewModel
    {
        public IEnumerable<ContractType> ContractTypeList { get; set; }
        public IEnumerable<EmployeeContractsViewModel> ContractsViewModel { get; set; }

        public bool IsEnable { get; set; }
    }
}