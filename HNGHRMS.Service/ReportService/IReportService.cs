using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Service
{
    public interface IReportService
    {
        int[] GetNewEmployeeQuantity(IEnumerable<Company> Companies, DateTime Date);
        int[] GetNewEmployeeQuantityByCompany(Company Company);
        int[] GetTerminatedEmployeeQuantity(IEnumerable<Company> Companies, DateTime Date);
        int[] GetTerminatedEmployeeQuantityByCompany(Company Company);

        double GetTotalSalaryByCompany(Company Company);
    }
}
