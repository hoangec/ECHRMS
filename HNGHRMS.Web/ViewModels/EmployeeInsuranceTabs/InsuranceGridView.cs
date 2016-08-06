using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class InsuranceGridView
    {
        [Display(Name="Mã hệ thống")]
        public int Id { get; set; }
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
        public double Amount { get; set; }

        [Display(Name = "Tỷ lệ")]
        [DisplayFormat(DataFormatString="{0:P2}")]
        public double LabaratorRatePercent { get; set; }

        //
        [Display(Name = "Tỷ lệ")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double CompanyRatePercent { get; set; }

        [Display(Name = "Phải đóng")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double CompanyAmount { get {
            return this.CompanyRatePercent * this.Amount;
        } }

        [Display(Name = "Phải đóng")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double LabratorAmount
        {
            get
            {
                return this.LabaratorRatePercent * this.Amount;
            }
        }
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
