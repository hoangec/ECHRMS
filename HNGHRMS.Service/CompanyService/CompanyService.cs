using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Repository;
using System.Web.Script.Serialization;
namespace HNGHRMS.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;

        public CompanyService(ICompanyRepository companyRepository,IUnitOfWork unitOfWork)
        {
            this.companyRepository = companyRepository;
            this.unitOfWork = unitOfWork;
        }
        #region ICompanyService Members
        public IEnumerable<Company> GetCompanies()
        {
            var companies = companyRepository.GetAll();
            return companies;
        }
        public IEnumerable<Company> GetCompaniesByUser(ApplicationUser user){
            IEnumerable<Company> companies;
            if(user != null & !user.CompaniesRole.Contains("null"))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<int> companiesList = js.Deserialize<List<int>>(user.CompaniesRole);
                
                companies = from c in companyRepository.GetMany(co => companiesList.Contains(co.CompanyId))
                            select c;
                return companies;
                
            }
            return companies = companyRepository.GetAll();
        }
        public Company GetCompany(int CompanyId)
        {
            var company = companyRepository.GetById(CompanyId);
            return company;
        }

        public void CreateCompany(Company Company){
            companyRepository.Add(Company);
            SaveCompany();
        }
        public void EditCompany(Company Company){
            companyRepository.Update(Company);
            SaveCompany();
        }
        public void DeleteCompany(Company Company){
            companyRepository.Delete(Company);
            SaveCompany();
        }
       
        public void SaveCompany()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
