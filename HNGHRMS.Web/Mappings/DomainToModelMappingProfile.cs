using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HNGHRMS.Model.Models;
using HNGHRMS.Web.ViewModels;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Web.Core.Mapping;
namespace HNGHRMS.Web.Mappings
{
    public class DomainToModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Company, CompanyViewModel>();
            Mapper.CreateMap<Company, CompanyReportViewModel>();
            Mapper.CreateMap<Company, CompanyManageModel>();
            Mapper.CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<Employee, EmployeeViewModel>()
                  //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                  .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName));
            Mapper.CreateMap<Employee, EmployeeTerminateViewModel>()
                   .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.CompanyId))
                  .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                  .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Termination.Reason))
                  .ForMember(dest => dest.TerminationDate, opt => opt.MapFrom(src => src.Termination.TerminationDate));
            Mapper.CreateMap<Termination, EmployeeTerminateViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Employee.Address))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Employee.Gender))
                .ForMember(dest => dest.Departement, opt => opt.MapFrom(src => src.Employee.Departement))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Employee.Position))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Employee.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Employee.Company.CompanyName))
                .ForMember(dest => dest.JoinedDate, opt => opt.MapFrom(src => src.Employee.JoinedDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Employee.Salary))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Employee.Status));            


        }
    }
}