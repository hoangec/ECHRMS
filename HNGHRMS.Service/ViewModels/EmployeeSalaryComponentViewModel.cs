using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Service.ViewModels
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
        [Display(Name = "Loại lương")]
        public string SalaryComponentType { get; set; }
        [Display(Name = "Chu kỳ trả lương")]
        public string SalaryPayFerequency { get; set; }
        [DataType(DataType.Date)]
        [Display(Name="Thời điểm áp dụng")]
        public DateTime ApplyDate { get; set; }
        [Display(Name="Ghi chú")]
        public string Remark { get; set; }
    }
}
