using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeContractAddFormModel
    {
        public int EmployeeContractId { get; set; }
        [Display(Name="Số hợp đồng")]
        [Required(ErrorMessage="Nhập số hợp đồng")]
        public string ContractNo { get; set; }
        [Display(Name = "Loại hợp đồng")]
        [Required(ErrorMessage = "Chọn loại hợp đồng")]
        public int ContractTypeId { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Ghi chú")]
        public string Remark { get; set; }

        public string ContractAttachFile { get; set; }
    }
}