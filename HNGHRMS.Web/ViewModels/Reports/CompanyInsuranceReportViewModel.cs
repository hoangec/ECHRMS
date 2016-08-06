using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace HNGHRMS.Web.ViewModels
{
    public class CompanyInsuranceReportViewModel
    {
        [DisplayName("Mã công ty")]
        public int Id { get; set; }
        [DisplayName("Tên công ty")]
        public string CompanyName { get; set; }
        [DisplayName("Qũy lương hiện tại")]
        public double CurrentInsurance { get; set; }
        
        [DisplayName("Tháng 1")]
        public double OneMSalary { get; set; }

        [DisplayName("Tháng 2")]
        public double TwoMSalary { get; set; }
        [DisplayName("Tháng 3")]
        public double ThreeMSalary { get; set; }
        [DisplayName("Tháng 4")]
        public double FourMSalary { get; set; }
        [DisplayName("Tháng 5")]
        public double FiveMSalary { get; set; }


        [DisplayName("Tháng 6")]
        public double SixMSalary { get; set; }
        [DisplayName("Tháng 7")]
        public double SavenMSalary { get; set; }
        [DisplayName("Tháng 8")]
        public double EightMSalary { get; set; }
        [DisplayName("Tháng 9")]
        public double NineMSalary { get; set; }
        [DisplayName("Tháng 10")]
        public double TenMSalary { get; set; }
        [DisplayName("Tháng 11")]
        public double ElevenMSalary { get; set; }
        [DisplayName("Tháng 12")]
        public double TwelveMSalary { get; set; }
        
    }
}