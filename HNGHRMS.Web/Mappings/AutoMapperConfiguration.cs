using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
namespace HNGHRMS.Web.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => 
            {
                x.AddProfile<DomainToModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}