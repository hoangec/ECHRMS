using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using DevExpress.Web;
using HNGHRMS.Web;
using HNGHRMS.Model.Models;
using HNGHRMS.Service;
using HNGHRMS.Web.ViewModels;
using HNGHRMS.Model.Enums;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Web.Extensions;
using HNGHRMS.Web.Helpper;
using HNGHRMS.Service.Messaging;
using HNGHRMS.Service.Interface;
//using HNGHRMS.Service.ViewModels;
using AutoMapper;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using System.Text.RegularExpressions;
namespace HNGHRMS.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ICompanyService companyService;
        private readonly ITerminateService terminateService;
        private readonly IPositionService positionService;
        private readonly IContractService contractService;        
        private readonly IInsuranceService insuranceService;
        private readonly ISalaryService salaryService;
        private readonly IExperienceService experienceService;
        private UserManager<ApplicationUser> userManager;


        public EmployeeController(UserManager<ApplicationUser> UserManager, IExperienceService experienceService, ISalaryService salaryService, IEmployeeService employeeService, ICompanyService companyService, ITerminateService terminateService, IPositionService positionService, IContractService contractService, IInsuranceService insuranceService)
        {
            this.employeeService = employeeService;
            this.companyService = companyService;
            this.terminateService = terminateService;
            this.positionService = positionService;
            this.contractService = contractService;            
            this.insuranceService = insuranceService;
            this.salaryService = salaryService;
            this.experienceService = experienceService;
            this.userManager = UserManager;                 
        }
        //
        // GET: /Employee/

        #region Index
        public ActionResult Index(int? id)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            var positions = positionService.GetPositions();
            ViewBag.Companies = companies;
            ViewBag.Positions = positions;
            ViewBag.FirstComapanyId = (companies.Count() != 0) ?  companies.FirstOrDefault().Id : 0;
            ViewBag.FirstPositionId = (positions.Count() != 0 ) ?  positions.FirstOrDefault().Id : 0;

            if (id.HasValue && id != 0)
            {
                var employee = employeeService.GetEmployee(id.Value);
                if (employee != null)
                {
                    EmployeeInfoModel employeeInfor = new EmployeeInfoModel()
                    {
                        Id = employee.Id,
                        LastName = employee.LastName,
                        FirstName = employee.FirstName,
                        EmployeeCode = employee.EmployeeCode,
                        IdentityNo = employee.Identity.IdentityNo,
                        IdentityDateOfIssue = employee.Identity.DateOfIssue,
                        Gender = employee.Gender,
                        MaritalStatus = employee.MaritalStatus,
                        Nationality = employee.Nationality,
                        Salary = employee.Salary,
                        Photo = employee.Photo,
                        BirthDay = (DateTime)employee.BirthDay,
                        RealSalary = employeeService.GetEmployeeRealSalaryAtDate(employee,DateTime.Now)
                    };

                    ViewData["employeeInfor"] = employeeInfor;
                    //
                    EmployeeDetailReport empDetailReport = new EmployeeDetailReport(employee);
                    ViewData["report"] = empDetailReport;
                    Session["Empreport"] = empDetailReport;
                    EmployeeFunctionTabViewModel employeeFunctionTabViewModel = CreateEmployeeFunctionViewModelFromEmployee(employee);
                    employeeFunctionTabViewModel.report1 = empDetailReport;
                    return View("EmployeeDetail",employeeFunctionTabViewModel);
                }
            }
            return View();
        }

        [ValidateInput(false)]
        public ActionResult EmployeesListGridViewPartial()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var employees = employeeService.GetEmployeesByUser(user).OrderByDescending(emp => emp.Id);            
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            var companies = companyService.GetCompaniesByUser(user);
            var positions = positionService.GetPositions();
            ViewBag.Companies = companies.ToSelectListItems();
            ViewBag.Positions = positions.ToSelectListItems();  
            return PartialView("EmployeeList/_EmployeesListGridViewPartial", employeesView);
        }

        public ActionResult EmployeesGridViewPartialDelete(System.Int32 Id)
        {
            if (Id >= 0)
            {
                try
                {
                    var employee = employeeService.GetEmployee(Id);
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
            var positions = positionService.GetPositions();
            ViewBag.Companies = companies.ToSelectListItems();
            ViewBag.Positions = positions.ToSelectListItems();  
            return PartialView("EmployeeList/_EmployeesListGridViewPartial", employeesView);
        }

        [HttpPost]
        public ActionResult AjaxEmployeeSimpleAdd(EmployeeSimpleFormModel employeeSimpleAdd)
        {
            string checkModel = employeeSimpleAdd.CheckIfViewModelIsValid();
            if (ModelState.IsValid && checkModel == "")
            {
                
                if (employeeService.GetEmployeeByEmployeeCode(employeeSimpleAdd.EmployeeCode) == null)
                {
                    var employee = new Employee()
                    {
                        LastName = employeeSimpleAdd.LastName,
                        FirstName = employeeSimpleAdd.FirstName,
                        Gender = employeeSimpleAdd.Gender,
                        BirthDay = employeeSimpleAdd.BirthDay,
                        Identity = new Identity() { IdentityNo = employeeSimpleAdd.IdentityNo, DateOfIssue = employeeSimpleAdd.IdentityDateOfIssue },
                        CompanyId = employeeSimpleAdd.CompanyId,
                        PositionId = employeeSimpleAdd.PositionId,
                        JoinedDate = employeeSimpleAdd.JoinedDate,
                        Salary = employeeSimpleAdd.Salary,
                        MaritalStatus = employeeSimpleAdd.MaritalStatus,
                        Departement = employeeSimpleAdd.Departement,
                        Nationality = employeeSimpleAdd.Nationality,
                        EmployeeCode = employeeSimpleAdd.EmployeeCode,
                    };
                    CreateEmployeeResponse response = employeeService.CreateEmployee(employee);
                    if (response.Status)
                    {
                        return Json(new { status = "Success", message = response.Message });
                    }
                    else
                    {
                        return Json(new { status = "Fail", message = response.Message });
                    }
                }
                else {
                    return Json(new { status = "Fail", message = "Mã nhân viên đã tồn tại" });
                }
            }
            else
            {

                if (checkModel != "")
                {
                    return Json(new { status = "Fail", message = checkModel });
                }
                else {
                    return Json(new { status = "Fail", message = "Kiểm tra dữ liệu nhập" });
                }
                
            }

        }

        public ActionResult AjaxGetEmployeeNumberCode(int CompanyId) 
        {
            string result = employeeService.GetEmployeeCodeByCompanyId(CompanyId);

            if (result == "OFR")
            {
                return Json(new { status = "FAIL", message = "OFR" });
            }
            else if (result != "")
            {
                return Json(new { status = "OK", message = result});
            }
            else
            {
                return Json(new { status = "FAIL", message = "NOTFOUND" });
            }

        }
        public JsonResult IsEmployeeCodeExists(string EmployeeCode)
        {
            
            var employee = employeeService.GetEmployeeByEmployeeCode(EmployeeCode);
            if (employee != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ExportDocumentViewer()
        {
            return DevExpress.Web.Mvc.DocumentViewerExtension.ExportTo( (EmployeeDetailReport)Session["Empreport"]);
        }
        public ActionResult DocumentViewerPartial()
        {
            ViewData["report"] = Session["Empreport"];
            return PartialView("EmployeeDetail/_EmployeeDetailReport");            
        }
        #endregion

        #region terminated Employee
        public ActionResult Terminate()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            var positions = positionService.GetPositions();
            List<int> companyIdList = new List<int>();
            foreach (Company item in companies)
            {
                companyIdList.Add(item.Id);
            }
            var employees = employeeService.GetCurrentEmployeesBySpecifyCompany(companyIdList,null);
            var employee = employees.FirstOrDefault();
            var employeesTerminated = terminateService.GetEmployeesTerminatedByUser(user);
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
            ViewData["Positions"] = positions.ToSelectListItems();
            ViewData["Employees"] = employeesView;
            ViewData["TerminateFormModel"] = terminateFormModel;
            ViewData["EmployeesTerminated"] = employeesTerminatedView;
            return View();
        }

        public ActionResult EmployeeList()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var companies = companyService.GetCompaniesByUser(user);
            List<int> companyIdList = new List<int>();
            foreach (Company item in companies)
            {
                companyIdList.Add(item.Id);
            }
            var employees = employeeService.GetCurrentEmployeesBySpecifyCompany(companyIdList, null);
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return PartialView("EmployeesTerminate/_CmbEmployeeTerminate", employeesView);
        }
        public ActionResult EmployeeInformationById(int id)
        {
             EmployeeTerminatedFormModel employeeTerminatedFormModel;
            if (id != 0)
            {
                var employee = employeeService.GetEmployee(id);
                employeeTerminatedFormModel = new EmployeeTerminatedFormModel(employee);
            }
            else
            {
                employeeTerminatedFormModel = new EmployeeTerminatedFormModel();
            }
            return PartialView("EmployeesTerminate/_CbpEmployeeTerminateInfor", employeeTerminatedFormModel);
        }

        public ActionResult EmployeesTerminateGridViewPartial()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var employeesTerminated = terminateService.GetEmployeesTerminatedByUser(user);
            var employeesTerminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<EmployeeTerminateViewModel>>(employeesTerminated);
            var companies = companyService.GetCompanies();
            var positions = positionService.GetPositions();
            ViewBag.Companies = companies.ToSelectListItems();
            ViewBag.Positions = positions.ToSelectListItems();
            return PartialView("EmployeesTerminate/_EmployeesTerminateGridViewPartial", employeesTerminatedView);
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
                companyIdList.Add(item.Id);
            }
            var employeesTerminated = terminateService.GetEmployeesTerminated();
            var employeesTerminatedView = Mapper.Map<IEnumerable<Termination>, IEnumerable<EmployeeTerminateViewModel>>(employeesTerminated);
            ViewBag.Companies = companies.ToSelectListItems();
            return PartialView("EmployeesTerminate/_EmployeesTerminateGridViewPartial", employeesTerminatedView);
        }
        #endregion

        #region TransferEmployee
        public ActionResult TransferEmployee()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var employees = employeeService.GetCurrentEmployeesWorkingByUser(user);
            var employee = employees.FirstOrDefault();
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            var companies = companyService.GetCompanies();
            var positions = positionService.GetPositions();
            EmployeeTransferFormModel empTranferFormModel;
            if (employee != null)
            {
                empTranferFormModel  = new EmployeeTransferFormModel(employee);
            }
            else {
                empTranferFormModel = new EmployeeTransferFormModel();
            }
            var experiences = experienceService.GetAllExperences();
            var experiencesViewModel = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferEmployeeGridViewModel>>(experiences);          

            empTranferFormModel.NewCompanyList = companies;
            empTranferFormModel.NewPositionList = positions;
            ViewData["TransferEmployee"] = empTranferFormModel;
            ViewData["Employees"] = employeesView;
            ViewData["EmployeesTransfer"] = experiencesViewModel;
            ViewData["companies"] = companies;
            return View();
        }

        public ActionResult GetEmployeeTransferList()
        {
            var employees = employeeService.GetCurrentEmployeesWorking();
            var employeesView = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return PartialView("EmployeesTransfer/_CmbEmployeeTransferList", employeesView);
        }
        public ActionResult EmployeeTransferById(int id) 
        {
            EmployeeTransferFormModel employeeTransferFormModel;
            var companies = companyService.GetCompanies();
            var positions = positionService.GetPositions();
            if (id != 0)
            {
                var employee = employeeService.GetEmployee(id);
                employeeTransferFormModel = new EmployeeTransferFormModel(employee);
            }
            else
            {
                employeeTransferFormModel = new EmployeeTransferFormModel();
            }
            employeeTransferFormModel.NewCompanyList = companies;
            employeeTransferFormModel.NewPositionList = positions;
            return PartialView("EmployeesTransfer/_CbpEmployeeTransfer", employeeTransferFormModel);
        }

        [HttpPost]
        public ActionResult EmployeeTransferAdd(EmployeeTransferFormModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Employee employee = employeeService.GetEmployee(item.EmployeeId);
                    CreateExperienceForEmployeeRequest request = new CreateExperienceForEmployeeRequest() { 
                        Employee = employee,
                        NewCompanyId = item.NewCompanyId,
                        NewPositionId = item.NewPositionId,
                        NewDepartement = item.NewDepartement,
                        NewSalary = item.NewSalary,
                        TransferDate = item.TransferDate,
                        Reason = item.Reason,
                        AttachFile = item.FileAttach,            
                        IsInsuranceTransfer = item.IsInsuranceTransfer,
                        InsuranceTransferAmount = item.InsuranceAmount,
                        InsuranceApplyDate = item.InsuranceApplyDate,
            
                    };
                    CreateExperienceForEmployeeResponse response = experienceService.CreateNewExperienceForEmployee(request);                    
                    if (response.Status == true)
                    {
                        return Json(new { status = "OK", message = response.Message });
                    }
                    else {
                        return Json(new { status = "Fail", message = response.Message });
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    return Json(new { status = "Errors", message = e.Message });
                }               
            }
            else
            {
                return Json(new { status = "NotValid", message = "yyy" });
            }
        }

        public ActionResult EmployeesTransferGridViewPartial() 
        {
            var experiences  = experienceService.GetAllExperences();
            var companies = companyService.GetCompanies();
            var positions = positionService.GetPositions();

            ViewData["companies"] = companies;
            ViewData["positions"] = positions;            
            var experiencesViewModel = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferEmployeeGridViewModel>>(experiences);
            return PartialView("EmployeesTransfer/_EmployeesTransferGridViewPartial", experiencesViewModel);
        }


        public ActionResult EmployeeTransferAddAttachFileUploadControlCallbackAction() 
        {
            UploadControlExtension.GetUploadedFiles("EmployeeTransferAddUploadFile", UploadFileHelper.PdfValidationSettings, UploadFileHelper.uc_ExperienceFileUploadComplete);
            return null;
        }
        #endregion

        #region EmployeeDetails
        //[ValidateInput(false)]
        //public ActionResult EmployeeDetail(int? id)
        //{
        //    if(id.HasValue && id != 0)
        //    {
        //        var employee = employeeService.GetEmployee(id.Value);
        //        if(employee != null)
        //        {                    
        //            EmployeeInfoModel employeeInfor = new EmployeeInfoModel() { 
        //                Id = employee.Id,
        //                LastName = employee.LastName,
        //                FirstName = employee.FirstName,
        //                EmployeeNo = employee.EmployeeNo,
        //                IdentityNo = employee.Identity.IdentityNo,
        //                IdentityDateOfIssue = employee.Identity.DateOfIssue,
        //                Gender = employee.Gender,
        //                MaritalStatus = employee.MaritalStatus,
        //                Nationality = employee.Nationality,
        //                Photo = employee.Photo,
        //                BirthDay = (DateTime)employee.BirthDay,
        //            };       
                
        //            ViewData["employeeInfor"] = employeeInfor;
        //            //

        //            EmployeeFunctionTabViewModel employeeFunctionTabViewModel = CreateEmployeeFunctionViewModelFromEmployee(employee);
        //            return View(employeeFunctionTabViewModel);
        //        }
        //    }
        //    return RedirectToAction("EmployeeList");
        //}
        private EmployeeFunctionTabViewModel CreateEmployeeFunctionViewModelFromEmployee(Employee employee)
        {
            EmployeeFunctionTabViewModel employeeFunctionTabViewModel = new EmployeeFunctionTabViewModel();
            if (employee.Status == EmployeeStatus.Terminated)
            {
                employeeFunctionTabViewModel.IsEnabled = false;
            }
            else
            {
                employeeFunctionTabViewModel.IsEnabled = true;
            }
            EmployeeContactTabViewModel employeeContactTabViewModel = Mapper.Map<Employee, EmployeeContactTabViewModel>(employee);
            EmployeeJobTabViewModel employeeJobTabViewModel = Mapper.Map<Employee, EmployeeJobTabViewModel>(employee);
            employeeJobTabViewModel.PositionsList = positionService.GetPositions();
            employeeJobTabViewModel.CompanyList = companyService.GetCompanies();

            EmployeeContractTabViewModel employeeContractTabViewModel = new EmployeeContractTabViewModel();
            employeeContractTabViewModel.ContractsViewModel = Mapper.Map<IEnumerable<Contract>, IEnumerable<EmployeeContractsViewModel>>(employee.Contracts);
            employeeContractTabViewModel.ContractTypeList = contractService.GetContractTypes();
            employeeContractTabViewModel.IsEnable = employee.Status == EmployeeStatus.Terminated ? false : true;

            GetInsuranceByEmployeeIdRequest request = new GetInsuranceByEmployeeIdRequest()
            {
                EmployeeId = employee.Id
            };
            GetInsuranceByEmployeeIdResponse response = insuranceService.GetInsuranceByEmployeeId(request);
            EmployeeInsuranceTabViewModel empInsuranceTabView = new EmployeeInsuranceTabViewModel()
            {
                
                InsuranceList = Mapper.Map<IEnumerable<Insurance>,IEnumerable<InsuranceGridView>>( response.InsuranceList),   
                HasMandatoryInsurance = response.HasMandatoryInsurance,
                HasVoluntaryInsurance = response.HasVoluntaryInsurance
            };
            
           
            var empSalaryComponents = salaryService.GetSalaryComponentByEmployeeId(employee.Id);
            var empSalaryComponentsView = Mapper.Map<IEnumerable<EmployeeSalaryComponents>, IEnumerable<EmployeeSalaryComponentViewModel>>(empSalaryComponents);
        
            EmployeeSalaryTabViewModel empSalaryTabView = new EmployeeSalaryTabViewModel(){
                EmployeeSalaryComponents = empSalaryComponentsView,
                HaveSalaryComponent = empSalaryComponents.Count() == 0 ? false : true,
                
            };

            // Experience            
            var experiences = experienceService.GetExperirenceByEmployeeId(employee.Id);
            EmployeeExperienceTabViewModel experienceTabView = new EmployeeExperienceTabViewModel();
            var experiencesViewModel = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferEmployeeGridViewModel>>(experiences);
            experienceTabView.ExperiencesList = experiencesViewModel;
            

            employeeFunctionTabViewModel.EmployeeId = employee.Id;
            employeeFunctionTabViewModel.EmployeeContactTabViewModel = employeeContactTabViewModel;
            employeeFunctionTabViewModel.EmployeeContractTabViewModel = employeeContractTabViewModel;
            employeeFunctionTabViewModel.EmployeeJobTabViewModel = employeeJobTabViewModel;
            employeeFunctionTabViewModel.EmployeeInsuranceTabViewModel = empInsuranceTabView;
            employeeFunctionTabViewModel.EmployeeSalaryTabViewModel = empSalaryTabView;
            employeeFunctionTabViewModel.EmployeeExperienceTabViewModel = experienceTabView;
            return employeeFunctionTabViewModel;
        }
        #region Employee Infor
        [HttpPost]
        public ActionResult EmployeeInforUpdate(EmployeeInfoModel empolyeeInforUpdate)
        {
            if (ModelState.IsValid)
            {
                var employee = employeeService.GetEmployee(empolyeeInforUpdate.Id);
                if (employee != null)
                {

                    Mapper.Map<EmployeeInfoModel, Employee>(empolyeeInforUpdate, employee);
                    employeeService.EditEmployee(employee);
                    return Json(new { status = "Success", messeage = "OK" });
                }
                return Json(new { status = "Fail", messeage = "Nhân viên không tồn tại" });  
            }
            else
            {
                return Json(new { status = "Fail", messeage = "Kiểm tra dữ liệu nhập" });  
            }
        }
        public ActionResult EmployeePhotoUpdate()
        {          

            return BinaryImageEditExtension.GetCallbackResult();
        }        
       
        #endregion

        #region Employee Contact
        public ActionResult EmployeeContactDetailCallbackPanel(int? id)
        {
            if (id.HasValue && id != 0)
            {
                var employee = employeeService.GetEmployee(id.Value);
                if (employee != null)
                {
                    var employeeContactModel = Mapper.Map<Employee, EmployeeContactTabViewModel>(employee);
                    return PartialView("EmployeeContactTabs/_CbpEmployeeContactDetail", employeeContactModel);
                }
            }
            return RedirectToAction("Index", "Employee");
        }
        [HttpPost]
        public ActionResult EmployeeContactDetailUpdate(EmployeeContactTabViewModel employeeUpdate)
        {
            if (ModelState.IsValid)
            {
                var employee = employeeService.GetEmployee(employeeUpdate.EmployeeContactId);
                if (employee != null)
                {
                    Mapper.Map<EmployeeContactTabViewModel, Employee>(employeeUpdate, employee);
                    employeeService.EditEmployee(employee);
                    return Json(new { status = "Success", messeage = "OK" });
                }
                return Json(new { status = "Fail", messeage = "Khồng tồn tại nhân viên" });
            }
            else
            {

                return Json(new { status = "Fail", messeage = "Dữ liệu nhập không hợp lệ" });
            }
        }
        #endregion

        #region Employee Job
        public ActionResult EmployeeJobCallbackPanel(int? id)
        {
            if (id.HasValue && id != 0)
            {
                var employee = employeeService.GetEmployee(id.Value);
                var employeeJobModel = Mapper.Map<Employee, EmployeeJobTabViewModel>(employee);
                employeeJobModel.CompanyList = companyService.GetCompanies();
                employeeJobModel.PositionsList = positionService.GetPositions();
                return PartialView("EmployeeJobTabs/_CbpEmployeeJob", employeeJobModel);
            }
            return PartialView("EmployeeJobTabs/_CbpEmployeeJob");
        }

        [HttpPost]
        public ActionResult AjaxEmployeeJobUpdate(EmployeeJobTabViewModel employeeUpdate)
        {
            if (ModelState.IsValid)
            {
                var employee = employeeService.GetEmployee(employeeUpdate.EmployeeJobId);
                if (employee != null)
                {                                            
                    Mapper.Map<EmployeeJobTabViewModel, Employee>(employeeUpdate, employee);
                    employeeService.EditEmployee(employee);
                    return Json(new { status = "Success", messeage = "OK" });
                }
                return Json(new { status = "Fail", messeage = "Khồng tồn tại nhân viên" });
            }
            else
            {
                return Json(new { status = "Fail", messeage = "Dữ liệu nhập không hợp lệ" });
            }
        }

        #endregion

        #region Employee Contracts

        public ActionResult EmployeeContractCallbackPanel(int? id)
        {
            if (id.HasValue && id != 0)
            {
                var employee = employeeService.GetEmployee(id.Value);
                IEnumerable<EmployeeContractsViewModel> employeeContractModel = Mapper.Map<IEnumerable<Contract>, IEnumerable<EmployeeContractsViewModel>>(employee.Contracts);
                EmployeeContractTabViewModel employeeContractTabViewModel = new EmployeeContractTabViewModel();
                employeeContractTabViewModel.ContractsViewModel = employeeContractModel;
                employeeContractTabViewModel.ContractTypeList = contractService.GetContractTypes();
                return PartialView("_CbpEmployeeContract", employeeContractTabViewModel);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EmployeesContractsGridView(int? id)
        {
            if(id.HasValue && id != 0)
            {
                var employee = employeeService.GetEmployee(id.Value);
                var contracts = Mapper.Map<IEnumerable<Contract>,IEnumerable<EmployeeContractsViewModel>>(employee.Contracts);
                ViewData["IsEnable"] = employee.Status == EmployeeStatus.Terminated ? false : true;
                return PartialView("EmployeeContractTabs/_EmployeeContractsHistoryGridViewPartial", contracts);
            }
            Employee employeeNew = new Employee();
            return PartialView("EmployeeContractTabs/_EmployeeContractsHistoryGridViewPartial", employeeNew.Contracts);
        }

          [HttpPost]
        public ActionResult AjaxEmployeeContractUpdate(EmployeeContractUpdateFormModel contractUpdate)
        {
            if (ModelState.IsValid)
            {
                var contract = contractService.GetContract(contractUpdate.ContractUpdateId);
                if (contract != null)
                {
                    Contract contractUpdated = Mapper.Map<EmployeeContractUpdateFormModel, Contract>(contractUpdate, contract);
                    contractService.EditContract(contractUpdated);
                    return Json(new { status = "Success", message = contractUpdated.ContractNo});
                }
                else
                {
                    return Json(new { status = "Fail", message = "Fail" });
                }
            }
            else {
                return Json(new { status = "Fail", message = "Fail" });
            }
            
        }
             [HttpPost]
        public ActionResult AjaxEmployeeContractAdd(EmployeeContractAddFormModel contractAdd) 
        {
            if (ModelState.IsValid)
            {
                var contractAdded = Mapper.Map<EmployeeContractAddFormModel, Contract>(contractAdd);
                try
                {
                    contractAdded.CreatedDate = DateTime.Now;
                    contractAdded.UpdatedDate = DateTime.Now;
                    contractService.CreateContract(contractAdded);
                    return Json(new { status = "Success", message = contractAdded.ContractNo });
                }
                catch(Exception ex) {
                    return Json(new { status = "Fail", message = ex.Message});
                }
                
            }
            else {
                return Json(new { status = "Fail",message = "Giá trị nhập không hợp lệ"});
            }
        }
             [HttpPost]
        public ActionResult AjaxEmployeeContractDelete(string contractDeleteList)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var objs = (object[])js.Deserialize(contractDeleteList, typeof(object));
            int deletedCount = 0;
            foreach (object id in objs)
            {
                try
                {
                    contractService.DeleteContract(int.Parse(id.ToString()));
                    deletedCount++;
                }
                catch(Exception ex) {
                    return Json(new { status = "Fail", message = ex.Message });
                }
            }
            return Json(new { status = "Success", message = string.Format("{0}/{1}",deletedCount,objs.Count())});    
        }
        public ActionResult ContractUpdateUploadAttachFileUploadControlCallbackAction()
        {

            UploadControlExtension.GetUploadedFiles("ContractUpdateUploadFile", UploadFileHelper.PdfValidationSettings, UploadFileHelper.uc_ContractFileUploadComplete);
            return null;
        }
        public ActionResult ContractAddAttachFileUploadControlCallbackAction()
        {
            UploadControlExtension.GetUploadedFiles("ContractAddUploadFile", UploadFileHelper.PdfValidationSettings, UploadFileHelper.uc_ContractFileUploadComplete);
            return null;
        }
        #endregion  

        #region Employee Insurance
        public ActionResult EmployeeInsuranceGridView(int? id)
        {
            if (id.HasValue && id != 0)
            {
                var employee = employeeService.GetEmployee(id.Value);
                double insuranceLevel = employee.Position.InsuranceRate;
                GetInsuranceByEmployeeIdRequest request = new GetInsuranceByEmployeeIdRequest() { 
                    EmployeeId = employee.Id                  
                };
                GetInsuranceByEmployeeIdResponse response = insuranceService.GetInsuranceByEmployeeId(request);
                ViewData["realSalary"] = employeeService.GetEmployeeRealSalaryAtDate(employee, DateTime.Now);
                ViewData["hasMandatory"] = response.HasMandatoryInsurance;
                ViewData["hasVoluntary"] = response.HasVoluntaryInsurance;
                ViewData["deleteStatus"] = null;
                ViewData["IsEnable"] = employee.Status == EmployeeStatus.Terminated ? false : true;

                return PartialView("EmployeeInsuranceTabs/_EmployeeInsuranceTabGridViewPartial", Mapper.Map < IEnumerable<Insurance>, IEnumerable < InsuranceGridView >>(response.InsuranceList));
            }
            return PartialView("EmployeeInsuranceTabs/_EmployeeInsuranceTabGridViewPartial");
        }
        [HttpPost]
        public ActionResult AjaxAddEmployeeMandatoryInsurance(MandatoryInsuranceAddFormView insuranceaddFormView) 
        {
            if (ModelState.IsValid) {
                MandatoryInsuranceAddRequest request = new MandatoryInsuranceAddRequest() { 
                    EmployeeId = insuranceaddFormView.EployeeId,
                    DateOfIssue = insuranceaddFormView.DateOfIssue,
                    InssuranceNo = insuranceaddFormView.InsuranceNo,
                    Amount = insuranceaddFormView.InsuranceAmount,
                    IsDefined = insuranceaddFormView.IsDefined
                };

                MandatoryInsuranceAddResponse response = insuranceService.AddMandatoryInsurance(request);
                if (response.Success)
                {
                    return Json(new { status = "Success", message = response.Message });
                }
                else 
                {
                    return Json(new { status = "Fail", message = response.Message });
                }
            }
            return Json(new { status = "Fail", message ="Dữ liệu nhập không hợp lệ" });
        }
        public ActionResult AjaxAddEmployeeVoluntaryInsurance(VoluntaryInsuranceAddFormView insuranceaddFormView)
        {
            if (ModelState.IsValid)
            {
                VoluntaryInsuranceAddRequest request = new VoluntaryInsuranceAddRequest()
                {
                    EmployeeId = insuranceaddFormView.VoluntaryEployeeId,
                    DateOfIssue = insuranceaddFormView.VoluntaryDateOfIssue,
                    InssuranceNo = insuranceaddFormView.VoluntaryInsuranceNo,
                    AttachFile = insuranceaddFormView.VoluntaryAttachFile,
                    Amount = insuranceaddFormView.VoluntaryAmount
                };
                VoluntaryInsuranceAddResponse response = insuranceService.AddVoluntaryInsurance(request);
                if (response.Success)
                {
                    return Json(new { status = "Success", message = response.Message });
                }
                else
                {
                    return Json(new { status = "Fail", message = response.Message });
                }
            }
            return Json(new { status = "Fail", message = "Dữ liệu nhập không hợp lệ" });
        }
        public ActionResult EmployeeInsuranceGridViewDelete(int Id)
        {
            InsuranceDeleteByIdRequest request = new InsuranceDeleteByIdRequest() { insuranceId = Id};
            InsuranceDeleteByIdResponse response = insuranceService.DeleteInsuranceById(request);
            Employee emp = employeeService.GetEmployee(response.EmployeeId);                      
            ViewData["realSalary"] = employeeService.GetEmployeeRealSalaryAtDate(emp, DateTime.Now);
            ViewData["hasMandatory"] = response.InsuranceByEmployee.HasMandatoryInsurance;
            ViewData["hasVoluntary"] = response.InsuranceByEmployee.HasVoluntaryInsurance;
            ViewData["deleteStatus"] = response.Status;
            ViewData["IsEnable"] = emp.Status == EmployeeStatus.Terminated ? false : true;
            return PartialView("EmployeeInsuranceTabs/_EmployeeInsuranceTabGridViewPartial", Mapper.Map < IEnumerable<Insurance>, IEnumerable < InsuranceGridView >>(response.InsuranceByEmployee.InsuranceList));
        }

        public ActionResult VoluntaryInsuranceAddAttachFileUploadControlCallbackAction() {           
            UploadControlExtension.GetUploadedFiles("voluntaryInsuranceUploadFile", UploadFileHelper.PdfValidationSettings, UploadFileHelper.uc_VoluntaryInsuranceFileUploadComplete);
            return null;
        }

        public ActionResult MadatoryInsuranceAddAttachFileUploadControlCallbackAction()
        {
            UploadControlExtension.GetUploadedFiles("mandatoryInsuranceUploadFile", UploadFileHelper.PdfValidationSettings, UploadFileHelper.uc_VoluntaryInsuranceFileUploadComplete);
            return null;
        }
        #endregion

        #region EmployeeSalary
        public ActionResult EmployeeSalaryGridView(int? id)
        {            
            //GetSalaryComponentByEmployeeResponse responseSalary = salaryService.GetSalaryComponentByEmployeeId(id);           
            if (id.HasValue && id > 0)
            {
                var empSalaryComponents = salaryService.GetSalaryComponentByEmployeeId(id.Value);
                var empSalaryComponentsView = Mapper.Map<IEnumerable<EmployeeSalaryComponents>, IEnumerable<EmployeeSalaryComponentViewModel>>(empSalaryComponents);
                Employee employee = employeeService.GetEmployee(id.Value);
                ViewData["HaveSalaryComponent"] = empSalaryComponents.Count() == 0 ? false : true;               
                ViewData["IsEnable"] = (employee == null ||  employee.Status == EmployeeStatus.Terminated ) ? false : true;
                ViewData["realSalary"] = employeeService.GetEmployeeRealSalaryAtDate(employee, DateTime.Now);
                return PartialView("EmployeeSalaryTabs/_EmployeeSalaryTabGridViewPartial", empSalaryComponentsView);
            }
            return null;// PartialView("EmployeeSalaryTabs/_EmployeeSalaryTabGridViewPartial");
        }
        public ActionResult AjaxAddEmployeeSalaryComponent(SalaryComponentAddFormView salaryComponentAddForm) {
            if (ModelState.IsValid)
            {                
                AddSalaryComponentForEmployeeRequest request = new AddSalaryComponentForEmployeeRequest()
                {
                    EmployeeId = salaryComponentAddForm.SalaryComponentEmployeeId,    
                    SalaryComponentName = salaryComponentAddForm.SalaryComponentName,
                    Amount = salaryComponentAddForm.Amount,
                    ApplyDate = salaryComponentAddForm.ApplyDate,
                    EndApplyDate = salaryComponentAddForm.EndDate,
                    IsExtra = salaryComponentAddForm.IsExtra,
                    IsMainSalary = salaryComponentAddForm.IsMainSalary,
                    SalaryPayFerequency = salaryComponentAddForm.SalaryPayFrequency,                    
                    Remark = salaryComponentAddForm.SalaryComponentRemark
                };
                AddSalaryComponentForEmployeeResponse response = salaryService.AddSalaryComponentForEmployee(request);
                if (response.Status)
                {
                    return Json(new { Status = "Success", Message = response.Message });
                }
                else
                {
                    return Json(new { Status = "Fail", Message = response.Message });
                }
            }
            else {
                return Json(new { Status = "Fail", Message = "Kiểm tra dữ liệu nhập" });
            }
            
        }


        public ActionResult EmployeeSalaryGridViewDelete(int id)
        {
            DeleteEmpSalaryComponentResponse result = salaryService.DeleteEmpSalaryComponentById(id);
            if (result.Status)
            {
                var empSalaryComponents = salaryService.GetSalaryComponentByEmployeeId(result.EmployeeId);
                var empSalaryComponentsView = Mapper.Map<IEnumerable<EmployeeSalaryComponents>, IEnumerable<EmployeeSalaryComponentViewModel>>(empSalaryComponents);
                if (empSalaryComponents.Count() == 0)
                {
                    ViewData["HaveSalaryComponent"] = false;
                }
                else
                {
                    ViewData["HaveSalaryComponent"] = true;
                }
                ViewData["EditErrorOnDelete"] = result.Message;
                ViewData["HaveSalaryComponent"] = empSalaryComponents.Count() == 0 ? false : true;
                Employee employee = employeeService.GetEmployee(result.EmployeeId);
                ViewData["IsEnable"] = (employee == null || employee.Status == EmployeeStatus.Terminated) ? false : true;
                ViewData["realSalary"] = employeeService.GetEmployeeRealSalaryAtDate(employee, DateTime.Now);
                return PartialView("EmployeeSalaryTabs/_EmployeeSalaryTabGridViewPartial", empSalaryComponentsView);
            }
            return null;                        
        }
        #endregion

        #region Employee Experience
        public ActionResult EmployeesTransferGridViewPartialByEmployeeId(int id)
        {
            var experiences = experienceService.GetExperirenceByEmployeeId(id);
            Employee emp = employeeService.GetEmployee(id);
            var experiencesViewModel = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferEmployeeGridViewModel>>(experiences);
            var companies = companyService.GetCompanies();
            ViewData["companies"] = companies;
            ViewData["IsEnable"] = (emp != null || emp.Status == EmployeeStatus.Terminated) ? false : true;
            return PartialView("EmployeesExperienceTabs/_EmployeesTransferTabGridViewPartial", experiencesViewModel);
        }

        public ActionResult EmployeesTransferTabGridViewPartialDelete(int ExperienceId)
        {
            DeleteExperienceByIdResponse response = experienceService.DeleteExperirenceById(ExperienceId);
            if (response.Status == true)
            {
                if (response.Message != "OK")
                {
                    ViewData["EditError"] = response.Message;
                }
            }
            else {
                ViewData["EditError"] = response.Message;
            }
            var experiences = experienceService.GetExperirenceByEmployeeId(response.EmployeeId);
            var experiencesViewModel = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferEmployeeGridViewModel>>(experiences);
            return PartialView("EmployeesExperienceTabs/_EmployeesTransferTabGridViewPartial", experiencesViewModel);

        }

        public ActionResult EmployeesTransferTabGridViewPartialUpdate(TransferEmployeeGridViewModel experienceUpdate)
        {            
            if (experienceUpdate.EmployeeId != 0 && experienceUpdate.ExperienceId != 0)
            {
                Experience exp =  experienceService.GetExperienceById(experienceUpdate.ExperienceId);
                if (exp != null)
                {
                    exp.AttachFile = experienceUpdate.AttachFile;
                    experienceService.SaveExperience();
                }
                else 
                {
                    ViewData["EditError"] = "Cập nhật thất bại";
                }
            }
            var experiences = experienceService.GetExperirenceByEmployeeId(experienceUpdate.EmployeeId);
            var experiencesViewModel = Mapper.Map<IEnumerable<Experience>, IEnumerable<TransferEmployeeGridViewModel>>(experiences);
            return PartialView("EmployeesExperienceTabs/_EmployeesTransferTabGridViewPartial", experiencesViewModel);

        }
        public ActionResult EmployeeTransferTabAttachFileUploadControlCallbackAction()
        {
            string[] errorText;
            UploadControlExtension.GetUploadedFiles("EmployeeTransferTabUploadFile", UploadFileHelper.PdfValidationSettings,out errorText, UploadFileHelper.uc_ExperienceTabFileUploadComplete);
            return null;
        }
        #endregion
        #endregion
    }
}