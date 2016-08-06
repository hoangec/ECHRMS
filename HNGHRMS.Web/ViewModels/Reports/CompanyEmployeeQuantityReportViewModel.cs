using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace HNGHRMS.Web.ViewModels
{
    public class CompanyEmployeeQuantityReportViewModel
    {
        [DisplayName("Mã công ty")]
        public int Id { get; set; }
        [DisplayName("Tên công ty")]
        public string CompanyName { get; set; }
        [DisplayName("Số nhân viên")]
        public int CurrentEmployees { get; set; }
        [DisplayName("Số nhân viên tuyển mới")]
        public int NewEmployees { get; set; }
        [DisplayName ( "Số lượt điều chuyển đến")]
        public int NumArriveTransferEmployee { get; set; }
        [DisplayName("Số lượt điều chuyển đi")]
        public int NumLeaveTransferEmployee { get; set; }
        [DisplayName("Số nhân viên thôi việc")]
        public int TerminatedEmployees { get; set; }

        [DisplayName("Ngày bắt đầu")]
        public String DateReportStart { get; set; }

        [DisplayName("Ngày kết thúc")]
        public String DateReportEnd { get; set; }
        
    }
}