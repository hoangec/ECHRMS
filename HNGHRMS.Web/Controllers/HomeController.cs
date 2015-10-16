using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNGHRMS.Model.Models;
using HNGHRMS.Service;
using HNGHRMS.Web.ViewModels;
using AutoMapper;
using DevExpress.Web.Mvc;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Web.Core.Extensions;
namespace HNGHRMS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IEmployeeService employeeService;
        private readonly IReportService reportService;
        public HomeController(ICompanyService CompanyService,IEmployeeService EmployeeService,IReportService ReportService)
        {
            this.companyService = CompanyService;
            this.employeeService = EmployeeService;
            this.reportService = ReportService;
        }
        public ActionResult Index()
        {
            IList<EmployeeTypeQuantity> employeeQtyByDate = GetEmployeeTypeQuantityByDate(DateTime.Now);
            IList<EmployeeTypeQuantity> employeeQtyByCompany = GetEmployeeTypeQuantityByCompany(1);
            CompanySalaryReport companySalaryReport = GetSalayReport();
            EmployeeQuantityReport companyEmployeesQuantityReport = GetEmployeesQuantityReport();

            var companies = companyService.GetCompanies();

            ViewData["EmployeeTotalSalaryChart"] = companySalaryReport.SalaryChart;
            ViewData["TotalSalary"] = companySalaryReport.TotalSalary;
            ViewData["EmployeeTotalQuantityChart"] = companyEmployeesQuantityReport.QuantityChart;
            ViewData["TotalEmployeeQuantity"] = companyEmployeesQuantityReport.TotalQuantity;
            ViewData["EmployeeByDateChart"] = employeeQtyByDate;
            ViewData["EmployeeByCompanyChart"] = employeeQtyByCompany;
            ViewData["Companies"] =  companies ;
            return View();
        }

        #region General Report Table
        [ValidateInput(false)]        
        public ActionResult CompanyReportByDate(DateTime selectedDate)
        {
            DateTime date;
            if(selectedDate != null)
            {
                date = selectedDate;
            }
            else
            {
                date = DateTime.Now;
            }
           
            var companies = companyService.GetCompanies();
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyReportViewModel>>(companies);
            for(int i = 0; i < companies.Count();i++)
            {
                companiesView.ElementAt(i).CurrentEmployees = companies.ElementAt(i).GetNumOfEmployeesByDate(date);
                companiesView.ElementAt(i).NewEmployees = companies.ElementAt(i).GetNumOfEmployeesJoinByDate(date);
                companiesView.ElementAt(i).TerminatedEmployees = companies.ElementAt(i).GetNumOfEmployeesTerminatedByDate(date);
                companiesView.ElementAt(i).TotalSalary = companies.ElementAt(i).GetSumSalaryEmployeesByDate(date);
                companiesView.ElementAt(i).DateReport = date.ToString("dd/MM/yyyy");
            }
            return PartialView("_CompanyReportGridView", companiesView);               

        }
        public ActionResult CurrentEmployeesReport(string DataQuery)
        {
            int companyId;
            DateTime choicedDate;
            var companies = companyService.GetCompanies();
            if (!string.IsNullOrEmpty(DataQuery))
            {

                string[] M = DataQuery.Split('-');
                companyId = int.Parse(M[0]);
                choicedDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDate();
            }
            else
            {
               companyId = companies.FirstOrDefault().CompanyId;
                choicedDate = DateTime.Now;
            }

            var employees = employeeService.GetCurrentEmployeesByCompany(companyId, choicedDate);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            ViewBag.CompanyId = companyId;
            ViewBag.ChoiceDate = choicedDate;

            ViewData["SelectedCompany"] = companyId;
            ViewData["companies"] = companies.ToSelectListItems(companyId);
            ViewData["employess"] = employeesView;
            return View();
        }
        public ActionResult NewEmployeesReport(string DataQuery)
        {
            int companyId;
            DateTime choicedDate;
            var companies = companyService.GetCompanies();
            if (!string.IsNullOrEmpty(DataQuery))
            {

                string[] M = DataQuery.Split('-');
                companyId = int.Parse(M[0]);
                choicedDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDate();
            }
            else
            {
                companyId = companies.FirstOrDefault().CompanyId;
                choicedDate = DateTime.Now;
            }

            var employees = employeeService.GetNewEmployeesByCompany(companyId, choicedDate);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            ViewBag.CompanyId = companyId;
            ViewBag.ChoiceDate = choicedDate;

            ViewData["SelectedCompany"] = companyId;
            ViewData["companies"] = companies.ToSelectListItems(companyId);
            ViewData["employess"] = employeesView;
            return View();
        }
        public ActionResult TerminatedEmployeesReport(string DataQuery)
        {
            int companyId;
            DateTime choicedDate;
            var companies = companyService.GetCompanies();
            if (!string.IsNullOrEmpty(DataQuery))
            {

                string[] M = DataQuery.Split('-');
                companyId = int.Parse(M[0]);
                choicedDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDate();
            }
            else
            {
                companyId = companies.FirstOrDefault().CompanyId;
                choicedDate = DateTime.Now;
            }
            ViewBag.CompanyId = companyId;
            ViewBag.ChoiceDate = choicedDate;

            var employees = employeeService.GetTerminatedEmployeesByCompany(companyId, choicedDate);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeTerminateViewModel>>(employees);

            ViewData["SelectedCompany"] = companyId;
            ViewData["companies"] = companies.ToSelectListItems(companyId);
            ViewData["employess"] = employeesView;
            return View();
        }
        #endregion

        #region Detail Report Table
        [ValidateInput(false)]
        public ActionResult NewEmployeeReportGridView()
        {
            int CompanyId = int.Parse(Request.Params["CompanyId"]);
            DateTime DateSelected = DateTime.Parse(Request.Params["SelectedDate"]);
            var employees = employeeService.GetNewEmployeesByCompany(CompanyId, DateSelected);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return PartialView("_NewEmployeeReportGridView", employeesView);
        }

        [ValidateInput(false)]
        public ActionResult CurrentEmployeeReportGridView()
        {
            int CompanyId = int.Parse(Request.Params["CompanyId"]);
            DateTime DateSelected = DateTime.Parse(Request.Params["SelectedDate"]);
            var employees = employeeService.GetCurrentEmployeesByCompany(CompanyId, DateSelected);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return PartialView("_CurrentEmployeeReportGridView", employeesView);
        }

        [ValidateInput(false)]
        public ActionResult TerminatedEmployeeGridView()
        {
            int companyId = int.Parse(Request.Params["CompanyId"]);
            DateTime dateSelected = DateTime.Parse(Request.Params["SelectedDate"]);
            var employees = employeeService.GetTerminatedEmployeesByCompany(companyId, dateSelected);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeTerminateViewModel>>(employees);
            return PartialView("_TerminatedEmployeeGridView", employeesView);
        }
        #endregion
        
        #region Chart
        public ActionResult GetCompanyTotalSalaryChart(string ViewOptions = "V")
        {

            if(ViewOptions == "P")
            {
                ViewData["Options"] = "P";
            }else
            {
                ViewData["Options"] = "V";
            }
            CompanySalaryReport companySalaryReport = GetSalayReport();

            return PartialView("_CompanyTotalSalaryChart", companySalaryReport.SalaryChart);
        }
        public ActionResult GetCompanyTotalEmployeeQuantityChart(string ViewOptions = "V")
        {
            if (ViewOptions == "P")
            {
                ViewData["Options"] = "P";
            }
            else
            {
                ViewData["Options"] = "V";
            }
            EmployeeQuantityReport companyTotalEmployeeQuantityReport = GetEmployeesQuantityReport();

            return PartialView("_CompanyTotalEmployeeQuantityReport", companyTotalEmployeeQuantityReport.QuantityChart);
        }
        public ActionResult GetEmployeesCompaniesByDateChart(DateTime Date)
        {
           // DateTime dateSelected = Date.ConvertToDate();
            IList<EmployeeTypeQuantity> employeesCompaniesDate = GetEmployeeTypeQuantityByDate(Date);
            return PartialView("_EmployeesCompaniesByDateChart", employeesCompaniesDate);
        }
        public ActionResult GetEmployeesCompaniesByCompanyChart(int CompanyId)
        {
            IList<EmployeeTypeQuantity> employeeQtyByCompany = GetEmployeeTypeQuantityByCompany(CompanyId);
            return PartialView("_EmployeesCompaniesByCompanyChart", employeeQtyByCompany);
        }
        public IList<EmployeeTypeQuantity> GetEmployeeTypeQuantityByDate(DateTime Date)
        {
            List<EmployeeTypeQuantity> result = new List<EmployeeTypeQuantity>();
            string[] types = new string[] { "Tuyển mới", "Thôi việc" };
            var companies = companyService.GetCompanies();
            Dictionary<string, IList<int>> values = new Dictionary<string, IList<int>>();
            values.Add(types[0],reportService.GetNewEmployeeQuantity(companies,Date));
            values.Add(types[1], reportService.GetTerminatedEmployeeQuantity(companies,Date));
            
            foreach (string type in types)
                for (int i = 0; i < companies.Count(); i++)
                    result.Add(new EmployeeTypeQuantity(companies.ElementAt(i).CompanyName, type, values[type][i]));
            return result;
        }
        public IList<EmployeeTypeQuantity> GetEmployeeTypeQuantityByCompany(int CompanyId)
        {
            List<EmployeeTypeQuantity> result = new List<EmployeeTypeQuantity>();
            Company company;
            if (CompanyId > 0)
            {
                company = companyService.GetCompany(CompanyId);
            }
            else
            {
                company = companyService.GetCompanies().FirstOrDefault();
            }
            if(company != null)
            {
                string[] types = new string[] { "Tuyển mới", "Thôi việc" };
                string[] months = new string[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
                Dictionary<string, IList<int>> values = new Dictionary<string, IList<int>>();
                values.Add(types[0], reportService.GetNewEmployeeQuantityByCompany(company));
                values.Add(types[1], reportService.GetTerminatedEmployeeQuantityByCompany(company));

                foreach (string type in types)
                    for (int i = 0; i < months.Length; i++)
                        result.Add(new EmployeeTypeQuantity(months[i], type, values[type][i]));
            }            
            return result;
        }
        public CompanySalaryReport GetSalayReport()
        {
            var companies = companyService.GetCompanies();
            CompanySalaryReport result = new CompanySalaryReport();
            double totalSalary = 0;
            foreach(Company com in companies)
            {
                double salary = com.GetSumSalaryEmployeesByDate(DateTime.Now);
                totalSalary += salary;
                result.SalaryChart.Add(new QuantityChartModel(com.CompanyName, salary));           
            }
            result.TotalSalary = totalSalary;

            return result;
        }

        public EmployeeQuantityReport GetEmployeesQuantityReport()
        {
            var companies = companyService.GetCompanies();
            EmployeeQuantityReport result = new EmployeeQuantityReport();
            int totalQuantity = 0;
            foreach (Company com in companies)
            {
                int quantity = com.GetNumOfEmployeesByDate(DateTime.Now);
                totalQuantity += quantity;
                result.QuantityChart.Add(new QuantityChartModel(com.CompanyName, quantity));                   
            }
            result.TotalQuantity = totalQuantity;
            return result;
        }
        #endregion  
    }
}
public enum HeaderViewRenderMode { Full, Title }