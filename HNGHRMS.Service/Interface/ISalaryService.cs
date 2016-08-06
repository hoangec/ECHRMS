using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Service.Messaging;
namespace HNGHRMS.Service.Interface
{
    public interface ISalaryService
    {
       // GetSalaryComponentByEmployeeResponse GetSalaryComponentByEmployee(Employee Employee);
        //GetSalaryComponentByEmployeeResponse GetSalaryComponentByEmployeeId(int Id);

        IEnumerable<EmployeeSalaryComponents> GetSalaryComponentByEmployeeId(int Id);

        AddSalaryComponentForEmployeeResponse AddSalaryComponentForEmployee(AddSalaryComponentForEmployeeRequest request);

        DeleteEmpSalaryComponentResponse DeleteEmpSalaryComponentById(int Id);
        void SaveSalary();

    }
}
