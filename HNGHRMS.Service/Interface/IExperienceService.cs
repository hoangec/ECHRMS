using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Service.Messaging;
namespace HNGHRMS.Service.Interface
{
    public interface IExperienceService
    {
        IEnumerable<Experience> GetExperirenceByEmployeeId(int id);

        IEnumerable<Experience> GetAllExperences();

        IEnumerable<Experience> GetEmployeesTransferCompanyByDate(int CompanyId, DateTime StartDate, DateTime EndDate);

        Experience GetExperienceById(int id);

        CreateExperienceForEmployeeResponse CreateNewExperienceForEmployee(CreateExperienceForEmployeeRequest request);

        DeleteExperienceByIdResponse DeleteExperirenceById(int id);
        void SaveExperience();
    }
}
