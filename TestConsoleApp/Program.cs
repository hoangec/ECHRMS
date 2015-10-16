using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Models;
using HNGHRMS.Service;
using HNGHRMS.Service;
using Autofac;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HNGHRMS.Model.Enums;
using HNGHRMS.Infrastructure.Extensions;
namespace TestConsoleApp
{
    public enum TestEnum
    {
        Male = 1,
        Female = 2
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            EmployeeStatus b = EmployeeStatus.Terminated;
            Enum c;
            Gender e = Gender.Female;
            var test = e.GenderTranslate();
            
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>();
            builder.RegisterType<CompanyService>().As<ICompanyService>();
            builder.RegisterType<EmployeeService>().As<ITerminateService>();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            var container = builder.Build();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var companyService = scope.Resolve<ICompanyService>();
            //    var employeeService = scope.Resolve<IEmployeeService>();

            //    var companies = companyService.GetCompanies();
            //    Employee emp = new Employee();
            //    var com = (from c in companies where c.CompanyId == 1 select c).FirstOrDefault();
            //    emp.Name = "NVA";
            //    emp.Address = "11 NDC ";
            //    emp.Company = com;
            //    emp.Departement = "A";
            //    emp.Gender = Gender.Female;
            //    emp.JoinedDate = DateTime.Now;
            //    emp.Salary = 10000000;
            //    employeeService.CreateEmployee(emp);

            //    Console.ReadLine();
            //}

            //using (var db = new HngHrmsEntities())
            //{
            //    //var company = new Company() { CompanyName = "XYZ" };                
            //    //var company = (from c in db.Companies where c.CompanyName.Equals("ABCfsdf") select c).FirstOrDefault();

            //   // var em = new Employee() { Name = "hoang", Company = company };
            //    // db.Companies.Add(company);
            //    //db.Employees.Add(em);
            //    IDbSet<Company> dbSet = db.Set<Company>();
            //    var test = dbSet.ToList();
            //   // int rowsAffected = db.SaveChanges();
            //    //Console.WriteLine("so dong duoc dua vao la: {0}", rowsAffected);
            //    Console.ReadLine();


            //}
        }
    }
}
