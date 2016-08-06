using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Configuration
{
    public class IdentityConfiguration : EntityTypeConfiguration<Identity>
    {
        public IdentityConfiguration()
        {
            //Property(idt => idt.IdentityNo).IsRequired();
            // set Datetime2 for datatime col
            //Property(e => e.DateOfIssue).HasColumnType("datetime2");            
        }
    }
}
