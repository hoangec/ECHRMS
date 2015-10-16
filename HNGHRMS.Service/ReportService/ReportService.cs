using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Repository;
namespace HNGHRMS.Service
{
    public  class ReportService : IReportService
    {
        private ICompanyRepository companyRepository;

        public ReportService(ICompanyRepository CompanyRepository)
        {
            this.companyRepository = CompanyRepository;
        }
        public int[] GetNewEmployeeQuantity(IEnumerable<Company> Companies, DateTime Date)
        {
            int[] result = new int[Companies.Count()];
            int i = 0;
            foreach (Company com in Companies)
            {
                result[i] = com.GetNumOfEmployeesJoinByDate(Date);
                i++;
            }
            return result;
        }
        public int[] GetNewEmployeeQuantityByCompany(Company Company)
        {
            int[] result = new int[12];
            for (int i = 0; i < 12;i++ )
            {
                if(Company != null)
                {
                    result[i] = Company.GetNumOfEmployeesJoinByDate(new DateTime(DateTime.Now.Year, i + 1, 1));
                }
                else
                {
                    result[i] = 0;
                }

                
            }
            return result;
        }
        public int[] GetTerminatedEmployeeQuantity(IEnumerable<Company> Companies, DateTime Date)
        {
            int[] result = new int[Companies.Count()];
            int i = 0;
            foreach(Company com in Companies){
                result[i] =  com.GetNumOfEmployeesTerminatedByDate(Date);
                i++;
            }
            return result;
        }

        public int[] GetTerminatedEmployeeQuantityByCompany(Company Company)
        {
            int[] result = new int[12];
            for (int i = 0; i < 12; i++)
            {
                if(Company != null)
                {
                    result[i] = Company.GetNumOfEmployeesTerminatedByDate(new DateTime(DateTime.Now.Year, i + 1, 1)); result[i] = Company.GetNumOfEmployeesTerminatedByDate(new DateTime(DateTime.Now.Year, i + 1, 1));
                }
                else
                {
                    result[i] = 0;
                }
                
            }
            return result;
        }
        public double GetTotalSalaryByCompany(Company Company)
        {
            return Company.GetSumSalaryEmployeesByDate(DateTime.Now);
        }
    }
}
