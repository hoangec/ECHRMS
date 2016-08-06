using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Service.ViewModels;
using HNGHRMS.Service.Messaging;
using HNGHRMS.Service.Interface;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Service.Mapping;
using HNGHRMS.Model.Models;
using AutoMapper;
namespace HNGHRMS.Service.Implementations
{
    public class SalaryService : ISalaryService
    {
        private readonly IEmployeeSalaryComponentRepository empSalaryComponentRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUnitOfWork unitOfWork;
        public SalaryService(IEmployeeRepository employeeRepository, IEmployeeSalaryComponentRepository empSalaryComponentRepository, IUnitOfWork unitOfWork)
        {
            this.empSalaryComponentRepository = empSalaryComponentRepository;
            this.employeeRepository = employeeRepository;
            this.unitOfWork = unitOfWork;
        }

        //public GetSalaryComponentByEmployeeResponse GetSalaryComponentByEmployee(Employee Employee)
        //{
        //    GetSalaryComponentByEmployeeResponse response = new GetSalaryComponentByEmployeeResponse();
        //    var empSalaryComponents = empSalaryComponentRepository.GetMany(item => item.EmployeeId == Employee.Id);            
        //    EmployeeSalaryComponentViewModel employeeFixSalary = new EmployeeSalaryComponentViewModel()
        //    {
        //        Id = 0,
        //        Amount = Employee.Salary,
        //        SalaryComponentName = "Lương cố định",
        //        SalaryComponentType = "Cố định",
        //        SalaryPayFerequency = "Hàng tháng",
        //        ApplyDate = Employee.JoinedDate
        //    };
        //    response.EmployeeSalaryList.Add(employeeFixSalary);
        //    if (empSalaryComponents != null)
        //    {
        //        foreach (EmployeeSalaryComponents empSalaryCmp in empSalaryComponents)
        //        {
        //            response.EmployeeSalaryList.Add(empSalaryCmp.ConvertToEmployeeSalaryComponnetView());
        //        }
        //    }
            
        //    return response;
        //}
        //public GetSalaryComponentByEmployeeResponse GetSalaryComponentByEmployeeId(int Id)
        //{
        //    GetSalaryComponentByEmployeeResponse response = new GetSalaryComponentByEmployeeResponse();
        //    var empSalaryComponents = empSalaryComponentRepository.GetMany(item => item.EmployeeId == Id);
        //    Employee emp = employeeRepository.GetById(Id);
        //    EmployeeSalaryComponentViewModel employeeFixSalary = new EmployeeSalaryComponentViewModel()
        //    {
        //        Id = 0,
        //        Amount = emp.Salary,
        //        SalaryComponentName = "Lương cố định",
        //        SalaryComponentType = "Cố định",
        //        SalaryPayFerequency = "Hàng tháng",
        //        ApplyDate = emp.JoinedDate
        //    };
        //    response.EmployeeSalaryList.Add(employeeFixSalary);
        //    if (empSalaryComponents != null)
        //    {
        //        foreach (EmployeeSalaryComponents empSalaryCmp in empSalaryComponents)
        //        {
        //            response.EmployeeSalaryList.Add(empSalaryCmp.ConvertToEmployeeSalaryComponnetView());
        //        }
        //    }

        //    return response;
        //}

        public IEnumerable<EmployeeSalaryComponents> GetSalaryComponentByEmployeeId(int Id)
        {
            var empSalaryComponents = empSalaryComponentRepository.GetMany(item => item.EmployeeId == Id);
            return empSalaryComponents;
        }
  
        public AddSalaryComponentForEmployeeResponse AddSalaryComponentForEmployee(AddSalaryComponentForEmployeeRequest request)
        {
            AddSalaryComponentForEmployeeResponse response = new AddSalaryComponentForEmployeeResponse();
     
            
            EmployeeSalaryComponents empSalaryComponent = new EmployeeSalaryComponents()
            {
                EmployeeId = request.EmployeeId,     
                SalaryComponentName = request.SalaryComponentName,
                IsExtra = request.IsExtra,          
                SalaryPayFrequency = request.SalaryPayFerequency,
                Remark = request.Remark,               
            };
            if(request.IsMainSalary)
            {
                EmployeeSalaryComponents currentEmpSalaryComponent = GetMainEmployeeSalaryComponent(request.EmployeeId);
                Employee employee = employeeRepository.GetById(request.EmployeeId);
                empSalaryComponent.StartApplyDate = request.ApplyDate;
                empSalaryComponent.EndApplyDate = DateTime.MaxValue;
                empSalaryComponent.IsMainSalary = true;
                empSalaryComponent.IsSalary = true;
                employee.Salary = request.Amount;
                if (currentEmpSalaryComponent != null)
                {
                    currentEmpSalaryComponent.IsMainSalary = false;
                    currentEmpSalaryComponent.EndApplyDate = request.ApplyDate.AddDays(-1);                        
                }
            }
            else{
                empSalaryComponent.StartApplyDate = request.ApplyDate;
                empSalaryComponent.EndApplyDate = request.EndApplyDate;
                empSalaryComponent.IsMainSalary = false;
                empSalaryComponent.IsSalary = false;
            }
            //
            if (!request.IsExtra && request.IsMainSalary)
                 empSalaryComponent.Amount = request.Amount  ;
            else if (!request.IsExtra)
                empSalaryComponent.Amount = 0 - request.Amount;
            else {
                empSalaryComponent.Amount = request.Amount;
            }
            //
            if (request.SalaryPayFerequency == Model.Enums.SalaryPayFerequency.OneTime)
                empSalaryComponent.EndApplyDate = new DateTime(request.ApplyDate.Year, request.ApplyDate.Month, DateTime.DaysInMonth(request.ApplyDate.Year, request.ApplyDate.Month));        
            //          
            try
            {
                empSalaryComponentRepository.Add(empSalaryComponent);               
                SaveSalary();
                response.Status = true;
                response.Message = empSalaryComponent.Id.ToString();
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }                
            return response;
            
        }

        public DeleteEmpSalaryComponentResponse DeleteEmpSalaryComponentById(int Id)
        {
            DeleteEmpSalaryComponentResponse response = new DeleteEmpSalaryComponentResponse();
            EmployeeSalaryComponents empSalaryComponent = empSalaryComponentRepository.GetById(Id);
            Employee emp = empSalaryComponent.Employee;
            if (empSalaryComponent != null)
            {
                if (empSalaryComponent.IsMainSalary)
                {
                    response.Message = "Đã xóa mức lương chính thức của nhân viên, mức lương hiện tại sẽ bằng 0";                 
                    emp.Salary = 0;
                }
                else {
                    response.Message = "Đã xóa chi phí";
                }
                try
                {
                    empSalaryComponentRepository.Delete(empSalaryComponent);
                    SaveSalary();
                    response.Status = true;
                    response.EmployeeId = emp.Id;
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = ex.Message;
                }
            }
            else {
                response.Status = false;
                response.Message = "Không tồn tại mã cần xóa";
            }
            return response;
        }
        private EmployeeSalaryComponents GetMainEmployeeSalaryComponent(int EmployeeId)
        {
            return empSalaryComponentRepository.Get(empSlr => empSlr.EmployeeId == EmployeeId && empSlr.IsMainSalary);
        }
        public void SaveSalary()
        {
            unitOfWork.Commit();
        }
    }
}
