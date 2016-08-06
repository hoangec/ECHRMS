using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HNGHRMS.Service.ViewModels;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service
{
    public  class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        { 
            // 
            Mapper.CreateMap<Insurance, InsuranceView>()
                    .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<EmployeeSalaryComponents, EmployeeSalaryComponentViewModel>();

        }
    }
}
