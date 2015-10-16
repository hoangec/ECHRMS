using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Web.ViewModels
{
    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public int CurrentEmployees { get; set; }
        public int NewEmployees { get; set; }

        public double  TotalSalary { get; set; }
        public string DateReport { get; set; }
        public int TerminatedEmployees { get; set; }
       // public IEnumerable<Employee> Employees { get; set; }


    }
}