using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class TransferEmployeeGridViewModel
    {
        [Display(Name="Mã điều chuyển")]
        public int ExperienceId { get; set; }

        public int EmployeeId { get; set; }   
        [Display(Name="Mã nhân viên")]
        public int EmployeeCode { get; set; }

        [Display(Name = "Tên nhân viên")]
        public string EmployeeName { get; set; }
        
        [Display(Name = "Ngày vào làm")]
        public DateTime OldJoinedDate { get; set; }
        [Display(Name="Ngày điều chuyển")]
        public DateTime TransferDate { get; set; }
        [Display(Name = "Lý do điều chuyển")]
        public string Reason { get; set; }
        [Display(Name = "Thời gian làm việc")]
        public string ExperienceYears { get; set; }
        
        public int OldCompanyId { get; set; }
        
        [Display(Name = "Công ty cũ")]
        public string OldCompanyName { get; set; }
        
        [Display(Name = "Chức vụ cũ")]
        public string OldPositionName { get; set; }

        [Display(Name = "Phòng ban cũ")]
        public string OldDepartement { get; set; }
        [Display(Name = "Mức lương cũ")]
        public double OldSalary { get; set; }
        [Display(Name = "File đính kèm")]
        public string  AttachFile { get; set; }

    }
}
