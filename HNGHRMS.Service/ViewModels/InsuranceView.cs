using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Service.ViewModels
{
    public class InsuranceView
    {
        [Display(Name="Mã hệ thống")]
        public int InsuranceId { get; set; }
        [Display(Name="Số bảo hiểm")]
        public string InsuranceNo { get; set; }
        [Display(Name = "Ngày cấp")]
        [DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }
        [Display(Name = "Hình thức")] 
        public string InsuranceType { get; set; }

        [Display(Name = "Mức đóng")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double InsuranceLevel { get; set; }

        [Display(Name = "Tỷ lệ")]
        [DisplayFormat(DataFormatString="{0:P2}")]
        public double Rate { get; set; }

        [Display(Name="Phải đóng")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double Amount { get; set; }
        //
        [Display(Name = "Tỷ lệ")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double CompanyRate { get; set; }

        [Display(Name = "Phải đóng")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double CompanyAmount { get; set; }
        //
          
        [Display(Name="Đính kèm")]
        public string AttachFile { get; set; }
         [Display(Name = "Công Ty")]
        public string HistoryCompanyName { get; set; }
         [Display(Name = "Vị trí")]
        public string HistoryPositionName { get; set; }
        public bool IsHistory { get; set; }

    }
}
