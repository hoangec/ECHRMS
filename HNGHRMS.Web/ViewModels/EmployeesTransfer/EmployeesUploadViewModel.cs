using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeesUploadViewModel
    {
        public int CompanyId { get; set; }
        public int PositionId { get; set; }

        public IEnumerable<Company> CompanyList{ get; set; }
        public IEnumerable<Position> PositionList { get; set; }
    }
}