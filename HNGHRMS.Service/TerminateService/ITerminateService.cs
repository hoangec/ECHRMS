using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service
{
    public interface ITerminateService
    {
        IEnumerable<Termination> GetEmployeesTerminated();
        Termination GetEmployeeTerminated(int id);

        void CreateEmployeeTerminated(Termination employee);
        void EditEmployeeTerminated(Termination employee);
        void DeleteEmployeeTerminated(Termination employee);
        void SaveEmployeeTerminated();
    }
}
