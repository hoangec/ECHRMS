using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

using HNGHRMS.Model.Models;
using HNGHRMS.Service;
using HNGHRMS.Web.ViewModels;
using AutoMapper;
using DevExpress.Web.Mvc;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Web.Extensions;
using HNGHRMS.Service.Interface;
using DevExpress.XtraPrinting;
using HNGHRMS.Web.Helpper;
using System.Text.RegularExpressions;
namespace HNGHRMS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IEmployeeService employeeService;
        private readonly ITerminateService terminateService;
        private readonly IReportService reportService;
        private readonly IExperienceService experienceService;
        private readonly IPositionService positionService;
        private UserManager<ApplicationUser> userManager;
        public HomeController(UserManager<ApplicationUser> UserManager,ICompanyService CompanyService,IEmployeeService EmployeeService,IReportService ReportService,IExperienceService experienceService, ITerminateService terminateService, IPositionService positionService)
        {
            this.companyService = CompanyService;
            this.employeeService = EmployeeService;
            this.terminateService = terminateService;
            this.reportService = ReportService;
            this.experienceService = experienceService;
            this.positionService = positionService;
            this.userManager = UserManager;    
        }
        public ActionResult Index()
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = DateTime.Now;
            var companies = companyService.GetCompanies();
            
            CompanySalaryReportChartModel companySalaryReport = GetSalayReportChart();
            EmployeeQuantityReportChartModel companyEmployeesQuantityReport = GetEmployeesQuantityReportChart();

            IList<EmployeeTypeQuantityChartModel> employeeQtyByDate = GetEmployeeTypeQuantityByDate(startDate,endDate);
            IList<EmployeeTypeQuantityChartModel> employeeQtyByCompany = GetEmployeeTypeQuantityByCompany(companies.FirstOrDefault());

            

            ViewData["EmployeeTotalSalaryChart"] = companySalaryReport.SalaryChart;
            ViewData["TotalSalary"] = companySalaryReport.TotalSalary;
            ViewData["EmployeeTotalInsuranceChart"] = companySalaryReport.InsuranceChart;
            ViewData["TotalInsurance"] = companySalaryReport.TotalInsurance;
            ViewData["EmployeeTotalQuantityChart"] = companyEmployeesQuantityReport.QuantityChart;
            ViewData["TotalEmployeeQuantity"] = companyEmployeesQuantityReport.TotalQuantity;
            ViewData["EmployeeByDateChart"] = employeeQtyByDate;
            ViewData["EmployeeByCompanyChart"] = employeeQtyByCompany;
            ViewData["Companies"] =  companies ;
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            return View();
        }

        #region General Report Table
        [ValidateInput(false)]
        public ActionResult CompanyEmployeeQuantityReportByDate(DateTime? selectedDateStart, DateTime? selectedDateEnd)
        {
            DateTime startDate;
            DateTime endDate;
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            if (!selectedDateStart.HasValue || !selectedDateEnd.HasValue)
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = DateTime.Now;
            }
            else
            {
                startDate = selectedDateStart.Value;
                endDate = selectedDateEnd.Value;
            }
            var companies = companyService.GetCompaniesByUser(user);
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyEmployeeQuantityReportViewModel>>(companies);
            for(int i = 0; i < companies.Count();i++)
            {
                int companyId = companies.ElementAt(i).Id;
                companiesView.ElementAt(i).CurrentEmployees = reportService.GetTotalEmployeeQuantityAtCurrentByCompany(companyId); ;
                companiesView.ElementAt(i).NewEmployees = reportService.GetNewEmployeeQuantityByDate(companyId, startDate, endDate);
                companiesView.ElementAt(i).NumArriveTransferEmployee = reportService.GetTransferArriveEmployeeQuantityByDate(companyId, startDate, endDate);
                companiesView.ElementAt(i).NumLeaveTransferEmployee = reportService.GetTransferLeaveEmployeeQuantityByDate(companyId, startDate, endDate);
                companiesView.ElementAt(i).TerminatedEmployees = reportService.GetTerminatedEmployeeQuantityByDate(companyId, startDate, endDate);
                companiesView.ElementAt(i).DateReportStart = startDate.ToString("dd/MM/yyyy");
                companiesView.ElementAt(i).DateReportEnd = endDate.ToString("dd/MM/yyyy"); 
                
            }
            return PartialView("GeneralReport/_CompanyEmployeeQuantityReportGridView", companiesView);               

        }

        public ActionResult CompanyEmployeeCostReportByDate()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
           // var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyCostReport>>(companies);
            List<CompanyCostReport> companiesCostReport = new List<CompanyCostReport>();
            foreach (Company com in companies)
            {
                int companyId = com.Id;
                CompanyCostReport companyCostReport = new CompanyCostReport()
                {
                    CompanyName = com.CompanyName,
                    Id = companyId,
                    CurrentSalary = reportService.GetTotalSalaryByCompanyAtCurrent(companyId)
                };
                for (int i = 1; i <= 12; i++)
                {
                    System.Reflection.PropertyInfo realSalaryPro = companyCostReport.GetType().GetProperty("RealSalary" + i.ToString());
                    realSalaryPro.SetValue(companyCostReport, (DateTime.Now.Month >= i) ? reportService.GetTotalRealSalaryByCompany(companyId, new DateTime(DateTime.Now.Year, i, 28)) : 0);

                    System.Reflection.PropertyInfo salaryPro = companyCostReport.GetType().GetProperty("Salary" + i.ToString());
                    salaryPro.SetValue(companyCostReport, (DateTime.Now.Month >= i) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, i, 28)) : 0);
                    
                    System.Reflection.PropertyInfo insurancePro = companyCostReport.GetType().GetProperty("Insurance" + i.ToString());
                    insurancePro.SetValue(companyCostReport, (DateTime.Now.Month >= i) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, i, 28)) : 0);

                    System.Reflection.PropertyInfo otherCostPro = companyCostReport.GetType().GetProperty("OtherCost" + i.ToString());
                    otherCostPro.SetValue(companyCostReport, (DateTime.Now.Month >= i) ? reportService.GetTotalExtraCostByCompany(companyId, new DateTime(DateTime.Now.Year, i, 28)) : 0);
                }
                companiesCostReport.Add(companyCostReport);
            }
            return PartialView("GeneralReport/_CompanyEmployeeRealSalaryReportGridView", companiesCostReport);
        }
        public ActionResult CompanyEmployeeSalaryReportByDate()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanySalaryReportViewModel>>(companies);
            for (int i = 0; i < companies.Count(); i++)
            {
                int companyId = companies.ElementAt(i).Id;
                companiesView.ElementAt(i).CurrentSalary = reportService.GetTotalSalaryByCompanyAtCurrent(companies.ElementAt(i).Id);
                companiesView.ElementAt(i).OneMSalary = (DateTime.Now.Month >=  1) ?  reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 1, 30)) : 0; 
                companiesView.ElementAt(i).TwoMSalary = (DateTime.Now.Month >=  2) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 2, 27)) : 0;
                companiesView.ElementAt(i).ThreeMSalary = (DateTime.Now.Month >=  3) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 3, 30)) : 0;
                companiesView.ElementAt(i).FourMSalary = (DateTime.Now.Month >=  4) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 4, 30)) : 0;
                companiesView.ElementAt(i).FiveMSalary = (DateTime.Now.Month >=  5) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 5, 31)) :0;
                companiesView.ElementAt(i).SixMSalary = (DateTime.Now.Month >=  6) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 6, 30)) : 0;
                companiesView.ElementAt(i).SavenMSalary = (DateTime.Now.Month >=  7) ?  reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 7, 30)) : 0;
                companiesView.ElementAt(i).EightMSalary = (DateTime.Now.Month >=  8) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 8, 30)) : 0;
                companiesView.ElementAt(i).NineMSalary = (DateTime.Now.Month >=  9) ?  reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 9, 30)): 0;
                companiesView.ElementAt(i).TenMSalary = (DateTime.Now.Month >=  10) ? reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 10, 30)):0;
                companiesView.ElementAt(i).ElevenMSalary = (DateTime.Now.Month >=  11) ?  reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 11, 30)):0;
                companiesView.ElementAt(i).TwelveMSalary = (DateTime.Now.Month >=  12) ?  reportService.GetTotalSalaryByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 12, 30)) : 0;                
            }
            return PartialView("GeneralReport/_CompanyEmployeeSalaryReportGridView", companiesView);

        }

        public ActionResult CompanyEmployeeInsuranceReportByDate()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyInsuranceReportViewModel>>(companies);
            for (int i = 0; i < companies.Count(); i++)
            {
                int companyId = companies.ElementAt(i).Id;
                companiesView.ElementAt(i).CurrentInsurance = reportService.GetTotalInsuranceByCompanyAtCurrent(companies.ElementAt(i).Id);
                companiesView.ElementAt(i).OneMSalary = (DateTime.Now.Month >= 1) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 1, 30)) : 0;
                companiesView.ElementAt(i).TwoMSalary = (DateTime.Now.Month >= 2) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 2, 27)) : 0;
                companiesView.ElementAt(i).ThreeMSalary = (DateTime.Now.Month >= 3) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 3, 30)) : 0;
                companiesView.ElementAt(i).FourMSalary = (DateTime.Now.Month >= 4) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 4, 30)) : 0;
                companiesView.ElementAt(i).FiveMSalary = (DateTime.Now.Month >= 5) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 5, 31)) : 0;
                companiesView.ElementAt(i).SixMSalary = (DateTime.Now.Month >= 6) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 6, 30)) : 0;
                companiesView.ElementAt(i).SavenMSalary = (DateTime.Now.Month >= 7) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 7, 30)) : 0;
                companiesView.ElementAt(i).EightMSalary = (DateTime.Now.Month >= 8) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 8, 30)) : 0;
                companiesView.ElementAt(i).NineMSalary = (DateTime.Now.Month >= 9) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 9, 30)) : 0;
                companiesView.ElementAt(i).TenMSalary = (DateTime.Now.Month >= 10) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 10, 30)) : 0;
                companiesView.ElementAt(i).ElevenMSalary = (DateTime.Now.Month >= 11) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 11, 30)) : 0;
                companiesView.ElementAt(i).TwelveMSalary = (DateTime.Now.Month >= 12) ? reportService.GetTotalInsuranceByCompanyByDate(companyId, new DateTime(DateTime.Now.Year, 12, 30)) : 0;
            }
            return PartialView("GeneralReport/_CompanyEmployeeInsuranceReportGridView", companiesView);

        }
                 
        #endregion

        #region Detail Report Table
        public ActionResult NewEmployeesReport(string DataQuery)
        {

            DateTime startDate;
            DateTime endDate;
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            int companyId = companies.Count() > 0 ? companies.FirstOrDefault().Id : 0;
            if (!string.IsNullOrEmpty(DataQuery) && DataQuery.Length == 23)
            {

                string[] M = DataQuery.Split('-');
                int.TryParse(M[0], out companyId);
                startDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDateStartEndDateFormart(true);
                endDate = string.Format("{0}/{1}/{2}", M[4], M[5], M[6]).ConvertToDateStartEndDateFormart(false);
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = DateTime.Now;
            }

            var employees = employeeService.GetNewEmployeesCompanyByDate(companyId, startDate, endDate);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<NewEmployeeReportGridViewModel>>(employees);
            var positions = positionService.GetPositions();

            ViewBag.CompanyId = companyId;
            ViewBag.StartChoiceDate = startDate;
            ViewBag.EndChoiceDate = endDate;
            ViewData["positions"] = positions.ToSelectListItems();
            ViewData["SelectedCompany"] = companyId;
            ViewData["companies"] = companies.ToSelectListItems(companyId);
            ViewData["employess"] = employeesView;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult NewEmployeeReportGridView()
        {
            int CompanyId = int.Parse(Request.Params["CompanyId"]);
            DateTime StartDateSelected = DateTime.Parse(Request.Params["StartSelectedDate"]);
            DateTime EndDateSelected = DateTime.Parse(Request.Params["EndSelectedDate"]);
            var employees = employeeService.GetNewEmployeesCompanyByDate(CompanyId, StartDateSelected, EndDateSelected);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<NewEmployeeReportGridViewModel>>(employees);
            var positions = positionService.GetPositions();
            ViewData["positions"] = positions.ToSelectListItems();
            return PartialView("NewEmployeeReport/_NewEmployeeReportGridView", employeesView);
        }
        public ActionResult NewEmployeeReportExportTo(string QueryString)
        {
            DateTime startDate;
            DateTime endDate;
            int companyId = 0;
            if (!String.IsNullOrEmpty(QueryString))
            {
                string[] M = QueryString.Split('-');
                companyId = int.Parse(M[0]);
                startDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDateWithFormat("d/M/yyyy");
                endDate = string.Format("{0}/{1}/{2}", M[4], M[5], M[6]).ConvertToDateWithFormat("d/M/yyyy");
            }else
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = DateTime.Now;
            }
            var company = companyService.GetCompany(companyId);
            string companyName = "";
            if (company != null)
            {
                companyName = company.CompanyName;
                var employees = employeeService.GetNewEmployeesCompanyByDate(companyId, startDate, endDate);
                var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<NewEmployeeReportGridViewModel>>(employees);
                return GridViewExtension.ExportToXlsx(GridviewExportHelper.NewEmployeeGridViewExcelExportSettings(companyName), employeesView);    
            }
            else {
                return RedirectToAction("NewEmployeesReport", "Home", new { DataQuery = "" });
            }
                  
        }        
      
        public ActionResult TerminatedEmployeesReport(string DataQuery)
        {
            DateTime startDate;
            DateTime endDate;
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            int companyId = companies.Count() > 0 ? companies.FirstOrDefault().Id : 0;
            if (!string.IsNullOrEmpty(DataQuery) && DataQuery.Length == 23)
            {

                string[] M = DataQuery.Split('-');
                int.TryParse(M[0], out companyId);
                startDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDateStartEndDateFormart(true);
                endDate = string.Format("{0}/{1}/{2}", M[4], M[5], M[6]).ConvertToDateStartEndDateFormart(false);
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = DateTime.Now;
            }
            ViewBag.CompanyId = companyId;
            ViewBag.StartChoiceDate = startDate;
            ViewBag.EndChoiceDate = endDate;

            var positions = positionService.GetPositions();

            var terminated = terminateService.GetEmployeesTerminatedCompanyByDate(companyId, startDate, endDate);
            var terminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<TerminateReportGridViewModel>>(terminated);

            ViewData["positions"] = positions.ToSelectListItems();
            ViewData["SelectedCompany"] = companyId;
            ViewData["companies"] = companies.ToSelectListItems(companyId);
            ViewData["terminated"] = terminatedView;
            return View();
        }
        public ActionResult TerminatedEmployeeGridView()
        {
            int CompanyId = int.Parse(Request.Params["CompanyId"]);
            DateTime StartDateSelected = DateTime.Parse(Request.Params["StartSelectedDate"]);
            DateTime EndDateSelected = DateTime.Parse(Request.Params["EndSelectedDate"]);
            var terminated = terminateService.GetEmployeesTerminatedCompanyByDate(CompanyId, StartDateSelected, EndDateSelected);
            var terminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<TerminateReportGridViewModel>>(terminated);
            var positions = positionService.GetPositions();
            ViewData["positions"] = positions.ToSelectListItems();
            return PartialView("TerminatedEmployeeReport/_TerminatedEmployeeGridView", terminatedView);
        }
        public ActionResult TerminatedEmployeeReportExportTo(string QueryString)
        {
            DateTime startDate;
            DateTime endDate;
            int companyId = 0;
            if (!String.IsNullOrEmpty(QueryString))
            {
                string[] M = QueryString.Split('-');
                companyId = int.Parse(M[0]);
                startDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDateWithFormat("d/M/yyyy");
                endDate = string.Format("{0}/{1}/{2}", M[4], M[5], M[6]).ConvertToDateWithFormat("d/M/yyyy");
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = DateTime.Now;
            }
            var company = companyService.GetCompany(companyId);
            string companyName = "";
            if (company != null)
            {
                companyName = company.CompanyName;
                var terminated = terminateService.GetEmployeesTerminatedCompanyByDate(companyId, startDate, endDate);
                var terminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<EmployeeTerminateViewModel>>(terminated);
                return GridViewExtension.ExportToXlsx(GridviewExportHelper.TerminatedEmployeeGridViewExcelExportSettings(companyName), terminatedView);
            }
            else
            {
                return RedirectToAction("TerminatedEmployeesReport", "Home", new { DataQuery = "" });
            }

        }
        
        public ActionResult TransferEmployeesReport()
        {

            DateTime startDate;
            DateTime endDate;
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            var positions = positionService.GetPositions();            
            int companyId = companies.Count() > 0 ? companies.FirstOrDefault().Id : 0;
            startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate = DateTime.Now;
            var experinces = experienceService.GetEmployeesTransferCompanyByDate(companyId, startDate, endDate);
            var experienceViews = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferReportGridViewModel>>(experinces);

            ViewBag.CompanyId = companyId;
            ViewBag.StartChoiceDate = startDate;
            ViewBag.EndChoiceDate = endDate;

            ViewData["SelectedCompany"] = companyId;
            ViewData["companies"] = companies.ToSelectListItems(companyId);
            ViewData["positionsList"] = positions.ToSelectListItems();
            ViewData["experiences"] = experienceViews;
            return View();
        }
        public ActionResult TransferEmployeeGridview()
        {
            int CompanyId = int.Parse(Request.Params["CompanyId"]);
            DateTime StartDateSelected = DateTime.Parse(Request.Params["StartSelectedDate"]);
            DateTime EndDateSelected = DateTime.Parse(Request.Params["EndSelectedDate"]);
            var experinces = experienceService.GetEmployeesTransferCompanyByDate(CompanyId,StartDateSelected,EndDateSelected);
            var experienceViews = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferReportGridViewModel>>(experinces);
            var positions = positionService.GetPositions();
            ViewData["positionsList"] = positions.ToSelectListItems();
            return PartialView("TransferEmployeeReport/_TransferEmployeeReportGridView", experienceViews);
        }
        public ActionResult TransferEmployeeReportExportTo(string QueryString)
        {
            DateTime startDate;
            DateTime endDate;
            int companyId = 0;
            if (!String.IsNullOrEmpty(QueryString))
            {
                string[] M = QueryString.Split('-');
                companyId = int.Parse(M[0]);
                startDate = string.Format("{0}/{1}/{2}", M[1], M[2], M[3]).ConvertToDateWithFormat("d/M/yyyy");
                endDate = string.Format("{0}/{1}/{2}", M[4], M[5], M[6]).ConvertToDateWithFormat("d/M/yyyy");
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = DateTime.Now;
            }
            var company = companyService.GetCompany(companyId);
            string companyName = "";
            if (company != null)
            {
                companyName = company.CompanyName;
                var experinces = experienceService.GetEmployeesTransferCompanyByDate(companyId, startDate, endDate);
                var experienceViews = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferReportGridViewModel>>(experinces);
                return GridViewExtension.ExportToXlsx(GridviewExportHelper.TransferEmployeeGridViewExcelExportSettings(companyName), experienceViews);
            }
            else
            {
                return RedirectToAction("TransferEmployeesReport", "Home");
            }
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
            CompanySalaryReportChartModel companySalaryReport = GetSalayReportChart();

            return PartialView("Charts/_CompanyTotalSalaryChart", companySalaryReport.SalaryChart);
        }
        public ActionResult GetCompanyTotalInsuranceChart(string ViewOptions = "V")
        {

            if (ViewOptions == "P")
            {
                ViewData["Options"] = "P";
            }
            else
            {
                ViewData["Options"] = "V";
            }
            CompanySalaryReportChartModel companySalaryReport = GetSalayReportChart();

            return PartialView("Charts/_CompanyTotalInsuranceChart", companySalaryReport.InsuranceChart);
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
            EmployeeQuantityReportChartModel companyTotalEmployeeQuantityReport = GetEmployeesQuantityReportChart();

            return PartialView("Charts/_CompanyTotalEmployeeQuantityReport", companyTotalEmployeeQuantityReport.QuantityChart);
        }
        public ActionResult GetEmployeesQuantityTypeByDateChart(DateTime StartDate,DateTime EndDate)
        {
           // DateTime dateSelected = Date.ConvertToDate();
            IList<EmployeeTypeQuantityChartModel> employeesQuantityTypeByDate = GetEmployeeTypeQuantityByDate(StartDate,EndDate);
            return PartialView("Charts/_EmployeesQuantityTypeByDateChart", employeesQuantityTypeByDate);
        }
        public ActionResult GetEmployeesQuantityTypeByCompanyChart(int CompanyId)
        {
            Company company = companyService.GetCompany(CompanyId);
            IList<EmployeeTypeQuantityChartModel> employeeQtyByCompany = GetEmployeeTypeQuantityByCompany(company);
            return PartialView("Charts/_EmployeesQuantityTypeByCompanyChart", employeeQtyByCompany);
        }
        public IList<EmployeeTypeQuantityChartModel> GetEmployeeTypeQuantityByDate(DateTime StartDate,DateTime EndDate)
        {
            List<EmployeeTypeQuantityChartModel> result = new List<EmployeeTypeQuantityChartModel>();
            string[] types = new string[] { "Tuyển mới", "Thôi việc" };
            var companies = companyService.GetCompanies();
            Dictionary<string, IList<int>> values = new Dictionary<string, IList<int>>();
            values.Add(types[0],reportService.GetNewEmployeeQuantityTypeByDate(companies,StartDate,EndDate));
            values.Add(types[1], reportService.GetTerminatedEmployeeQuantityTypeByDate(companies,StartDate,EndDate));
            
            foreach (string type in types)
                for (int i = 0; i < companies.Count(); i++)
                    result.Add(new EmployeeTypeQuantityChartModel(companies.ElementAt(i).CompanyName, type, values[type][i]));
            return result;
        }
        public IList<EmployeeTypeQuantityChartModel> GetEmployeeTypeQuantityByCompany(Company Company)
        {
            List<EmployeeTypeQuantityChartModel> result = new List<EmployeeTypeQuantityChartModel>();
            if(Company != null)
            {
                string[] types = new string[] { "Tuyển mới", "Thôi việc" };
                string[] months = new string[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
                Dictionary<string, IList<int>> values = new Dictionary<string, IList<int>>();
                values.Add(types[0], reportService.GetNewEmployeeQuantityTypeByCompany(Company));
                values.Add(types[1], reportService.GetTerminatedEmployeeQuantityByCompany(Company));

                foreach (string type in types)
                    for (int i = 0; i < months.Length; i++)
                        result.Add(new EmployeeTypeQuantityChartModel(months[i], type, values[type][i]));
            }            
            return result;
        }
        public CompanySalaryReportChartModel GetSalayReportChart()
        {
            var companies = companyService.GetCompanies();
            CompanySalaryReportChartModel result = new CompanySalaryReportChartModel();
            double totalSalary = 0;
            double totalInsurance = 0;
            foreach(Company com in companies)
            {
                double salary = reportService.GetTotalSalaryByCompanyAtCurrent(com.Id);
                double insurance = reportService.GetTotalInsuranceByCompanyAtCurrent(com.Id);
                totalSalary += salary;
                totalInsurance +=  insurance;
                result.SalaryChart.Add(new QuantityChartModel(com.CompanyName, salary));
                result.InsuranceChart.Add(new QuantityChartModel(com.CompanyName, insurance));
            }
            result.TotalSalary = totalSalary;
            result.TotalInsurance = totalInsurance;
            return result;
        }

        public EmployeeQuantityReportChartModel GetEmployeesQuantityReportChart()
        {
            var companies = companyService.GetCompanies();
            EmployeeQuantityReportChartModel result = new EmployeeQuantityReportChartModel();
            int totalQuantity = 0;
            foreach (Company com in companies)
            {
                int quantity = reportService.GetTotalEmployeeQuantityAtCurrentByCompany(com.Id);
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