using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Infrastructure.Domain;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Model.Models
{
    public class EmployeeSalaryComponents : EntityBase,IAggregateRoot
    {
        public string SalaryComponentName { get; set; }
        public int EmployeeId { get; set; }
       // public int SalaryComponentId { get; set; }

        public bool IsMainSalary { get; set; }

        public bool IsSalary { get; set; }
        public virtual Employee Employee { get; set; }
       // public virtual SalaryComponent SalaryComponent { get; set; }
        public double  Amount { get; set; }
        public string  Remark { get; set; }

        public bool IsExtra { get; set; }
        public SalaryPayFerequency SalaryPayFrequency { get; set; }
        public DateTime StartApplyDate { get; set; }
        public DateTime EndApplyDate { get; set; }

        public bool CheckBetweenDateTime(DateTime DateCompare)
        {
            return DateCompare.CompareBetweenDateByMonthAndYear(this.StartApplyDate, this.EndApplyDate);
        }
        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
