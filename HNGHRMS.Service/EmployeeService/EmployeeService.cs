using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Data.Repository;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private IUnitOfWork unitOfWork;
        public EmployeeService (IEmployeeRepository employeeRepository,IUnitOfWork unitOfWork)
        {
            this.employeeRepository = employeeRepository;
            this.unitOfWork = unitOfWork;
        }

        #region EmployeeService Members

        public Employee GetEmployee(int id)
        {
            var employee = employeeRepository.GetById(id);
            return employee;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            var employees = employeeRepository.GetAll();
            return employees;

        }

        public IEnumerable<Employee> GetNewEmployeesByCompany(int companyId,DateTime date)
        {
            var employees = employeeRepository.GetMany(em =>em.Company.CompanyId == companyId && em.Status == EmployeeStatus.Present && em.JoinedDate.Month == date.Month && em.JoinedDate.Year == date.Year);
            return employees;
        }   

        public IEnumerable<Employee> GetCurrentEmployeesByCompany(int companyId,DateTime? date)
        {
            IEnumerable<Employee> employees;
            if (date.HasValue)
            {
                employees = from e in employeeRepository.GetMany(em => em.Company.CompanyId == companyId && em.Status == EmployeeStatus.Present)
                                    where e.JoinedDate.CompareDateByMonthAndYear(date.Value) == true
                                select e;
            }
            else
            {
                employees = from e in employeeRepository.GetMany(em => em.Company.CompanyId == companyId && em.Status == EmployeeStatus.Present)
                                select e;
            }
            
            return employees;
        }

        public IEnumerable<Employee> GetCurrentEmployeesBySpecifyCompany(List<int> companyIdList, DateTime? date)
        {
            IEnumerable<Employee> employees;
            if (date.HasValue)
            {
                employees = from e in employeeRepository.GetMany(em => companyIdList.Contains(em.CompanyId) && em.JoinedDate.CompareDateByMonthAndYear(date.Value) && em.Status == EmployeeStatus.Present)                       
                                select e;
            }
            else
            {
                employees = from e in employeeRepository.GetMany(em => companyIdList.Contains(em.CompanyId) && em.Status == EmployeeStatus.Present)
                                select e;
            }

            return employees;
        }

        public IEnumerable<Employee> GetTerminatedEmployeesByCompany(int companyId,DateTime date)
        {
            var employees = from e in employeeRepository.GetMany(em => em.Company.CompanyId == companyId && em.Status == EmployeeStatus.Terminated)
                            where(e.Termination.TerminationDate.CompareDateByMonthAndYear(date))
                            select e;
            return employees;
        }

        public void CreateEmployee(Employee employee)
        {
            employeeRepository.Add(employee);
            SaveEmployee();
        }

        public void EditEmployee(Employee employee)
        {
            employeeRepository.Update(employee);
            SaveEmployee();
        }
        public void DeleteEmployee(Employee employee)
        {
            employeeRepository.Delete(employee);
            SaveEmployee();
        }
        public void SaveEmployee()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
