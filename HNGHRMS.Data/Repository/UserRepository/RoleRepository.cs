using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Data.Repository
{
    public class RoleRepository : RepositoryBase<ApplicationRole>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
