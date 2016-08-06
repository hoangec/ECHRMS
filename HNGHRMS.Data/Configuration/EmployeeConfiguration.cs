using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;

namespace HNGHRMS.Data.Configuration
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            HasOptional(e => e.Termination).WithRequired(te => te.Employee).WillCascadeOnDelete(true);          
            //HasMany(e => e.EmployeeEducation).WithRequired(empedu => empedu.Employee).HasForeignKey(empedu => empedu.EmployeeId);
            HasMany(e => e.Contracts).WithRequired(contract => contract.Employee).HasForeignKey(contract => contract.EmployeeId);
            HasMany(e => e.EmployeeSalaryComponents).WithRequired(empSalary => empSalary.Employee).HasForeignKey(empSalary => empSalary.EmployeeId);
            HasMany(e => e.Experiences).WithRequired(exp => exp.Employee).HasForeignKey(exp => exp.EmployeeId).WillCascadeOnDelete(true); ;
            //                                  
            Property(e => e.Departement).IsRequired();
            Property(e => e.Salary).IsRequired();            
            Property(e => e.Gender).IsRequired();
            Property(e => e.BirthDay).IsOptional();
            Property(e => e.Identity.IdentityNo).IsOptional();
            Property(e => e.Identity.DateOfIssue).IsOptional();
            Property(e => e.TerminatedDate).IsOptional();           
            Property(e => e.MadatoryInsurance).IsOptional();
            Property(e => e.MadotoryInsuranceDate).IsOptional();
            Property(e => e.EmployeeCode).IsRequired();
        }
    }
}
