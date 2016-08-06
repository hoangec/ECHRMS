using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Configuration
{
    public class EducationConfiguration : EntityTypeConfiguration<Education>
    {
        public EducationConfiguration()
        {
            HasMany(edu => edu.EmployeeEducation).WithRequired(empedu => empedu.Education).HasForeignKey(empedu => empedu.EducationId);
            Property(edu => edu.Name).IsRequired();
        }
    }
}
