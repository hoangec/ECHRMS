using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Configuration
{
   public class TerminationConfiguration : EntityTypeConfiguration<Termination>
    {
       public TerminationConfiguration()
       {
           //HasRequired<Employee>(te => te.Employee).WithOptional(em => em.Termination).WillCascadeOnDelete(true);
           //HasKey(t => t.EmployeeId);
           //Property(t => t.Reason).IsRequired();
       }
    }
}
