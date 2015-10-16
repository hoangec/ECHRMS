using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Models;
using HNGHRMS.Service;
using HNGHRMS.Model.Models;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using HNGHRMS.Web.Mappings;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace HNGHRMS
{
    public static class BootStrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }
        public static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            //
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerRequest();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerRequest();

            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerRequest();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerRequest();

            builder.RegisterType<TerminateService>().As<ITerminateService>().InstancePerRequest();
            builder.RegisterType<TerminateRepository>().As<ITerminateRepository>().InstancePerRequest();

            builder.RegisterType<ReportService>().As<IReportService>().InstancePerRequest();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();

            builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HngHrmsEntities())))
               .As<UserManager<ApplicationUser>>().InstancePerRequest();
           
            builder.Register(c => new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new HngHrmsEntities())))
              .As<RoleManager<ApplicationRole>>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }

   
}