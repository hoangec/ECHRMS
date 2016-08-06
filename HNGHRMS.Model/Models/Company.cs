using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Enums;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Infrastructure.Domain;
namespace HNGHRMS.Model.Models
{
    public class Company : EntityBase , IAggregateRoot
    {
        public string CompanyName { get; set; }
        public double CompanyInsuranceRatePercent { get; set; }
        public double LabaratorInsuranceRatePercent { get; set; }
        public long NumberCodeStarRange { get; set; }
        public long NumberCodeEndRange { get; set; }

        public string CompanyCode { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
      //  public virtual ICollection<Experience> Experiences { get; set; }
        public int GetNumOfEmployeesCurrentDate()
        {
            var numOfEmpoyeesByDate = (from e in this.Employees
                                 where ( e.Status != EmployeeStatus.Terminated) &&  (e.JoinedDate.CompareDateByMonthAndYear(DateTime.Now))
                                 select e).Count();
            return numOfEmpoyeesByDate;
        }
        public int GetNumOfEmployeesByDate(DateTime startDate, DateTime endDate)
        {
            var numOfEmpoyeesByDate = (from e in this.Employees
                                       where (e.Status != EmployeeStatus.Terminated) && (e.JoinedDate.CompareBetweenDateByMonthAndYear(startDate, endDate))
                                       select e).Count();
            return numOfEmpoyeesByDate;
        }
        public double GetSumSalaryEmployeesCurrentDate()
        {
            var sumSalary = (from e in this.Employees
                             where e.Status != EmployeeStatus.Terminated && e.JoinedDate.CompareDateByMonthAndYear(DateTime.Now)
                             select e.Salary).Sum();
            return sumSalary;
        }
        public double GetSumSalaryEmployeesByDate(DateTime startDate, DateTime endDate)
        {
            var sumSalary = (from e in this.Employees
                             where e.Status != EmployeeStatus.Terminated && e.JoinedDate.CompareBetweenDateByMonthAndYear(startDate, endDate)
                             select e.Salary).Sum();
            return sumSalary;
        }

        public double GetSumInsuranceEmployeesByDate(DateTime startDate, DateTime endDate)
        {
            var sumInsurance = (from e in this.Employees
                                where e.Status == EmployeeStatus.Present && e.JoinedDate.CompareBetweenDateByMonthAndYear(startDate, endDate)
                             select e.MadatoryInsurance).Sum();
            return sumInsurance;
        }
        public double GetSumInsuranceEmployeesByCurrentDate()
        {
            var sumInsurance = (from e in this.Employees
                                where e.Status == EmployeeStatus.Present && e.JoinedDate.CompareDateByMonthAndYear(DateTime.Now)
                                select e.MadatoryInsurance).Sum();
            return sumInsurance;
        }
        public int GetNumOfEmployeesJoinByDate(DateTime startDate, DateTime endDate)
        {
            var numOfEmpoyees = (from e in this.Employees
                                 where (e.Status == EmployeeStatus.Present) && (e.JoinedDate.CompareBetweenDateByMonthAndYear(startDate, endDate))
                                 select e).Count();
            return numOfEmpoyees;
        }
        public int GetNumOfEmployeesArriveTransferByDate(DateTime startDate, DateTime endDate)
        {
            var numOfEmpoyees = (from e in this.Employees
                                 where (e.Status == EmployeeStatus.Transfer) && (e.JoinedDate.CompareBetweenDateByMonthAndYear(startDate, endDate))
                                 select e).Count();
            return numOfEmpoyees;
        }

        //public int GetNumOfEmployeesLeaveTransferByDate(DateTime startDate, DateTime endDate)
        //{
        //    var numOfEmpoyees = (from exp in this.Experiences where exp.TransferDate.CompareBetweenDateByMonthAndYear(startDate, endDate)
        //                         select exp).Count();
        //    return numOfEmpoyees;
        //}
        public int GetNumOfEmployeesTerminatedByDate(DateTime startDate, DateTime endDate)
        {
            var numOfEmpoyees = (from e in this.Employees
                                 where e.Status == EmployeeStatus.Terminated && e.Termination.TerminationDate.CompareBetweenDateByMonthAndYear(startDate, endDate)
                                 select e).Count();
            return numOfEmpoyees;
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }

    }
}
