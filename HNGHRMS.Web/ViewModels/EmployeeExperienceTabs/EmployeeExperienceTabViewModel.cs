using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Service.ViewModels;
using HNGHRMS.Data.Models;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeExperienceTabViewModel
    {
        public IEnumerable<TransferEmployeeGridViewModel> ExperiencesList { get; set; }
        
    }
}