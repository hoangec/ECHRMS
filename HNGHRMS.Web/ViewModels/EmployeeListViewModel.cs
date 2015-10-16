using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeListViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
        public string[] Genders { get; set; }
        public string[] EmployeeStatus { get; set; }

    }
}