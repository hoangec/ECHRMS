using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;

namespace HNGHRMS.Service.Interface
{
    public interface IReportService
    {
        int[] GetNewEmployeeQuantityTypeByDate(IEnumerable<Company> Companies, DateTime StartDate,DateTime EndDate);

        int[] GetNewEmployeeQuantityTypeByCompany(Company Company);
        int[] GetTerminatedEmployeeQuantityTypeByDate(IEnumerable<Company> Companies, DateTime StartDate,DateTime EndDate);
        int[] GetTerminatedEmployeeQuantityByCompany(Company Company);

        int GetTotalEmployeeQuantityAtCurrentByCompany(int CompanyId);
        int GetNewEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate);
        int GetTransferArriveEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate);
        int GetTransferLeaveEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate);
        int GetTerminatedEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate);

        double GetTotalSalaryByCompanyAtCurrent(int CompanyId);
        double GetTotalSalaryByCompanyByDate(int CompanyId,DateTime Date);
        double GetTotalInsuranceByCompanyByDate(int CompanyId, DateTime Date);
        double GetTotalInsuranceByCompanyAtCurrent(int CompanyId);
        double GetTotalSalaryByCompany(Company Company);

        double GetTotalRealSalaryByCompany(int Companyid,DateTime Date);

        double GetTotalExtraCostByCompany(int Companyid, DateTime Date);
    }
}
