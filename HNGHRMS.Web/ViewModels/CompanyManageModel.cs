using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class CompanyManageModel
    {
        [Display(Name="Mã công ty")]
        public int CompanyId { get; set; }

        [Display(Name="Tên công ty")]
        [Required(ErrorMessage="Tên không để trống")]
        public string CompanyName { get; set; }
    }
}