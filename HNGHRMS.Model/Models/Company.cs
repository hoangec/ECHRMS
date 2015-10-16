using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Enums;
using HNGHRMS.Infrastructure.Extensions;
namespace HNGHRMS.Model.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public Company()
        {

        }
        public Company(string companyName)
        {
            this.CompanyName = companyName;
        }
        public virtual ICollection<Employee> Employees { get; set; }


        public int GetNumOfEmployeesByDate(DateTime date)
        {
            var numOfEmpoyeesByDate = (from e in this.Employees
                                 where e.Status == EmployeeStatus.Present &&  (e.JoinedDate.CompareDateByMonthAndYear(date))
                                 select e).Count();
            return numOfEmpoyeesByDate;
        }

        public double GetSumSalaryEmployeesByDate(DateTime date)
        {
            var sumSalary = (from e in this.Employees
                             where e.Status == EmployeeStatus.Present && e.JoinedDate.CompareDateByMonthAndYear(date)
                             select e.Salary).Sum();
            return sumSalary;
        }
        public int GetNumOfEmployeesJoinByDate(DateTime date)
        {
            var numOfEmpoyees = (from e in this.Employees
                                 where e.JoinedDate.Month == date.Month && e.JoinedDate.Year == date.Year && e.Status == EmployeeStatus.Present
                                 select e).Count();
            return numOfEmpoyees;
        }
        public int GetNumOfEmployeesTerminatedByDate(DateTime date)
        {
            var numOfEmpoyees = (from e in this.Employees
                                 where e.Status == EmployeeStatus.Terminated && e.Termination.TerminationDate.Month == date.Month && e.Termination.TerminationDate.Year == date.Year
                                 select e).Count();
            return numOfEmpoyees;
        }


    }
}
