using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class TransferReportGridViewModel
    {
        [Display(Name="Mã điều chuyển")]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
          
        [Display(Name = "Họ tên nhân viên")]
        public string EmployeeName { get; set; }

        
        [Display(Name = "Giới tính")]
        public Gender Gender { get; set; }

        
        [Display(Name = "Quốc tịch")]
        public string Nationality { get; set; }

        
        [Display(Name = "Ngày điều chuyển")]
        public DateTime TransferDate { get; set; }
          
        [Display(Name = "Công ty")]
        public string CompanyName { get; set; }

        [Display(Name = "Chức vụ")]
        public string PositionId { get; set; }
        public string PositionName { get; set; }

  
        [Display(Name = "Phòng ban")]        
        public string DepartementName { get; set; }

              
        [Display(Name = "Mức lương")]
        public double Salary { get; set; }
              
        [Display(Name = "Mức lương cũ")]
        public double OldSalary { get; set; }
        public int OldCompanyId { get; set; }

          
        [Display(Name = "Công ty cũ")]
        public string OldCompanyName { get; set; }
            
        public int OldPositionId { get; set; }
              
        [Display(Name = "Chức vụ cũ")]
        public string  OldPositionName { get; set; }
             
        [Display(Name = "Phòng ban cũ")]
        public string OldDepartement { get; set; }
              
        [Display(Name = "Lỹ do điều chuyển")]
        public string Reason { get; set; }      


    }
}