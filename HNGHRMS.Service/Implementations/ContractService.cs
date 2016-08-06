using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Repository;
using HNGHRMS.Service.Interface;
using HNGHRMS.Service.Messaging;
namespace HNGHRMS.Service.Implementations
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository contractRepository;
        private readonly IContractTypeRepository contractTypeRepository;
        private readonly IUnitOfWork unitOfWork;
        public ContractService(IContractRepository contractRepository,IContractTypeRepository contractTypeRepository, IUnitOfWork unitOfWork)
        {
            this.contractRepository = contractRepository;
            this.contractTypeRepository = contractTypeRepository;
            this.unitOfWork = unitOfWork;
        }
        #region Contracts
        public IEnumerable<Contract> GetContract()
        {
            return this.contractRepository.GetAll();
        }

        public Contract GetContract(int Id)
        {
            return this.contractRepository.GetById(Id);
        }

        public void CreateContract(Contract Contract)
        {
            this.contractRepository.Add(Contract);
            SaveContract();
        }

        public void EditContract(Contract Contract)
        {
            this.contractRepository.Update(Contract);
            SaveContract();
        }

        public void DeleteContract(Contract Contract)
        {
            this.contractRepository.Delete(Contract);
            SaveContract();
        }
        public void DeleteContract(int id)
        {
           this.contractRepository.Delete(con => con.Id == id );
            SaveContract();
        }

        public void SaveContract()
        {
            this.unitOfWork.Commit();
        }
        #endregion

        #region Contract type
        public IEnumerable<ContractType> GetContractTypes()
        {
            return this.contractTypeRepository.GetAll();
        }

        public ContractType GetContractType(int Id)
        {
            return this.contractTypeRepository.GetById(Id);
        }

        public void CreateContractType(ContractType ContractType)
        {
            this.contractTypeRepository.Add(ContractType);
            SaveContractType();
        }

        public void EditContractType(ContractType ContractType)
        {
            this.contractTypeRepository.Update(ContractType);
            SaveContractType();
        }

        public void DeleteContractType(ContractType ContractType)
        {
            this.contractTypeRepository.Delete(ContractType);
            SaveContractType();
        }

        public DeleteContractTypeByIdResponse DeleteContractTypeById(int Id)
        {
            DeleteContractTypeByIdResponse response = new DeleteContractTypeByIdResponse();
            ContractType contractType = contractTypeRepository.GetById(Id);
            int contractUsed = contractType.Contracts.Count();
            if (contractType != null && contractType.Contracts.Count() == 0)
            {
                try
                {
                    contractTypeRepository.Delete(contractType);
                    SaveContractType();
                    response.Status = true;
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = ex.Message;
                }
            }
            else {
                response.Status = false;
                if (contractType == null)
                {
                    response.Message = "Loại hợp đồng không tồn tại";
                }
                else if (contractUsed > 0 )
                {
                    response.Message = "Loại hợp đồng đã được sử dụng, không thể xóa được";
                    response.NumOfContractsUsed = contractUsed;
                }
            }
            return response;
        }
        public void SaveContractType()
        {
            this.unitOfWork.Commit();
        }
        #endregion
    }
}
