using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using HNGHRMS.Infrastructure;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeViewModel
    {
          [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        public int Id { get; set; }
        [DisplayName("Họ tên")]
        public string FullName { get; set; }
        [DisplayName("Tên")]
        public string FirstName { get; set; }
           [DisplayName("Họ")]
        public string  LastName { get; set; }
        [DisplayName("Giới tính")]
        public Gender Gender { get; set; }
        [DisplayName("Quốc tịch")]
        
        public string Nationality { get; set; }
        [DisplayName("Địa chỉ")]
        public string  Address { get; set; }
        [DisplayName("Phòng ban")]
        public string  Departement { get; set; }
        [DisplayName("Chức vụ")]
        public string PositionName { get; set; }

        [DisplayName("Chức vụ")]
        public int PositionId { get; set; }
        [DisplayName("Mức lương")]
        public Double Salary { get; set; }
          [DisplayName("Ngày vào làm")]
        public DateTime JoinedDate { get; set; }
          [DisplayName("Tình trạng")]
        public EmployeeStatus Status { get; set; }
          [DisplayName("Công ty")]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

    }
}