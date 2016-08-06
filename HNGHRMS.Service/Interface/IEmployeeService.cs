using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Service.Messaging;
namespace HNGHRMS.Service.Interface
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesByUser(ApplicationUser User);
        Employee GetEmployee(int id);
        Employee GetEmployeeByEmployeeCode(string EmployeeCode);
        IEnumerable<Employee> GetNewEmployeesCompanyByDate(int CompanyId,DateTime StartDate,DateTime EndDate);
        IEnumerable<Employee> GetCurrentEmployeesByCompany(int companyId, DateTime? date);
        IEnumerable<Employee> GetCurrentEmployeesWorking();
        IEnumerable<Employee> GetCurrentEmployeesWorkingByUser(ApplicationUser User);
        IEnumerable<Employee> GetTerminatedEmployeesByCompany(int companyId, DateTime date);
        IEnumerable<Employee> GetCurrentEmployeesBySpecifyCompany(List<int> companyIdList, DateTime? date);
        String GetEmployeeCodeByCompanyId(int Id);

        GetEmployeesImportReadExcelFileResponse GetEmployeesImportExcelFile(string FileName, int CompanyId, int PositionId);

        Double GetEmployeeRealSalaryAtDate(Employee Employee, DateTime Date);

        CreateEmployeesImportExcelToDBResponse CreateEmployeesFromImportExcelToDB(string FileName, int CompanyId, int PositionId);
        CreateEmployeeResponse CreateEmployee(Employee employee);
        void EditEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        void SaveEmployee();
    }
}
