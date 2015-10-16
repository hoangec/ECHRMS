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
            Property(e => e.EmployeeId).IsRequired();            
            Property(e => e.Departement).IsRequired();
            Property(e => e.Position).IsRequired();
            Property(e => e.Salary).IsRequired();
            Property(e => e.Name).IsRequired();
            Property(e => e.Gender).IsRequired();
        }
    }
}
