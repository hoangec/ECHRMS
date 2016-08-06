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
            //Database.SetInitializer<HngHrmsEntities>(new HngHrmsSampleData());
        }
        public DbSet<Company> Companies { get;set;}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Termination> Terminations { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EmployeeEducation> EmployeeEdiucation{get;set;}
        public DbSet<Position> Positions { get; set; }
        public DbSet<ContractType> ContractsType { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<EmployeeSalaryComponents> EmployeeSalaryComponents { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public virtual void Commit()
        {            
            try
            {
                base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // set datetime 2 type in sql server 
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
           // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();            
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new PositionConfiguraton());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
           // modelBuilder.Configurations.Add(new IdentityConfiguration());

            modelBuilder.Configurations.Add(new EducationConfiguration());
            modelBuilder.Configurations.Add(new EmployeeEducationConfiguration());
           
            modelBuilder.Configurations.Add(new InsuranceConfiguration());
           // modelBuilder.Configurations.Add(new ExperienceConfiguration());
            modelBuilder.Configurations.Add(new EmployeeSalaryComponentConfiguration());            

        }
    }
}
