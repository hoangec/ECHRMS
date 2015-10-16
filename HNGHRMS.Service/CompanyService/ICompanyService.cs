using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetCompanies();

        IEnumerable<Company> GetCompaniesByUser(ApplicationUser user);
        Company GetCompany(int Id);
        void CreateCompany(Company Company);
        void EditCompany(Company Company);
        void DeleteCompany(Company Company);
        void SaveCompany();
    }
}
