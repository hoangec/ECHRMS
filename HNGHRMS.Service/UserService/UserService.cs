using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Infrastructure;
using Microsoft.AspNet.Identity;
namespace HNGHRMS.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public ApplicationUser GetUser(string userId)
        {
            return userRepository.Get(u => u.Id == userId);
            
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            var users = userRepository.GetAll();
            return users;
        }

        public IEnumerable<ApplicationUser> GetUsers(string username)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserProfile(string userid)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUsersByEmail(string email)
        {
            var users = userRepository.Get(u => u.Email.Contains(email));
            return users;
        }

        public IEnumerable<ApplicationUser> SearchUser(string searchString)
        {
            var users = userRepository.GetMany(u => u.UserName.Contains(searchString) || u.FirstName.Contains(searchString) || u.LastName.Contains(searchString) || u.Email.Contains(searchString)).OrderBy(u => u.DisplayName);
            return users;
        }

        public void UpdateUser(ApplicationUser user)
        {
            userRepository.Update(user);
            SaveUser();
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        public void EditUser(string id, string firstname, string lastname, string email)
        {
            var user = GetUser(id);
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Email = email;
            UpdateUser(user);
        }

        public void SaveImageURL(string userId, string imageUrl)
        {
            var user = GetUser(userId);
            user.ProfilePicUrl = imageUrl;
            UpdateUser(user);
        }
    }
}
