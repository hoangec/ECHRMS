using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HNGHRMS.Web.ViewModels;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Web.Mappings
{
    public class InsuranceTypeReslover : ValueResolver<Insurance,string>
    {
        protected override string ResolveCore(Insurance source)
        {
            if (source.IsMandatory)
                return "Bắt buộc";
            else
                return "TỰ nguyện";
        }
    }
}