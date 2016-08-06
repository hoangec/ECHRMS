using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Repository;
using HNGHRMS.Service.Interface;
using HNGHRMS.Infrastructure.Extensions;
namespace HNGHRMS.Service.Implementations
{
    public  class ReportService : IReportService
    {
        private ICompanyRepository companyRepository;
        private IEmployeeRepository employeeRespository;
        private IExperienceRepository experienceRepository;
        private ITerminateRepository terminateRepository;
        public ReportService(ICompanyRepository CompanyRepository,IEmployeeRepository employeeRespository, IExperienceRepository experienceRepository,ITerminateRepository terminateRepository)
        {
            this.companyRepository = CompanyRepository;
            this.employeeRespository = employeeRespository;
            this.experienceRepository = experienceRepository;
            this.terminateRepository = terminateRepository;
        }

        public int GetTotalEmployeeQuantityAtCurrentByCompany(int CompanyId) 
        {
            int result = 0;
            result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId && emp.Status != EmployeeStatus.Terminated).Count();
            return result;
        }
        public int GetNewEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate)
        {
            int result = 0;
            result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId 
                && emp.Status != EmployeeStatus.Terminated
                ).Where(emp => emp.JoinedDate.CompareBetweenDateByMonthAndYear(StartDate, EndDate)).Count();
            return result;
        }
        public int GetTransferArriveEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate)
        {
            int result = 0;
            result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId
                && emp.Status == EmployeeStatus.Transfer
                ).Where(emp => emp.JoinedDate.CompareBetweenDateByMonthAndYear(StartDate, EndDate)).Count();
            return result;
        }
        public int GetTransferLeaveEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate)
        {
            int result = 0;
            result = experienceRepository.GetMany(exp => exp.CompanyId == CompanyId)
                .Where(exp => exp.TransferDate.CompareBetweenDateByMonthAndYear(StartDate, EndDate)).Count();
            return result;
        }
        public int GetTerminatedEmployeeQuantityByDate(int CompanyId, DateTime StartDate, DateTime EndDate)
        {
            int result = 0;
            result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId
                && emp.Status == EmployeeStatus.Terminated
                ).Where(emp => emp.Termination.TerminationDate.CompareBetweenDateByMonthAndYear(StartDate, EndDate)).Count();
            return result;
        }
        public double GetTotalSalaryByCompanyAtCurrent(int CompanyId)
        {
            double result = 0;
            result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId
                && emp.Status != EmployeeStatus.Terminated).
                Where(emp=>emp.JoinedDate.CompareDateByMonthAndYear(DateTime.Now)).Sum(emp => emp.Salary);
            return result;
        }

        public double GetTotalSalaryByCompanyByDate(int CompanyId, DateTime Date)
        {
            double result = 0;
            //result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId).
            //    Where(emp => emp.JoinedDate.CompareDateByMonthAndYear(Date) && emp.TerminatedDate.CompareDateByMonthAndYear(Date) == false).Sum(emp => emp.Salary);
            var employees = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId).
                Where(emp => emp.JoinedDate.CompareDateByMonthAndYear(Date) && emp.TerminatedDate.CompareDateByMonthAndYear(Date) == false);
            foreach (Employee emp in employees)
            {
                double salaryEachEmp = 0;
               
                if (Date.Month == DateTime.Now.Month && Date.Year == DateTime.Now.Year)
                {
                    salaryEachEmp += emp.EmployeeSalaryComponents.Where(empSalary => empSalary.IsMainSalary && empSalary.IsSalary).Sum(empSalary => empSalary.Amount);
                }
                else {
                    EmployeeSalaryComponents mainSalary = emp.EmployeeSalaryComponents
                   .Where(empSalary => empSalary.IsSalary && empSalary.CheckBetweenDateTime(Date))
                   .OrderByDescending(empSalary => empSalary.EndApplyDate).FirstOrDefault();
                    salaryEachEmp +=  ( mainSalary != null) ? mainSalary.Amount : 0;
                }
                result += salaryEachEmp;
            }
            return result;
        }

        public double GetTotalInsuranceByCompanyByDate(int CompanyId, DateTime Date)
        {
            double result = 0;
            result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId).
                Where(emp => emp.MadotoryInsuranceDate.CompareDateByMonthAndYear(Date) && emp.TerminatedDate.CompareDateByMonthAndYear(Date) == false).Sum(emp => emp.MadatoryInsurance);
            return result;
        }
        public double GetTotalInsuranceByCompanyAtCurrent(int CompanyId) 
        {
            double result = 0;
            result = employeeRespository.GetMany(emp => emp.CompanyId == CompanyId
                && emp.Status != EmployeeStatus.Terminated).
                Where(emp => emp.MadotoryInsuranceDate.CompareDateByMonthAndYear(DateTime.Now)).Sum(emp => emp.MadatoryInsurance);
            return result;
        }
        public int[] GetNewEmployeeQuantityTypeByDate(IEnumerable<Company> Companies, DateTime StartDate,DateTime EndDate)
        {
            int[] result = new int[Companies.Count()];
            int i = 0;
            foreach (Company com in Companies)
            {
                result[i] = GetNewEmployeeQuantityByDate(com.Id, StartDate, EndDate);
                i++;
            }
            return result;
        }
        public int[] GetNewEmployeeQuantityTypeByCompany(Company Company)
        {
            int[] result = new int[12];
            for (int i = 0; i < 12;i++ )
            {
                if(Company != null)
                {
                    int year = DateTime.Now.Year;
                    int month = i + 1;
                    int lastDate = DateTime.DaysInMonth(year, month);
                    result[i] = GetNewEmployeeQuantityByDate(Company.Id,new DateTime(year,month,1),new DateTime(year,month,lastDate));
                }
                else
                {
                    result[i] = 0;
                }

                
            }
            return result;
        }
        public int[] GetTerminatedEmployeeQuantityTypeByDate(IEnumerable<Company> Companies, DateTime StartDate,DateTime EndDate)
        {
            int[] result = new int[Companies.Count()];
            int i = 0;
            foreach(Company com in Companies){
                result[i] = GetTerminatedEmployeeQuantityByDate(com.Id, StartDate, EndDate);
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
                    int year = DateTime.Now.Year;
                    int month = i + 1;
                    int lastDate = DateTime.DaysInMonth(year, month);
                    result[i] = GetTerminatedEmployeeQuantityByDate(Company.Id, new DateTime(year, month, 1), new DateTime(year, month, lastDate));
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
            return Company.GetSumSalaryEmployeesByDate(DateTime.Now, DateTime.Now);
        }

        public double GetTotalRealSalaryByCompany(int Companyid,DateTime Date)
        {
            double result = 0;
            var employees = employeeRespository.GetMany(emp => emp.CompanyId == Companyid).
                 Where(emp => emp.JoinedDate.CompareDateByMonthAndYear(Date) && emp.TerminatedDate.CompareDateByMonthAndYear(Date) == false);
            foreach (Employee emp in employees)
            {
                double empRealSalary = 0;
                double baseSalary = emp.Salary;
                double madatoryInsurance = emp.MadotoryInsuranceDate.CompareDateByMonthAndYear(Date) ? emp.MadatoryInsurance : 0;
                double extraSalary = 0;
                foreach (EmployeeSalaryComponents empSalary in emp.EmployeeSalaryComponents)
                {
                    if (Date.CompareBetweenDateByMonthAndYear(empSalary.StartApplyDate, empSalary.EndApplyDate) && (!empSalary.IsMainSalary) && (!empSalary.IsSalary))
                    {
                        extraSalary += empSalary.Amount;
                    }                    
                }
                empRealSalary += baseSalary + madatoryInsurance + extraSalary;
                result += empRealSalary;
            }
            return result;
        }

        public double GetTotalExtraCostByCompany(int Companyid, DateTime Date)
        {
            double result = 0;
            var employees = employeeRespository.GetMany(emp => emp.CompanyId == Companyid).
                 Where(emp => emp.JoinedDate.CompareDateByMonthAndYear(Date) && emp.TerminatedDate.CompareDateByMonthAndYear(Date) == false);
            foreach (Employee emp in employees)
            {                
                double extraSalary = 0;
                foreach (EmployeeSalaryComponents empSalary in emp.EmployeeSalaryComponents)
                {
                    if (Date.CompareBetweenDateByMonthAndYear(empSalary.StartApplyDate, empSalary.EndApplyDate) && !empSalary.IsSalary)
                    {
                        extraSalary += empSalary.Amount;
                    }
                    result +=  extraSalary;
                }                
            }
            return result;
        }
    }
}
