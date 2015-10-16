using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace HNGHRMS.Service
{
    public interface IRoleService
    {
        IEnumerable<ApplicationRole> GetRoles();
        bool CreateRole(string name);
        bool RoleExists(string name);
    }
}
