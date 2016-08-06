using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Web.ViewModels
{
    public class ContractTypeConfigModel
    {
        [Display(Name="Mã loại HĐ")]
        
        public int Id { get; set; }

        [Display(Name="Tên hợp đồng")]
        [Required(ErrorMessage="Tên không để trống")]
        public string ContractTypeName { get; set; }
       
        [Display(Name = "Kỳ hạn (tháng) / 0 = Không kỳ hạn")]
        [Required(ErrorMessage = "Giá trị không để trống")]
        public int Duration { get; set; }       
    }
}