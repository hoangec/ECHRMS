using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using HNGHRMS.Infrastructure;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeTerminateViewModel
    {
          [DisplayName("Mã nhân viên")]
        public int EmployeeId { get; set; }
        [DisplayName("Họ tên")]
        public string  FullName { get; set; }
        [DisplayName("Giới tính")]
        public Gender Gender { get; set; }
        [DisplayName("Địa chỉ")]
        public string  Address { get; set; }
        [DisplayName("Phòng ban")]
        public string  Departement { get; set; }
        [DisplayName("Chức vụ")]
        public int PositionId { get; set; }
        [DisplayName("Quốc tịch")]
        public string Nationality { get; set; }
        public string PositionName { get; set; }
       
        [DisplayName("Mức lương")]
        public Double Salary { get; set; }
          [DisplayName("Ngày vào làm")]
        public String JoinedDate { get; set; }

        [DisplayName("Công ty")]
    public int CompanyId { get; set; }
          public string CompanyName { get; set; }
        [DisplayName("Ngày thôi việc")]
          public DateTime TerminationDate { get; set; }
        [DisplayName("Lý do thôi việc")]
        [Required]
          public string Reason { get; set; }
          public string Note { get; set; }

    }
}