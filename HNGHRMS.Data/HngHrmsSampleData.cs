using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HNGHRMS.Data.Models;
using HNGHRMS.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace HNGHRMS.Data
{
    public class HngHrmsSampleData : DropCreateDatabaseIfModelChanges<HngHrmsEntities>
    {
        protected override void Seed(HngHrmsEntities context)
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
            context.Roles.Add(adminRole);
            context.Roles.Add(managerRole);
            context.Roles.Add(superUserRole);
            context.Roles.Add(userRole);

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
