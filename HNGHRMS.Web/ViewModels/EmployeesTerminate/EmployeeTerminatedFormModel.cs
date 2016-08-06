using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using HNGHRMS.Model.Enums;
using HNGHRMS.Model.Models;
using System.Globalization;
using HNGHRMS.Web.Validations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeTerminatedFormModel
    {
        [DisplayName("Mã nhân viên")]
        public int EmployeeId { get; set; }
        [DisplayName("Họ tên")]
        public string  Name { get; set; }
       
        public Gender Gender { get; set; }

         [DisplayName("Giới tính")]
        public string GenderName
        {
            get { 
                if(this.Gender == Model.Enums.Gender.Male)
                {
                    return "Nam";
                }
                else
                {
                    return "Nữ";
                }

            }
        }

        [DisplayName("Địa chỉ")]
        public string  Address { get; set; }
        [DisplayName("Phòng ban")]
        public string  Departement { get; set; }
        [DisplayName("Chức vụ")]
        public string PositionName { get; set; }

        [DisplayName("Mức lương")]
        public string Salary { get; set; }
          
        [DisplayName("Ngày vào làm")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime JoinedDate { get; set; }
        
        [DisplayName("Tình trạng")]
        public EmployeeStatus Status { get; set; }
          [DisplayName("Công ty")]
          public string CompanyName { get; set; }

         // [DateLargerCurrentDate(ErrorMessage = "Ngày làm việc lớn hơn ngày hiện tại")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
          public DateTime TerminationDate { get; set; }
           [Required(ErrorMessage="Nhập lý do")]
          public string Reason { get; set; }
          public string Note { get; set; }

          public EmployeeTerminatedFormModel(Employee Employee)
          {
              TerminationDate = DateTime.Now;
              this.EmployeeId = Employee.Id;
              this.Name = Employee.FullName;
              this.CompanyName = Employee.Company.CompanyName;
              this.Address = Employee.Address;
              this.Gender = Employee.Gender;
              this.Departement = Employee.Departement;
              this.PositionName = Employee.Position.PositionName;
              this.JoinedDate = Employee.JoinedDate;     
              this.Salary = Employee.Salary.ToString("c0");                            
          }

        public EmployeeTerminatedFormModel()
          {
              TerminationDate = DateTime.Now;
          }
    }
}