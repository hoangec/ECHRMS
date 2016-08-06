using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Model.Models;
using HNGHRMS.Service.Messaging;
namespace HNGHRMS.Service.Interface
{
    public interface IContractService
    {
        IEnumerable<Contract> GetContract();

        Contract GetContract(int Id);
        void CreateContract(Contract Contract);
        void EditContract(Contract Contract);
        void DeleteContract(Contract Contract);

        DeleteContractTypeByIdResponse DeleteContractTypeById(int Id);
        void DeleteContract(int id);
        void SaveContract();

        IEnumerable<ContractType> GetContractTypes();

        ContractType GetContractType(int Id);
        void CreateContractType(ContractType ContractType);
        void EditContractType(ContractType ContractType);
        void DeleteContractType(ContractType ContractType);
        void SaveContractType();
    }
}
