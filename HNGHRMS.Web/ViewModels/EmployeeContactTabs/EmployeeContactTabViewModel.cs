using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeContactTabViewModel
    {
        public int EmployeeContactId { get; set; }                
        public string Address { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        public string Email { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
    }
}