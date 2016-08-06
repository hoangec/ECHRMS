using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Service.ViewModels;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
using HNGHRMS.Web.Validations;
namespace HNGHRMS.Web.ViewModels
{
    public class SalaryComponentAddFormView
    {
       
        public int SalaryComponentEmployeeId { get; set; }
      
           
        [Display(Name = "Tên chi phí")]
        [Required(ErrorMessage = "Không được để trống")]
        public string SalaryComponentName { get; set; }
        
        [Display(Name="Điều chỉnh tăng")]
        public bool IsExtra { get; set; }
       
        [Display(Name = "Ghi vào lương cơ bản")]
        public bool IsMainSalary{ get; set; }      
        [Display(Name="Hình thức trả")]
        public SalaryPayFerequency SalaryPayFrequency { get; set; }
        

        [Display(Name = "Thời điểm áp dụng")]        
        public DateTime ApplyDate { get; set; }

        [Display(Name = "Thời điểm kết thúc")]
        [Required(ErrorMessage = "Nhập ngày kết thúc")]
        public DateTime EndDate { get; set; }
       
        [Display(Name = "Số tiền")]
        [Required(ErrorMessage="Nhập số tiền")]
        [Range(1000.0,double.MaxValue,ErrorMessage = "Số tiền phải lơn hơn 1,000")]
        public double Amount { get; set; }
        [Display(Name = "Ghi chú")]
        public string SalaryComponentRemark { get; set; }
    }
}