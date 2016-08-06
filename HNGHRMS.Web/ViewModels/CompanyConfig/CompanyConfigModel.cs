using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class CompanyConfigModel
    {
        [Display(Name="Mã công ty")]
        public int Id { get; set; }

        [Display(Name="Tên công ty")]
        [Required(ErrorMessage="Tên không để trống")]
        public string CompanyName { get; set; }
       
        [Display(Name = "Mức đóng bảo hiểm công ty")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public double CompanyInsuranceRatePercent { get; set; }
        
        [Display(Name = "Mức đóng bảo hiểm nhân viên")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public double LabaratorInsuranceRatePercent { get; set; }

        [Display(Name = "Dãy mã bắt đầu")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public long NumberCodeStarRange { get; set; }

        [Display(Name = "Dãy mã kết thúc")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public long NumberCodeEndRange { get; set; }

        [Display(Name = "Mã tiền tố")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public string CompanyCode { get; set; }
    }
}