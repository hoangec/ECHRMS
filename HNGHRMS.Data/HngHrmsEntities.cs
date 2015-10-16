using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using HNGHRMS.Model.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HNGHRMS.Data.Configuration;
namespace HNGHRMS.Data.Models
{
    public class HngHrmsEntities : IdentityDbContext<ApplicationUser>
    {
        public HngHrmsEntities() : base("hrmsConnectionString")
        {

        }

        public DbSet<Company> Companies { get;set;}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Termination> Terminations { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();            
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new TerminationConfiguration());

        }
    }
}
