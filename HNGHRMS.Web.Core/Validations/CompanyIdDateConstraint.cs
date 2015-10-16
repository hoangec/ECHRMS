using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using HNGHRMS.Infrastructure.Extensions;
namespace HNGHRMS.Web.Core.Validations
{
    public class CompanyIdDateConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext,Route route, string parameterName,RouteValueDictionary values,RouteDirection routeDirection)
        {
            object dataQuery;
            bool result = false;
            if(values.TryGetValue(parameterName,out dataQuery) && dataQuery != null)
            {
                Regex regex = new Regex(@"\d+-\d{2}-\d{2}-\d{4}");
                Match match = regex.Match(dataQuery.ToString());
                if(match.Success)
                {
                    string[] M = dataQuery.ToString().Split('-');
                    var companyId = int.Parse(M[0]);
                    string date = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]);
                    if(companyId > 0 &&  date.ConvertToDate() != DateTime.MinValue)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
