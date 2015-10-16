using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetNewEmployeesByCompany(int companyId,DateTime date);
        IEnumerable<Employee> GetCurrentEmployeesByCompany(int companyId, DateTime? date);

        IEnumerable<Employee> GetTerminatedEmployeesByCompany(int companyId, DateTime date);
        IEnumerable<Employee> GetCurrentEmployeesBySpecifyCompany(List<int> companyIdList, DateTime? date);
        void CreateEmployee(Employee employee);
        void EditEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        void SaveEmployee();
    }
}
