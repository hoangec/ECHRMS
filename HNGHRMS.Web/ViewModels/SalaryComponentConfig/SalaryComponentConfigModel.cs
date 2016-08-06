using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Web.ViewModels
{
    public class SalaryComponentConfigModel
    {
        [Display(Name="Mã loại chi phí")]        
        public int Id { get; set; }

        [Display(Name="Tên chí phí lương")]
        [Required(ErrorMessage="Tên không để trống")]
        public string ComponentName { get; set; }
       
        [Display(Name = "Điều chỉnh tăng")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public bool IsExtra { get; set; }

        [Display(Name = "Ghi nhận vào lương cơ bản")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public bool IsMainSalary { get; set; }
         
        [Display(Name = "Lần trả")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public SalaryPayFerequency SalaryPayFrequency { get; set; }
    }
}