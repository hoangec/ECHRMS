using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HNGHRMS.Model.Models;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeContractsViewModel
    {
        [Display(Name="Mã hợp đồng")]
        public int EmployeeContractId { get; set; }
         [Display(Name = "Số hợp đồng")]        
        public string ContractNo { get; set; }


         [DataType(DataType.Date)]        
        [Display(Name="Ngày bắt đầu")]
        public DateTime StartDate { get; set; }
         [DataType(DataType.Date)]
         [Display(Name = "Ngày kết thúc")]
         public DateTime EndDate
         {
             get
             {
                 if (this.ContractType.Duration == 0)
                     return DateTime.MaxValue;
                 else
                     return this.StartDate.AddMonths(this.ContractType.Duration);
             }
         }
         [Display(Name = "Đính kèm")]
        public string  ContractAttachFile { get; set; }
         [Display(Name = "Ghi chú")]
        public string Remark { get; set; }
    
        public int ContractTypeId { get; set; }

        public ContractType ContractType { get; set; }
        public EmployeeContractsViewModel()
        {
            //StartDate = DateTime.Now;
        }

    }
  
}