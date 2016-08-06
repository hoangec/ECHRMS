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
            return companies.OrderBy(com => com.Id)
                             .Select(com =>
                                 new SelectListItem
                                 {
                                     Selected = (com.Id == selectedId),
                                     Text = com.CompanyName,
                                     Value = com.Id.ToString()
                                 });
        }

       public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<Position> positions, int selectedId = 0)
       {
           return positions.OrderBy(pos => pos.Id)
                            .Select(pos =>
                                new SelectListItem
                                {
                                    Selected = (pos.Id == selectedId),
                                    Text = pos.PositionName,
                                    Value = pos.Id.ToString()
                                });
       }
    }
}
