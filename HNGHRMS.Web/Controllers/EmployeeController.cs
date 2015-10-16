using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNGHRMS.Web;
using HNGHRMS.Model.Models;
using HNGHRMS.Service;
using HNGHRMS.Web.ViewModels;
using AutoMapper;
using DevExpress.Web.Mvc;
using HNGHRMS.Model.Enums;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Web.Core.Extensions;
using HNGHRMS.Web.Core.Helper;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using DevExpress.Web;
namespace HNGHRMS.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ICompanyService companyService;
        private readonly ITerminateService terminateService;
        public EmployeeController(IEmployeeService employeeService,ICompanyService companyService,ITerminateService terminateService)
        {
            this.employeeService = employeeService;
            this.companyService = companyService;
            this.terminateService = terminateService;
        }
        //
        // GET: /Employee/
        public ActionResult Index()
        {

            return View();
        }

        [ValidateInput(false)]
        public ActionResult EmployeesGridViewPartial()
        {
            var employees = employeeService.GetEmployees();
            
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            var companies = companyService.GetCompanies();
            ViewBag.Companies = companies.ToSelectListItems();                
            return PartialView("_EmployeesGridViewPartial", employeesView);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EmployeesGridViewPartialAddNew(EmployeeFormModel item)
        {
            
            var companies = companyService.GetCompanies();
            ViewBag.Companies = companies.ToSelectListItems();
            if (ModelState.IsValid)
            {
                try
                {
                    Employee employee = Mapper.Map<EmployeeFormModel, Employee>(item);
                    employeeService.CreateEmployee(employee);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var employees = employeeService.GetEmployees();
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return PartialView("_EmployeesGridViewPartial", employeesView);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EmployeesGridViewPartialUpdate(EmployeeFormModel item)
        {
 
            if (ModelState.IsValid)
            {
                try
                {
                    if (item.Status == EmployeeStatus.Terminated)
                    {
                        ViewData["EditError"] = "Không chỉnh sửa được thông tin nhân viên thôi việc";
                    }
                    else
                    {
                        Employee employeeToEdit = Mapper.Map<EmployeeFormModel, Employee>(item);
                        employeeService.EditEmployee(employeeToEdit);
                    }                
                  
                   
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var employees = employeeService.GetEmployees();
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            var companies = companyService.GetCompanies();
            ViewBag.Companies = companies.ToSelectListItems();
            return PartialView("_EmployeesGridViewPartial", employeesView);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EmployeesGridViewPartialDelete(System.Int32 EmployeeId)
        {
            if (EmployeeId >= 0)
            {
                try
                {
                    var employee = employeeService.GetEmployee(EmployeeId);
                    if(employee == null)
                    {
                        return HttpNotFound();
                    }
                    employeeService.DeleteEmployee(employee);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var employees = employeeService.GetEmployees();
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            var companies = companyService.GetCompanies();
            ViewBag.Companies = companies.ToSelectListItems();
            return PartialView("_EmployeesGridViewPartial", employeesView);
        }

        #region terminated Employee
        public ActionResult Terminate()
        {
            var companies = companyService.GetCompanies();
            List<int> companyIdList = new List<int>();
            foreach (Company item in companies)
            {
                companyIdList.Add(item.CompanyId);
            }
            var employees = employeeService.GetCurrentEmployeesBySpecifyCompany(companyIdList,null);
            var employee = employees.FirstOrDefault();
            var employeesTerminated = terminateService.GetEmployeesTerminated();
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            var employeesTerminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<EmployeeTerminateViewModel>>(employeesTerminated);
                  
            EmployeeTerminatedFormModel terminateFormModel;
            if(employee != null)
            {
                terminateFormModel = new EmployeeTerminatedFormModel(employee);
            }
            else
            {
                terminateFormModel = new EmployeeTerminatedFormModel();
            }
            ViewData["companies"] = companies.ToSelectListItems();
            ViewData["Employees"] = employeesView;
            ViewData["TerminateFormModel"] = terminateFormModel;
            ViewData["EmployeesTerminated"] = employeesTerminatedView;
            return View();
        }

        public ActionResult EmployeeList()
        {
            var companies = companyService.GetCompanies();
            List<int> companyIdList = new List<int>();
            foreach (Company item in companies)
            {
                companyIdList.Add(item.CompanyId);
            }
            var employees = employeeService.GetCurrentEmployeesBySpecifyCompany(companyIdList, null);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);           
            return PartialView("_CmbEmployeeList", employeesView);
        }
        public ActionResult EmployeeInformationById(int id)
        {
            var employee = employeeService.GetEmployee(id);
            EmployeeTerminatedFormModel employeeTerminatedFormModel = new EmployeeTerminatedFormModel(employee);
            return PartialView("_CbpEmployeeInfor", employeeTerminatedFormModel);
        }

        public ActionResult EmployeesTerminateGridViewPartial()
        {
            var employeesTerminated = terminateService.GetEmployeesTerminated();
            var employeesTerminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<EmployeeTerminateViewModel>>(employeesTerminated);
            return PartialView("_EmployeesTerminateGridViewPartial", employeesTerminatedView);
        }
        [HttpPost]
        public ActionResult EmployeeTerminatedAdd(EmployeeTerminatedFormModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Termination terminate = Mapper.Map<EmployeeTerminatedFormModel, Termination>(item);
                    terminateService.CreateEmployeeTerminated(terminate);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    return Json(new { status = "Errors", messeage = e.Message});
                }
                return Json(new { status = "OK", messeage = "xxx" });
            }
            else
            {
                return Json(new { status = "NotValid", messeage = "yyy" });
            }
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult EmployeesTerminateGridViewPartialDelete(int EmployeeId)
        {
            if (EmployeeId >= 0)
            {
                try
                {
                    var employee = terminateService.GetEmployeeTerminated(EmployeeId);
                    if (employee == null)
                    {
                        return HttpNotFound();
                    }
                    terminateService.DeleteEmployeeTerminated(employee);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var companies = companyService.GetCompanies();
            List<int> companyIdList = new List<int>();
            foreach (Company item in companies)
            {
                companyIdList.Add(item.CompanyId);
            }
            var employeesTerminated = terminateService.GetEmployeesTerminated();
            var employeesTerminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<EmployeeTerminateViewModel>>(employeesTerminated);
            ViewBag.Companies = companies.ToSelectListItems();
            return PartialView("_EmployeesTerminateGridViewPartial", employeesTerminatedView);
        }
        #endregion
    }

}