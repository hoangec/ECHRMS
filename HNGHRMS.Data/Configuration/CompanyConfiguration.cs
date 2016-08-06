using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Configuration
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            HasMany(c => c.Employees).WithRequired(emp => emp.Company).HasForeignKey(emp => emp.CompanyId);
            //HasMany(c => c.Experiences).WithRequired(exp =>exp.Company).HasForeignKey(exp => exp.CompanyId).WillCascadeOnDelete(false);

            Property(e => e.CompanyCode).IsRequired();
            Property(e => e.NumberCodeStarRange).IsRequired();
            Property(e => e.NumberCodeEndRange).IsRequired();
        }
    }
}
