using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeSalaryComponentViewModel
    {
        [Display(Name="Mã hệ thống")]
        public int Id { get; set; }
        [Display(Name="Tên")]
        public string SalaryComponentName { get; set; }
        [Display(Name = "Số tiền")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString="c0")]
        public double Amount { get; set; }

         [Display(Name = "Điều chỉnh tăng")]
        public bool IsExtra { get; set; }

        [Display(Name = "Lương hiện tại")]
        public bool IsMainSalary { get; set; }

        public bool  IsSalary { get; set; }
        [Display(Name = "Chu kỳ trả lương")]
        public SalaryPayFerequency SalaryPayFrequency { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Thời điểm áp dụng")]
        public DateTime StartApplyDate { get; set; }

        [Display(Name = "Thời điểm kết thúc")]
        public DateTime EndApplyDate { get; set; }

        [Display(Name="Ghi chú")]
        public string Remark { get; set; }
    }
}
