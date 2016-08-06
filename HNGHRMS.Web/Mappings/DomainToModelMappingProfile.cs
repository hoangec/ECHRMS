using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HNGHRMS.Model.Models;
using HNGHRMS.Web.ViewModels;
using HNGHRMS.Infrastructure.Extensions;
namespace HNGHRMS.Web.Mappings
{
    public class DomainToModelMappingProfile : Profile
    {
        //public override string ProfileName
        //{
        //    //get
        //    //{
        //    //    return "DomainToViewModelMappings";
        //    //}
        //}

        protected override void Configure()
        {
           // Mapper.CreateMap<Company, CompanyViewModel>();
            Mapper.CreateMap<Company, CompanyEmployeeQuantityReportViewModel>();
            Mapper.CreateMap<Company, CompanySalaryReportViewModel>();
            Mapper.CreateMap<Company, CompanyInsuranceReportViewModel>();
            Mapper.CreateMap<Company, CompanyConfigModel>();
            Mapper.CreateMap<ContractType, ContractTypeConfigModel>();


            Mapper.CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<Employee, EmployeeViewModel>()
                  //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                  .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                  .ForMember(dest => dest.PositionName,opt=>opt.MapFrom(src=>src.Position.PositionName))
                  .ForMember(dest=>dest.FullName,opt=>opt.MapFrom(src=>src.LastName + " " + src.FirstName));

            Mapper.CreateMap<Employee, NewEmployeeReportGridViewModel>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.PositionName));

            Mapper.CreateMap<Employee, EmployeeTerminateViewModel>()
                   .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                  .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))                  
                  .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Termination.Reason))
                  .ForMember(dest => dest.TerminationDate, opt => opt.MapFrom(src => src.Termination.TerminationDate));


            Mapper.CreateMap<Termination, EmployeeTerminateViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Employee.Address))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Employee.Gender))
                .ForMember(dest => dest.Departement, opt => opt.MapFrom(src => src.Employee.Departement))
                .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.Employee.Position.Id))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Employee.Position.PositionName))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Employee.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Employee.Company.CompanyName))
                .ForMember(dest => dest.JoinedDate, opt => opt.MapFrom(src => src.Employee.JoinedDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Employee.Salary))                
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Employee.Nationality));
            Mapper.CreateMap<Termination, TerminateReportGridViewModel>()
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Employee.FullName))            
             .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Employee.Gender))
             .ForMember(dest => dest.Departement, opt => opt.MapFrom(src => src.Employee.Departement))
             .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.Employee.Position.Id))
             .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Employee.Position.PositionName))            
             .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Employee.Company.CompanyName))
             .ForMember(dest => dest.JoinedDate, opt => opt.MapFrom(src => src.Employee.JoinedDate))
             .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Employee.Salary))
             .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Employee.Nationality));
            Mapper.CreateMap<Contract, EmployeeContractsViewModel>()
                .ForMember(dest => dest.EmployeeContractId, opt => opt.MapFrom(src => src.Id));
            
            Mapper.CreateMap<Employee, EmployeeContactTabViewModel>()
                .ForMember(dest => dest.EmployeeContactId, opt => opt.MapFrom(src => src.Id));
            Mapper.CreateMap<Employee, EmployeeJobTabViewModel>()
                .ForMember(dest => dest.EmployeeJobId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<Position, PositionConfigModel>();

            Mapper.CreateMap<Insurance, InsuranceGridView>()
                  .ForMember(dest => dest.InsuranceType , opt => opt.ResolveUsing<InsuranceTypeReslover>());

            Mapper.CreateMap<Experience, TransferEmployeeGridViewModel>()
                 .ForMember(dest => dest.ExperienceId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                 .ForMember(dest => dest.OldCompanyId, opt => opt.MapFrom(src => src.CompanyId));
            Mapper.CreateMap<Experience, TransferReportGridViewModel>()
                .ForMember(dest=>dest.CompanyName , opt => opt.MapFrom(src =>src.Employee.Company.CompanyName))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee.Id))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.Employee.PositionId))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Employee.Position.PositionName))
                .ForMember(dest => dest.DepartementName, opt => opt.MapFrom(src => src.Employee.Departement))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Employee.Nationality))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Employee.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Employee.Salary));

            Mapper.CreateMap<EmployeeSalaryComponents, EmployeeSalaryComponentViewModel>();          
        }
    }
}