using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Configuration
{
    public class EmployeeEducationConfiguration : EntityTypeConfiguration<EmployeeEducation>
    {
        public EmployeeEducationConfiguration()
        {
            Property(empedu => empedu.UniversityName).IsRequired();
            Property(empedu => empedu.Major).IsRequired();            
        }
    }
}
