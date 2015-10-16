using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Web.Core.Extensions
{
    public static class SelectListExtensions
    {
       public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<Company> companies,int selectedId = 0)
        {
            return companies.OrderBy(com => com.CompanyId)
                             .Select(com =>
                                 new SelectListItem
                                 {
                                     Selected = (com.CompanyId == selectedId),
                                     Text = com.CompanyName,
                                     Value = com.CompanyId.ToString()
                                 });
        }
    }
}
