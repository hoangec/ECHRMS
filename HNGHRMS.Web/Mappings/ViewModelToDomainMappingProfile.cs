using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HNGHRMS.Web.ViewModels;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        //public override string ProfileName
        //{
        //    //get { return "ViewModelToDomainMappings"; }
        //}

        protected override void Configure()
        {
            Mapper.CreateMap<byte[], byte[]>().ConvertUsing(x => x);
            Mapper.CreateMap<EmployeeSimpleFormModel, Employee>();
            Mapper.CreateMap<EmployeeTerminatedFormModel, Termination>()
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.EmployeeId));
            Mapper.CreateMap<EmployeeTransferFormModel, Experience>()
              .ForMember(dest => dest.OldCompanyName, opt => opt.MapFrom(src => src.CompanyName))
              .ForMember(dest => dest.OldPositionName, opt => opt.MapFrom(src => src.PositionName))
              .ForMember(dest => dest.OldSalary, opt => opt.MapFrom(src => src.Salary));              
            Mapper.CreateMap<EmployeeInfoModel, Employee>()
                  .ForMember(dest => dest.Identity, opt => opt.ResolveUsing<EmployeeIdentityResolver>());
            Mapper.CreateMap<EmployeeContactTabViewModel, Employee>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore());
            Mapper.CreateMap<EmployeeJobTabViewModel, Employee>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.Status, opt => opt.Ignore());
            Mapper.CreateMap<EmployeeContractAddFormModel,Contract>()
                  .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeContractId));
            Mapper.CreateMap<EmployeeContractUpdateFormModel, Contract>()
                  .ForMember(dest => dest.ContractNo, opt => opt.MapFrom(src => src.ContractUpdateNo))
                  .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ContractUpdateStartDate))
                  .ForMember(dest => dest.ContractTypeId, opt => opt.MapFrom(src => src.ContractUpdateTypeId))
                  .ForMember(dest => dest.ContractAttachFile, opt => opt.MapFrom(src => src.ContractUpdateAttachFile))                  
                  .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.ContractUpdateRemark));
                 
        }
    }
}