using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HNGHRMS.Infrastructure.Service;
namespace HNGHRMS.Service.Messaging
{
    public class GetEmployeesImportReadExcelFileResponse : BaseMessage
    {        
        public DataTable TableFormExcelFile { get; set; }
        public int TotalRecord { get; set; }
        public int TotalInValidRecord { get; set; }
        public double TotalSalary { get; set; }

        public GetEmployeesImportReadExcelFileResponse()
        {
            TotalRecord = 0;
            TotalInValidRecord = 0;
            TotalSalary = 0;         
        }
    }
}
