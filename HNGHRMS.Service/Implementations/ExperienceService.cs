using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
using HNGHRMS.Service.Interface;
using HNGHRMS.Service.Messaging;
using HNGHRMS.Service.Mapping;
using HNGHRMS.Infrastructure.Extensions;
namespace HNGHRMS.Service.Implementations
{
   public  class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository experienceRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IPositionRepository positionRepository;
        private readonly IInsuranceRepository insuranceRepository;
        private IUnitOfWork unitOfWork;


        public ExperienceService(IInsuranceRepository insuranceRepository, IExperienceRepository experienceRepository, IEmployeeRepository employeeRepository,ICompanyRepository companyRepository,IPositionRepository positionRepository, IUnitOfWork unitOfWork)
        {
            this.experienceRepository = experienceRepository;
            this.employeeRepository = employeeRepository;
            this.companyRepository = companyRepository;
            this.positionRepository = positionRepository;
            this.insuranceRepository = insuranceRepository;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Experience> GetExperirenceByEmployeeId(int Id)
        {            
            var experiences = experienceRepository.GetMany(x => x.EmployeeId == Id).OrderByDescending(ord=>ord.Id);
            return experiences;
        }
        public Experience GetExperienceById(int id)
        {
            var experience = experienceRepository.GetById(id);
            return experience;
        }

        public IEnumerable<Experience> GetEmployeesTransferCompanyByDate(int CompanyId, DateTime StartDate, DateTime EndDate)
        {
            var experiences = experienceRepository.GetMany(exp => exp.Employee.CompanyId == CompanyId)
               .Where(exp => exp.TransferDate.CompareBetweenDateByMonthAndYear(StartDate, EndDate));
            return experiences;
        }
        public CreateExperienceForEmployeeResponse CreateNewExperienceForEmployee(CreateExperienceForEmployeeRequest requets)
        {
            CreateExperienceForEmployeeResponse response = new CreateExperienceForEmployeeResponse();

            Employee employeeUpdated = requets.Employee;
            if (employeeUpdated.CompanyId == requets.NewCompanyId &&
                employeeUpdated.PositionId == requets.NewPositionId &&
                employeeUpdated.Departement == requets.NewDepartement)
            {
                response.Status = false;
                response.Message = "Không thay đổi";
            }
            else
            {
                // create new exp 
                Experience experience = new Experience() { 
                    OldCompanyName = employeeUpdated.Company.CompanyName,
                    OldPositionName = employeeUpdated.Position.PositionName,
                    OldDepartement = employeeUpdated.Departement,
                    PositionId = employeeUpdated.PositionId,
                    OldJoinedDate = employeeUpdated.JoinedDate,
                    TransferDate = requets.TransferDate,
                    ExperienceYears = employeeUpdated.WorkingTimeSpan,
                    OldSalary = employeeUpdated.Salary,
                    OldInsurance = employeeUpdated.MadatoryInsurance,
                    Reason = requets.Reason,
                    AttachFile = requets.AttachFile,
                    EmployeeId = employeeUpdated.Id,                 
                    CompanyId = employeeUpdated.CompanyId,                    
                };
                // Make all insurance is history
                IEnumerable<Insurance> insuranceList = insuranceRepository.GetMany(ins => ins.EmployeeId == employeeUpdated.Id);
                foreach (Insurance ins in insuranceList)
                {
                    ins.IsHistory = true;
                    ins.HistoryCompanyName = employeeUpdated.Company.CompanyName;
                    ins.HistoryPositionName = employeeUpdated.Position.PositionName;
                }
                // update new employee infor
                employeeUpdated.JoinedDate = requets.TransferDate;
                employeeUpdated.CompanyId = requets.NewCompanyId;
                employeeUpdated.PositionId = requets.NewPositionId;
                employeeUpdated.Salary = requets.NewSalary;
                employeeUpdated.Departement = requets.NewDepartement;
                employeeUpdated.Status = Model.Enums.EmployeeStatus.Transfer;
                //
                if (employeeUpdated.GetMainSalaryComponent() != null)
                {
                    employeeUpdated.GetMainSalaryComponent().StartApplyDate = requets.TransferDate;
                    employeeUpdated.GetMainSalaryComponent().Amount = requets.NewSalary;
                }
                else
                {
                    EmployeeSalaryComponents empSalaryComponet = new EmployeeSalaryComponents()
                    {
                        SalaryComponentName = "Lương cơ bản",
                        IsSalary = true,
                        IsExtra = true,
                        IsMainSalary = true,
                        StartApplyDate = requets.TransferDate,
                        SalaryPayFrequency = SalaryPayFerequency.Monthly,
                        EndApplyDate = DateTime.MaxValue,
                        Amount = requets.NewSalary,
                        EmployeeId = employeeUpdated.Id,
                    };
                    employeeUpdated.EmployeeSalaryComponents.Add(empSalaryComponet);
                }
                // Check if have tranfer insurance               
                if (requets.IsInsuranceTransfer)
                {
                    double postionInsuranceRate = positionRepository.GetById(requets.NewPositionId).InsuranceRate;
                    double companyInsuranceRate = companyRepository.GetById(requets.NewCompanyId).CompanyInsuranceRatePercent;
                    double labratorInsuranceRate = companyRepository.GetById(requets.NewCompanyId).LabaratorInsuranceRatePercent;
                    string insuranceNo = string.Format("BH/{0}/T/{1}", employeeUpdated.EmployeeCode,requets.InsuranceApplyDate.ToShortDateString());
                    Insurance ins;
                    if (requets.InsuranceTransferAmount != 0)
                    {
                        ins = new Insurance()
                        {
                            DateOfIssue = requets.InsuranceApplyDate,
                            Amount = requets.InsuranceTransferAmount,
                            Values = requets.InsuranceTransferAmount * labratorInsuranceRate,
                            CompanyValue = companyInsuranceRate * requets.InsuranceTransferAmount,
                            CompanyRatePercent = companyInsuranceRate,
                            LabaratorRatePercent = labratorInsuranceRate,
                            IsMandatory = true,
                            IsHistory = false,
                            EmployeeId = employeeUpdated.Id,
                            InsuranceNo = insuranceNo
                        };
                    }
                    else {
                        ins = new Insurance()
                        {
                            DateOfIssue = requets.InsuranceApplyDate,
                            Amount = employeeUpdated.Position.InsuranceRate,
                            CompanyRatePercent = companyInsuranceRate,
                            LabaratorRatePercent = labratorInsuranceRate,
                            Values = postionInsuranceRate * labratorInsuranceRate,
                            CompanyValue = companyInsuranceRate * postionInsuranceRate,
                            IsMandatory = true,
                            IsHistory = false,
                            EmployeeId = employeeUpdated.Id,
                            InsuranceNo = insuranceNo
                        };
                    }
                    insuranceRepository.Add(ins);                   
                }

                try
                {
                    employeeRepository.Update(employeeUpdated);                    
                    experienceRepository.Add(experience);
                    SaveExperience();
                    response.Status = true;
                    response.Message = experience.Id.ToString();
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = "System error";
                }               
            }
            return response;
           
        }

        public IEnumerable<Experience> GetAllExperences()
        {            
            var experiences = experienceRepository.GetAll().OrderByDescending(x=>x.Id);
            return experiences;
        }

       public DeleteExperienceByIdResponse DeleteExperirenceById(int id)
       {
           DeleteExperienceByIdResponse response = new DeleteExperienceByIdResponse(); 
           var experience = experienceRepository.GetById(id);
           if (experience != null)
           {
               try
               {
                   Employee employee = employeeRepository.GetById(experience.EmployeeId);
                   Company com = companyRepository.GetById(experience.CompanyId);
                   Position pos = positionRepository.GetById(experience.PositionId);
                   if (employee != null && com != null && pos != null)
                   {
                       employee.CompanyId = experience.CompanyId;
                       employee.PositionId = experience.PositionId;
                       employee.Departement = experience.OldDepartement;
                       employee.JoinedDate = experience.OldJoinedDate;
                       employee.Salary = experience.OldSalary;
                       employee.Status = Model.Enums.EmployeeStatus.Present;                       
                   }
                   else 
                   {
                       response.Message = "Kinh nghiệm làm việc được xóa nhưng thông tin nhân viên không được phục hồi !";
                   }
                   experienceRepository.Delete(experience);
                   SaveExperience();                   
                   response.Status = true;
                   response.EmployeeId = experience.EmployeeId;
               }
               catch (Exception ex)
               {
                   response.Message = ex.Message;
               }
           }
           else {
               response.Message = "Không tồn tại !";
           }
            return response;
       }
       public void SaveExperience()
        {
            this.unitOfWork.Commit();
        }
    }
}
