using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeJobTabViewModel
    {
        public int EmployeeJobId { get; set; }
        public DateTime JoinedDate { get; set; }
        public int PositionId { get; set; }
        public int CompanyId { get; set; }
        public string Departement { get; set; }
        public string WorkingTimeSpan { get; set; }
        public EmployeeStatus Status { get; set; }
        public IEnumerable<Position> PositionsList { get; set; }
        public IEnumerable<Company> CompanyList { get; set; }
    
    }
}