using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Configuration
{
    class InsuranceConfiguration : EntityTypeConfiguration<Insurance>
    {
        public InsuranceConfiguration()
        {
            Property(i => i.Values).IsOptional();            
        }
    }
}
