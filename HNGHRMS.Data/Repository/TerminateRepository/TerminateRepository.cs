using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Infrastructure;
namespace HNGHRMS.Data.Repository
{
    public class TerminateRepository : RepositoryBase<Termination>, ITerminateRepository
    {
        public TerminateRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        { }
    }
}
