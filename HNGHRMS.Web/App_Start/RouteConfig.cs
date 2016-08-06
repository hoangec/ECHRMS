using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HNGHRMS.Web.Validations;
namespace HNGHRMS {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
            routes.MapRoute(
                name: "NewEmployeesReport",
                url: "Home/NewEmployeesReport/{DataQuery}",
                defaults: new { controller = "Home", action = "NewEmployeesReport" },
                constraints: new { DataQuery = new CompanyIdDateConstraint() }
            );
            routes.MapRoute(
                "CurrentEmployeesReport",
                "Home/CurrentEmployeesReport/{DataQuery}",
                new { controller = "Home", action = "CurrentEmployeesReport" },
                new { DataQuery = new CompanyIdDateConstraint()}
            );
            routes.MapRoute(
                name: "TerminatedEmployeesReport",
                url: "Home/TerminatedEmployeesReport/{DataQuery}",
                defaults: new { controller = "Home", action = "TerminatedEmployeesReport" },
                constraints: new { DataQuery = new CompanyIdDateConstraint() }
            );
            routes.MapRoute(
             name: "CompanyReport",
             url: "Home/CompanyReportByDate/{selectedDate}",
             defaults: new { controller = "Home", action = "ComapanyReportByDate" }
            );
            routes.MapRoute(
                 name: "Employee",
             url: "Employee/Index/{id}",
             defaults: new { controller = "Employee", action = "Index" , id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default", // Route name
                url: "{controller}/{action}/{id}", // URL with parameters
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
      
        }
    }
}