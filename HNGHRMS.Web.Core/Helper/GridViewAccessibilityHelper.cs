using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
namespace HNGHRMS.Web.Core.Helper
{
    public class GridViewAccessibilityHelper
    {
        public static void ApplyCurrentCulture()
        {
            if (HttpContext.Current.Session["CurrentCulture"] != null)
            {
                CultureInfo ci = (CultureInfo)HttpContext.Current.Session["CurrentCulture"];
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
    }
}
