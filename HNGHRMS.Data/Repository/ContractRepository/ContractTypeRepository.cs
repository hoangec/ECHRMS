using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Repository;
namespace HNGHRMS.Data.Repository
{
    public class ContractTypeRepository : RepositoryBase<ContractType> , IContractTypeRepository
    {
        public ContractTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
