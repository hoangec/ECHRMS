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
using HNGHRMS.Web.Extensions;
using HNGHRMS.Web.Helpper;
using HNGHRMS.Service.Messaging;
using HNGHRMS.Service.Interface;
using HNGHRMS.Service.Implementations;
using System.Data;
using System.Data.OleDb;
namespace HNGHRMS.Web.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class GeneralController : Controller
    {
        private readonly IUserService userService;
        private readonly ICompanyService companyService;        
        private readonly IPositionService positionService;
        private readonly IEmployeeService employeeService;
        private readonly IContractService contractService;
        private readonly ISalaryService salaryService;
        public GeneralController(ICompanyService companyService, IUserService userService, IPositionService positionService,IEmployeeService employeeService, IContractService contractService,ISalaryService salaryService)
        {
            this.userService = userService;
            this.companyService = companyService;
            this.positionService = positionService;
            this.employeeService = employeeService;
            this.contractService = contractService;
            this.salaryService = salaryService;
        }

        #region Company Config
        //
        // GET: /Company/
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult CompanyGridViewPartial()
        {
            var companies = companyService.GetCompanies();
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyConfigModel>>(companies);
            return PartialView("Company/_CompanyGridViewPartial", companiesView);
        }

        [HttpPost]
        public ActionResult CompanyAdd(CompanyConfigModel item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Company company = new Company() { 
                        CompanyName = item.CompanyName, 
                        CompanyInsuranceRatePercent = item.CompanyInsuranceRatePercent, 
                        LabaratorInsuranceRatePercent = item.LabaratorInsuranceRatePercent,
                        NumberCodeStarRange = item.NumberCodeStarRange,
                        NumberCodeEndRange = item.NumberCodeEndRange,
                        CompanyCode = item.CompanyCode
                    };
                    companyService.CreateCompany(company);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var companies = companyService.GetCompanies();
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyConfigModel>>(companies);
            return PartialView("Company/_CompanyGridViewPartial", companiesView);
        }

        [HttpPost]
        public ActionResult CompanyUpdate(CompanyConfigModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Company company = companyService.GetCompany(item.Id);
                    if (company != null)
                    {
                        company.CompanyName = item.CompanyName;
                        company.LabaratorInsuranceRatePercent = item.LabaratorInsuranceRatePercent;
                        company.CompanyInsuranceRatePercent = item.CompanyInsuranceRatePercent;
                        companyService.SaveCompany();
                    }
                    else
                    {
                        ViewData["EditError"] = "Không tồn tại công ty";
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var companies = companyService.GetCompanies();
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyConfigModel>>(companies);
            return PartialView("Company/_CompanyGridViewPartial", companiesView);
        }

        [HttpPost]
        public ActionResult CompanyDelete(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Company company = companyService.GetCompany(Id);
                    if (company != null)
                    {
                        companyService.DeleteCompany(company);
                    }
                    else
                    {
                        ViewData["EditError"] = "Không tồn tại công ty";
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var companies = companyService.GetCompanies();
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyConfigModel>>(companies);
            return PartialView("Company/_CompanyGridViewPartial", companiesView);
        }
        #endregion

        #region Position Config
        // GET: /Position/
        public ActionResult Position()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult PositionGridViewPartial()
        {
            var positions = positionService.GetPositions();
            var positionView = Mapper.Map<IEnumerable<Position>, IEnumerable<PositionConfigModel>>(positions);
            return PartialView("Position/_PositionGridViewPartial", positionView);
        }

        [HttpPost]
        public ActionResult PositionAdd(PositionConfigModel item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Position position = new Position()
                    {
                        PositionName = item.PositionName,
                        InsuranceRate = item.InsuranceRate
                    };
                    positionService.CreatePostion(position);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var positions = positionService.GetPositions();
            var positionView = Mapper.Map<IEnumerable<Position>, IEnumerable<PositionConfigModel>>(positions);
            return PartialView("Position/_PositionGridViewPartial", positionView);
        }

        [HttpPost]
        public ActionResult PositionUpdate(PositionConfigModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Position position = positionService.GetPosition(item.Id);
                    if (position != null)
                    {
                        position.PositionName = item.PositionName;
                        position.InsuranceRate = item.InsuranceRate;
                        positionService.SavePosition();
                    }
                    else
                    {
                        ViewData["EditError"] = "Không tồn tại vị trí chức vụ";
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var positions = positionService.GetPositions();
            var positionView = Mapper.Map<IEnumerable<Position>, IEnumerable<PositionConfigModel>>(positions);
            return PartialView("Position/_PositionGridViewPartial", positionView);
        }

        [HttpPost]
        public ActionResult PositionDelete(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Position position = positionService.GetPosition(Id);
                    if (position != null)
                    {
                        positionService.DeletePosition(position);
                    }
                    else
                    {
                        ViewData["EditError"] = "Không tồn tại vị trí chức vụ";
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var positions = positionService.GetPositions();
            var positionView = Mapper.Map<IEnumerable<Position>, IEnumerable<PositionConfigModel>>(positions);
            return PartialView("Position/_PositionGridViewPartial", positionView);
        }
        #endregion

        #region Employee Import
        public ActionResult EmployeesImport()
        {
            var companies = companyService.GetCompanies();
            var positions = positionService.GetPositions();
            //ViewData["positions"] = positions;
            //ViewData["companies"] = companies;
            EmployeesUploadViewModel empUploadViewModel = new EmployeesUploadViewModel();
            empUploadViewModel.CompanyId = (companies.Count() > 0) ? companies.FirstOrDefault().Id : 0;
            empUploadViewModel.PositionId = (positions.Count() > 0) ?  positions.FirstOrDefault().Id : 0;
            empUploadViewModel.CompanyList = companies;
            empUploadViewModel.PositionList = positions;
            return View(empUploadViewModel);
        }

        public ActionResult EmployeesImportUploadControlCallbackAction()
        {
            UploadControlExtension.GetUploadedFiles("EmployeesExcelImportUploadFile", UploadFileHelper.ExcelValidationSettings, UploadFileHelper.uc_EmployeesImportFileUploadComplete);
            return null;
        }

        public ActionResult EmployeesImportUploadFileGridViewPartial(bool? loadExcel, string fileNameString = "")      
        {
            GetEmployeesImportReadExcelFileResponse response = new GetEmployeesImportReadExcelFileResponse();
            if (loadExcel.HasValue && loadExcel.Value == true && fileNameString != "")
            {
                string[] temp = fileNameString.Split('-');
                int companyId = int.Parse(temp[0]);
                int positionId = int.Parse(temp[1]);
                string resultFilePath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Upload/EmployeesImport/{0}", fileNameString.Trim()));
                response = employeeService.GetEmployeesImportExcelFile(resultFilePath,companyId,positionId);
            }            
            ViewData["CallBackTotalRecord"] = response.TotalRecord;
            ViewData["CallBackTotalInValidCount"] = response.TotalInValidRecord;
            ViewData["CallBackTotalSalary"] = response.TotalSalary; ;
            return PartialView("EmployeesImport/_EmployeesImportUploadFileGridview",response.TableFormExcelFile);

        }

        public ActionResult EmployeesImportProcessingStatus()
        {
            var result = new { ProgressResult = EmployeeService.WriteExcelToDBProcess};
            return Json(result);            
        }

        public ActionResult EmployeesImportToDatabase(string FileNameString)
        {
            string[] temp = FileNameString.Split('-');
            int companyId = int.Parse(temp[0]);
            int positionId = int.Parse(temp[1]);
            string resultFilePath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Upload/EmployeesImport/{0}", FileNameString.Trim()));
            CreateEmployeesImportExcelToDBResponse response = employeeService.CreateEmployeesFromImportExcelToDB(resultFilePath, companyId, positionId);
            return Json(new { TotalInserted = response.TotalInserted , TotalRecord = response.TotalRecorded, TotalSalary = response.TotalSalary.ToString("C0") });

        }


        #endregion

        #region Contract Type Config
        public ActionResult Contract()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult ContractTypeGridViewPartial()
        {
            var contractType = contractService.GetContractTypes();
            var contractTypeView = Mapper.Map<IEnumerable<ContractType>, IEnumerable<ContractTypeConfigModel>>(contractType);
            return PartialView("Contract/_ContractTypeGridViewPartial", contractTypeView);
        }

        [HttpPost]
        public ActionResult ContractTypeAdd(ContractTypeConfigModel item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ContractType contractType = new ContractType(){
                        ContractTypeName = item.ContractTypeName,
                        Duration = item.Duration
                    };
                    contractService.CreateContractType(contractType);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var contractTypes = contractService.GetContractTypes();
            var contractTypeView = Mapper.Map<IEnumerable<ContractType>, IEnumerable<ContractTypeConfigModel>>(contractTypes);
            return PartialView("Contract/_ContractTypeGridViewPartial", contractTypeView);
        }

        [HttpPost]
        public ActionResult ContractTypeUpdate(ContractTypeConfigModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ContractType contractType = contractService.GetContractType(item.Id);
                    if (contractType != null)
                    {
                        contractType.ContractTypeName = item.ContractTypeName;
                        contractType.Duration = item.Duration;
                        contractService.EditContractType(contractType);
                    }
                    else
                    {
                        ViewData["EditError"] = "Không tồn tại vị trí chức vụ";
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var contractTypes = contractService.GetContractTypes();
            var contractTypeView = Mapper.Map<IEnumerable<ContractType>, IEnumerable<ContractTypeConfigModel>>(contractTypes);
            return PartialView("Contract/_ContractTypeGridViewPartial", contractTypeView);
        }

        [HttpPost]
        public ActionResult ContractTypeDelete(int Id)
        {
            if (ModelState.IsValid)
            {
                DeleteContractTypeByIdResponse result = contractService.DeleteContractTypeById(Id);
                if (result.Status == false)
                {
                    ViewData["EditErrorOnDelete"] = result.Message;
                }
            }
            else
                ViewData["EditError"] = "Xin vui lòng kiểm tra lại dữ liệu nhập.";
            var contractTypes = contractService.GetContractTypes();
            var contractTypeView = Mapper.Map<IEnumerable<ContractType>, IEnumerable<ContractTypeConfigModel>>(contractTypes);
            return PartialView("Contract/_ContractTypeGridViewPartial", contractTypeView);  
        }
        #endregion

        
    }
}