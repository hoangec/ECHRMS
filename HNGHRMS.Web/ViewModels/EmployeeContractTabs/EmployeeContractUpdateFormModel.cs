using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeContractUpdateFormModel
    {
        public int ContractUpdateId { get; set; }
        [Required(ErrorMessage="Nhập số hợp đồng")]
        public string ContractUpdateNo { get; set; }
        public int ContractUpdateTypeId { get; set; }
        public DateTime ContractUpdateStartDate { get; set; }
        public string ContractUpdateRemark { get; set; }

        public string ContractUpdateAttachFile { get; set; }
    }
}