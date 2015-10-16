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
            Property(c => c.CompanyId).IsRequired();
            Property(c => c.CompanyName).IsRequired();

        }
    }
}
