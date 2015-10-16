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
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {

            Mapper.CreateMap<EmployeeFormModel, Employee>();
            Mapper.CreateMap<EmployeeTerminatedFormModel, Termination>();
        }
    }
}