using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Data.Repository;
using HNGHRMS.Model.Enums;
using HNGHRMS.Service.Interface;
namespace HNGHRMS.Service.Implementations
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
        public IEnumerable<Termination> GetEmployeesTerminatedByUser(ApplicationUser User)
        {
            IEnumerable<Termination> terminations;
            if (User != null & !User.CompaniesRole.Contains("null"))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<int> companiesList = js.Deserialize<List<int>>(User.CompaniesRole);

                terminations = from ter in terminateRepository.GetMany(te => companiesList.Contains(te.Employee.CompanyId))
                            select ter;
                return terminations;

            }
            return terminations = terminateRepository.GetAll();
        }
        public IEnumerable<Termination> GetEmployeesTerminatedCompanyByDate(int CompanyId, DateTime StartDate, DateTime EndDate)
        {            
            var terminated = terminateRepository.GetMany(ter => ter.Employee.CompanyId == CompanyId)
                .Where(ter=> ter.TerminationDate.CompareBetweenDateByMonthAndYear(StartDate, EndDate));
            return terminated;
        }

        public void CreateEmployeeTerminated(Termination employeeTerminated)
        {
            terminateRepository.Add(employeeTerminated);
            var employee = employeeRepository.GetById(employeeTerminated.Id);
            employee.Status = EmployeeStatus.Terminated;
            employee.TerminatedDate = employeeTerminated.TerminationDate;
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
            employeeTerminated.Employee.TerminatedDate = null;
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
