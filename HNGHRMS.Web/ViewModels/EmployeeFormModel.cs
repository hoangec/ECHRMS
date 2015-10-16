using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HNGHRMS.Web.Core.Validations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeFormModel
    {
        public int EmployeeId { get;set;}
        [Required(ErrorMessage="Nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage="Nhập Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage="Nhập phòng ban")]
        public String Departement { get; set; }
        [Required(ErrorMessage = "Nhập vị trí công tác")]
        public string  Position { get; set; }
        public Gender Gender { get; set; }
        [DataType(DataType.Currency)]
        [Range(1,double.MaxValue,ErrorMessage="Tiền lương lớn hơn 0")]
        public Double Salary { get; set; }
        [DateLargerCurrentDate(ErrorMessage="Ngày làm việc lớn hơn ngày hiện tại")]
        public DateTime JoinedDate { get; set; }
         [Required(ErrorMessage = "Chọn công ty")]
        public int CompanyId { get; set; }

         public EmployeeStatus Status { get; set; }
    }
}