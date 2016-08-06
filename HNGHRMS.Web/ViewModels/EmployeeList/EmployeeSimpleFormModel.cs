using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using HNGHRMS.Web.Validations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeSimpleFormModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        [Display(Name="Mã nhân viên")]
        [Remote("IsEmployeeCodeExists", "Employee", ErrorMessage = "Mã nhân viên đã tồn tại")]
        public string EmployeeCode { get; set; }
        
        [Required(ErrorMessage = "Họ không được để trống")]
        [Display(Name="Họ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Tên")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [Display(Name="Ngày sinh")]
        public DateTime BirthDay { get; set; }
        [Required(ErrorMessage = "CMND/Hộ chiếu không được để trống")]

        [DisplayName("CMND/ Hộ chiếu")]
        public string IdentityNo { get; set; }
        [Required(ErrorMessage = "Ngày cấp CMND/Hộ chiếu không được để trống")]
        
        [DisplayName("Ngày cấp")]
        public DateTime IdentityDateOfIssue { get; set; }

        [DisplayName("Giới tính")]
        public Gender Gender { get; set; }
        [DisplayName("Gia đình")]
        public MaritalStatus MaritalStatus { get; set; }
        
        [DisplayName("Phòng ban")]
        [Required(ErrorMessage = "Nhập vị trí công tác")]
        public String Departement { get; set; }

        [Required(ErrorMessage = "Chọn chức vụ")]
        [DisplayName("Chức vụ")]
        public int  PositionId { get; set; }


        [DataType(DataType.Currency)]
        [Range(1,double.MaxValue,ErrorMessage="Tiền lương lớn hơn 0")]
        [Required(ErrorMessage = "Lương không được để trống")]
        [DisplayName("Tiền lương")]
        public Double Salary { get; set; }
        [DateLargerCurrentDate(ErrorMessage="Ngày làm việc lớn hơn ngày hiện tại")]
        [DisplayName("Ngày vào làm")]
        [Required(ErrorMessage = "Ngày vào làm không được để trống")]
        public DateTime JoinedDate { get; set; }
         
        [Required(ErrorMessage = "Chọn công ty")]
        [DisplayName("Công ty")]
        public int CompanyId { get; set; }

        public EmployeeStatus Status { get; set; }
          
        [DisplayName("Quốc tịch")]
        [Required(ErrorMessage = "Quốc tịch không được để trống")]
        public string Nationality { get; set; }
        public string CheckIfViewModelIsValid()
        {
            String result = "";
            if (this.CompanyId == 0 && this.PositionId == 0)
            {
                return result = "Mã công ty và chức vụ không tồn tại";
            }
            else if (this.CompanyId == 0)
            {
                return result = "Mã công ty không tồn tại";
            }
            else if(this.PositionId ==0)
            {
                return result = "Mã chức vụ không tồn tại";
            }
            return result;
        }
    }
}