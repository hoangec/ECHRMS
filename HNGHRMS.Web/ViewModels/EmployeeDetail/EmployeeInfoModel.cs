using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeInfoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Họ không được để trống")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]  
        public string  FirstName { get; set; }

        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        public string  EmployeeCode { get; set; }

        [Required(ErrorMessage = "CMND/Hộ chiếu không được để trống")]
        public string IdentityNo { get; set; }
          [Required(ErrorMessage = "Ngày cấp CMND/Hộ chiếu không được để trống")]
        public DateTime IdentityDateOfIssue { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Double RealSalary { get; set; }

        public Double Salary { get; set; }
        public byte[] Photo { get; set; }
        public string Nationality { get; set; }
          [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime BirthDay { get; set; }

    }
}