using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using HNGHRMS.Service.ViewModels;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeSalaryTabViewModel
    {
        public IEnumerable<EmployeeSalaryComponentViewModel> EmployeeSalaryComponents { get; set; }
        public bool HaveSalaryComponent { get; set; }
       
    }
}