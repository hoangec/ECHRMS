using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class VoluntaryInsuranceAddFormView
    {
        public int VoluntaryEployeeId { get; set; }
        [Display(Name="Số bảo hiểm")]
        [Required(ErrorMessage="Nhập số hợp đồng")]
        public string VoluntaryInsuranceNo { get; set; }

        [Display(Name = "Số tiền đóng")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Nhập số tiền đóng bảo hiểm")]
        [Range(1,double.MaxValue,ErrorMessage="Gía trị nhập lớn hơn không")]
        public double VoluntaryAmount { get; set; }
        
        [Display(Name = "Ngày cấp")]
        public DateTime VoluntaryDateOfIssue { get; set; }
        public string VoluntaryAttachFile { get; set; }
    }
}