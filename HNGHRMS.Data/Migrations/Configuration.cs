namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HNGHRMS.Data.Models;
    using HNGHRMS.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    internal sealed class Configuration : DbMigrationsConfiguration<HNGHRMS.Data.Models.HngHrmsEntities>
    {
        public Configuration()
        {            
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HNGHRMS.Data.Models.HngHrmsEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            SeedUserRole(context);

        }

        private void SeedUserRole(HNGHRMS.Data.Models.HngHrmsEntities context)
        {
            // Role Cretea
            ApplicationRole adminRole = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
            };
            ApplicationRole managerRole = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Manager",
            };
            ApplicationRole superUserRole = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "SuperUser",
            };
            ApplicationRole userRole = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
            };
            if(!(context.Roles.Any(r=>r.Name == "Admin")))
                context.Roles.AddOrUpdate(adminRole);
            if (!(context.Roles.Any(r => r.Name == "Manager")))
                context.Roles.AddOrUpdate(managerRole);
            if (!(context.Roles.Any(r => r.Name == "SuperUser")))
                context.Roles.AddOrUpdate(superUserRole);
            if (!(context.Roles.Any(r => r.Name == "User")))
                context.Roles.AddOrUpdate(userRole);

            // User 
            if (!(context.Users.Any(u => u.UserName == "admin")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "admin",
                    FirstName = "Hoang",
                    LastName = "Vo",
                    Email = "hoang.vo@hagl.com.vn",
                    CompaniesRole = "null",
                    RoleId = "Admin"
                };
                userManager.Create(userToInsert, "123456");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }
        }
    }
}
