using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Service.Mapping;
using HNGHRMS.Service.Interface;
using HNGHRMS.Service.Messaging;
using HNGHRMS.Service.ViewModels;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Infrastructure;
namespace HNGHRMS.Service.Implementations
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepository insuranceRepository;
        private readonly IEmployeeRepository employeeRepository;
        private IUnitOfWork unitOfWork;

        public InsuranceService(IInsuranceRepository insuranceRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            this.insuranceRepository = insuranceRepository;
            this.employeeRepository = employeeRepository;
            this.unitOfWork = unitOfWork;
        }
        public GetInsuranceByEmployeeIdResponse GetInsuranceByEmployeeId2(GetInsuranceByEmployeeIdRequest request)
        {
            GetInsuranceByEmployeeIdResponse response = new GetInsuranceByEmployeeIdResponse();          
            
            //var employee = employeeRepository.GetById(request.EmployeeId);
            //double companyInsuraceRate = employee.Company.CompanyInsuranceRatePercent;
            //double labaratorInsuranceRate = employee.Company.LabaratorInsuranceRatePercent;
            //double insuranceLevel = employee.Position.InsuranceRate;

            //Insurance madMadatoryInsurance = insuranceRepository.GetMany(i => i.EmployeeId == request.EmployeeId && i.IsMandatory == true).FirstOrDefault();
            //Insurance voluntaryInsurance = insuranceRepository.GetMany(i => i.EmployeeId == request.EmployeeId && i.IsMandatory == false).FirstOrDefault();
            //if (madMadatoryInsurance != null)
            //    response.MadatoryInsurance = madMadatoryInsurance.ConvertToInsuranceView(labaratorInsuranceRate, companyInsuraceRate, insuranceLevel);                
            //if (voluntaryInsurance != null)
            //    response.VoluntaryInsurance = voluntaryInsurance.ConvertToInsuranceView(labaratorInsuranceRate, companyInsuraceRate, insuranceLevel);
            return response;
        }
        public GetInsuranceByEmployeeIdResponse GetInsuranceByEmployeeId(GetInsuranceByEmployeeIdRequest request)
        {
            GetInsuranceByEmployeeIdResponse response = new GetInsuranceByEmployeeIdResponse();

            var employee = employeeRepository.GetById(request.EmployeeId);
            double companyInsuraceRate = employee.Company.CompanyInsuranceRatePercent;
            double labaratorInsuranceRate = employee.Company.LabaratorInsuranceRatePercent;
            double insuranceLevel = employee.Position.InsuranceRate;

            Insurance madMadatoryInsurance = insuranceRepository.GetMany(i => i.EmployeeId == request.EmployeeId && i.IsMandatory == true &&  i.IsHistory == false).FirstOrDefault();
            Insurance voluntaryInsurance = insuranceRepository.GetMany(i => i.EmployeeId == request.EmployeeId && i.IsMandatory == false && i.IsHistory == false).FirstOrDefault();
            IEnumerable<Insurance> historyInsurance = insuranceRepository.GetMany(i => i.EmployeeId == request.EmployeeId && i.IsHistory == true);
            if (madMadatoryInsurance != null)
                response.MadatoryInsurance = madMadatoryInsurance;
            if (voluntaryInsurance != null)
                response.VoluntaryInsurance = voluntaryInsurance;
            if (historyInsurance.Count() >= 0)
                response.InsuranceHistory = historyInsurance;
            return response;
        }
        public MandatoryInsuranceAddResponse AddMandatoryInsurance(MandatoryInsuranceAddRequest request) {
            MandatoryInsuranceAddResponse response = new MandatoryInsuranceAddResponse();
            Employee emp = employeeRepository.GetById(request.EmployeeId);           
            if (emp != null)
            {
                double rateCompanyInsurance = emp.Company.CompanyInsuranceRatePercent;
                double rateLabaratorInsurance = emp.Company.LabaratorInsuranceRatePercent;
                double valuePositionInsurance = emp.Position.InsuranceRate;
                double companyValue = rateCompanyInsurance * valuePositionInsurance;
                double labaratorValue = rateLabaratorInsurance * valuePositionInsurance;
                emp.MadatoryInsurance = companyValue;
                emp.MadotoryInsuranceDate = request.DateOfIssue;
                Insurance insertedInsurance ;
                if (!request.IsDefined)
                {
                    insertedInsurance = new Insurance()
                    {
                        InsuranceNo = request.InssuranceNo,
                        DateOfIssue = request.DateOfIssue,
                        IsMandatory = true,
                        IsHistory = false,
                        CompanyRatePercent = rateCompanyInsurance,
                        LabaratorRatePercent = rateLabaratorInsurance,
                        Values = labaratorValue,
                        CompanyValue = companyValue,
                        Amount = valuePositionInsurance,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        EmployeeId = request.EmployeeId
                    };
                }
                else {
                    insertedInsurance = new Insurance()
                    {
                       InsuranceNo = request.InssuranceNo,
                        DateOfIssue = request.DateOfIssue,
                        IsMandatory = true,
                        IsHistory = false,
                        CompanyRatePercent = rateCompanyInsurance,
                        LabaratorRatePercent = rateLabaratorInsurance,
                        Values = rateLabaratorInsurance * request.Amount,
                        CompanyValue = rateCompanyInsurance * request.Amount,
                        Amount = request.Amount,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        EmployeeId = request.EmployeeId
                    };
                }               
                try
                {
                    insuranceRepository.Add(insertedInsurance);
                    employeeRepository.Update(emp);
                    SaveInsurance();                    
                    response.Success = true;
                    response.Message = insertedInsurance.Id.ToString();
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "No Employee Id Exits";
            }           
            return response;
        }

        public VoluntaryInsuranceAddResponse AddVoluntaryInsurance(VoluntaryInsuranceAddRequest request)
        {
            VoluntaryInsuranceAddResponse response = new VoluntaryInsuranceAddResponse();
            Insurance insertedInsurance = new Insurance()
            {
                InsuranceNo = request.InssuranceNo,
                DateOfIssue = request.DateOfIssue,
                IsMandatory = false,
                Values = request.Amount,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                EmployeeId = request.EmployeeId,
                AttachFile = request.AttachFile
            };
            try
            {
                insuranceRepository.Add(insertedInsurance);
                SaveInsurance();
                response.Success = true;
                response.Message = insertedInsurance.Id.ToString();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public InsuranceDeleteByIdResponse DeleteInsuranceById(InsuranceDeleteByIdRequest request)
        {
            InsuranceDeleteByIdResponse response = new InsuranceDeleteByIdResponse();
            Insurance insurance = insuranceRepository.GetById(request.insuranceId);
            if (insurance != null) 
            {
                int employeeId = insurance.EmployeeId;
                Employee emp = employeeRepository.GetById(employeeId);
                response.EmployeeId = employeeId;
                GetInsuranceByEmployeeIdRequest insuranceListRequest = new GetInsuranceByEmployeeIdRequest() { EmployeeId = employeeId };
                try
                {
                    insuranceRepository.Delete(insurance);
                    emp.MadatoryInsurance = 0;
                    emp.MadotoryInsuranceDate = null;
                    SaveInsurance();
                    GetInsuranceByEmployeeIdResponse insuranceListResponse = GetInsuranceByEmployeeId(insuranceListRequest);
                    response.InsuranceByEmployee = insuranceListResponse;
                    response.Status = true;
                    
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = ex.Message;
                }
            }
           
            return response;
        }

        public void SaveInsurance()
        {
            this.unitOfWork.Commit();
        }

    }
}
