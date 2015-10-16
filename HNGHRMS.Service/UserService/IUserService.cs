using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service
{
    public interface IUserService
    {
        ApplicationUser GetUser(string userId);
        IEnumerable<ApplicationUser> GetUsers();
        IEnumerable<ApplicationUser> GetUsers(string username);
        ApplicationUser GetUserProfile(string userid);
        ApplicationUser GetUsersByEmail(string email);
        IEnumerable<ApplicationUser> SearchUser(string searchString);
     
        void UpdateUser(ApplicationUser user);
        void SaveUser();
        void EditUser(string id, string firstname, string lastname, string email);


        void SaveImageURL(string userId, string imageUrl);
    }
}
