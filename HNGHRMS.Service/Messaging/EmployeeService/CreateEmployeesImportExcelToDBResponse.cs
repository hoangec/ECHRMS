using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Service.Messaging
{
    public class CreateEmployeesImportExcelToDBResponse
    {
        public Boolean Status { get; set; }
        public int TotalRecorded { get; set; }
        public int TotalInserted { get; set; }
        public Double TotalSalary { get; set; }
    }
}
