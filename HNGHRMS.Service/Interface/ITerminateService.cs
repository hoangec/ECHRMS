using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service.Interface
{
    public interface ITerminateService
    {
        IEnumerable<Termination> GetEmployeesTerminated();
        IEnumerable<Termination> GetEmployeesTerminatedByUser(ApplicationUser User);
        IEnumerable<Termination> GetEmployeesTerminatedCompanyByDate(int CompanyId,DateTime StartDate, DateTime EndDate);
        Termination GetEmployeeTerminated(int id);

        void CreateEmployeeTerminated(Termination employee);
        void EditEmployeeTerminated(Termination employee);
        void DeleteEmployeeTerminated(Termination employee);
        void SaveEmployeeTerminated();
    }
}
