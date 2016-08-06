using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HNGHRMS.Web.ViewModels;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Web.Mappings
{
    public class EmployeeIdentityResolver: ValueResolver<EmployeeInfoModel,Identity>
    {
        protected override Identity ResolveCore(EmployeeInfoModel source)
        {
            return new Identity() { 
                IdentityNo = source.IdentityNo,
                DateOfIssue = source.IdentityDateOfIssue
            };
        }
    }
}