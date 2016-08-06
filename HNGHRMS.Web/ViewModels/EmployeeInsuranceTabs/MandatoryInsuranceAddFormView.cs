using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class MandatoryInsuranceAddFormView
    {
        public int EployeeId { get; set; }
        [Display(Name="Số hợp đồng")]
        [Required(ErrorMessage="Nhập số hợp đồng")]
        public string InsuranceNo { get; set; }
        [Display(Name= "Ngày cấp")]
        public DateTime DateOfIssue { get; set; }

        [Display(Name = "Tự định nghĩa")]
        public bool IsDefined { get; set; }

        [Display(Name = "Mức đóng")]
        public double InsuranceAmount { get; set; }

    }
}