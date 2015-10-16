using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Data.Repository;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Service
{
    public class TerminateService : ITerminateService
    {
        private readonly ITerminateRepository terminateRepository;
        private readonly IEmployeeRepository employeeRepository;
        private IUnitOfWork unitOfWork;
        public TerminateService(ITerminateRepository TerminateRepository,IEmployeeRepository EmployeeRepository ,IUnitOfWork UnitOfWork)
        {
            this.terminateRepository = TerminateRepository;
            this.employeeRepository = EmployeeRepository;
            this.unitOfWork = UnitOfWork;
        }

        #region TerminateService Members

        public Termination GetEmployeeTerminated(int id)
        {
            var termination = terminateRepository.GetById(id);
            return termination;
        }
        public IEnumerable<Termination> GetEmployeesTerminated()
        {
            var terminations = terminateRepository.GetAll();
            return terminations;

        }

        public void CreateEmployeeTerminated(Termination employeeTerminated)
        {
            terminateRepository.Add(employeeTerminated);
            var employee = employeeRepository.GetById(employeeTerminated.EmployeeId);
            employee.Status = EmployeeStatus.Terminated;
            SaveEmployeeTerminated();            
        }

        public void EditEmployeeTerminated(Termination employeeTerminated)
        {
            terminateRepository.Update(employeeTerminated);
            SaveEmployeeTerminated();
        }
        public void DeleteEmployeeTerminated(Termination employeeTerminated)
        {
            employeeTerminated.Employee.Status = EmployeeStatus.Present;
            terminateRepository.Delete(employeeTerminated);            
            SaveEmployeeTerminated();
        }
        public void SaveEmployeeTerminated()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
