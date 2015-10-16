using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace HNGHRMS.Web.ViewModels
{
    public class CompanyReportViewModel
    {
        [DisplayName("Mã công ty")]
        public int CompanyId { get; set; }
        [DisplayName("Tên công ty")]
        public string CompanyName { get; set; }
        [DisplayName("Số nhân viên hiện tại")]
        public int CurrentEmployees { get; set; }
        [DisplayName("Số nhân viên tuyển mới")]
        public int NewEmployees { get; set; }
        [DisplayName ( "Số nhân viên thôi việc")]
        public int TerminatedEmployees { get; set; }
        [DisplayName ( "Tổng quỹ lương")]
        public double TotalSalary { get; set; }
        [DisplayName("Ngày báo cáo")]
        public String DateReport { get; set; }
        
    }
}