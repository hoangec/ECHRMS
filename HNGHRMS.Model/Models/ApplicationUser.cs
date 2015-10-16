using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
namespace HNGHRMS.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }

        public string ProfilePicUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastLoginTime { get; set; }

        public bool Activated { get; set; }

        public string RoleId { get; set; }

        public ApplicationUser()
        {
            DateCreated = DateTime.Now;            
        }
        public string CompaniesRole { get; set; }
        public string DisplayName { get { return string.Format("{0} {1}",FirstName,LastName); } }
    }
}
