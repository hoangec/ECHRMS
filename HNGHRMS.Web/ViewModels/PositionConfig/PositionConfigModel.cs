using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace HNGHRMS.Web.ViewModels
{
    public class PositionConfigModel
    {
        public int Id { get; set; }
        [Display(Name="Tên chức vụ")]        
        [Required(ErrorMessage="Tên không được để trống")]
        public string PositionName { get; set; }
        [Display(Name = "Mức đóng bảo hiểm")]
        [Required(ErrorMessage = "Mức đóng bảo hiểm không được để trống")]
        public double InsuranceRate { get; set; }

    }
}