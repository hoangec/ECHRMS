using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Service.ViewModels;
namespace HNGHRMS.Service.Messaging
{
    public class GetAllSalaryComponentResponse
    {
        public List<SalaryComponentView> SalaryComponentList {get;set;}
        public GetAllSalaryComponentResponse()
        {
            SalaryComponentList = new List<SalaryComponentView>();
        }
    }
}
