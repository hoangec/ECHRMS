using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Configuration
{
    public class PositionConfiguraton : EntityTypeConfiguration<Position>
    {
        public PositionConfiguraton()
        {
            HasMany(posi => posi.Employees).WithRequired(emp => emp.Position).HasForeignKey(emp => emp.PositionId);
        }
    }
}
