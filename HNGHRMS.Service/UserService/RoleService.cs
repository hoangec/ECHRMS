using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace HNGHRMS.Service
{     
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly RoleManager<ApplicationRole> RoleManager;
        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork, RoleManager<ApplicationRole> roleManager)
        {
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
            this.RoleManager = roleManager;
        }

        public IEnumerable<ApplicationRole> GetRoles()
        {
            var roles = roleRepository.GetAll();
            return roles;
        }
        public bool CreateRole(string name)
        {
            var idResult = RoleManager.Create(new ApplicationRole(name));
            return idResult.Succeeded;
        }

        public bool RoleExists(string name)
        {
            return RoleManager.RoleExists(name);
        }
    }
}
