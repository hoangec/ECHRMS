using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HNGHRMS.Web.Models;
using HNGHRMS.Service;
using HNGHRMS.Model.Models;
using HNGHRMS.Web.ViewModels;
using AutoMapper;
namespace HNGHRMS.Web.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CompanyController : Controller
    {
        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        public CompanyController(ICompanyService companyService,IUserService userService)
        {
            this.userService = userService;
            this.companyService = companyService;
        }

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
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyManageModel>>(companies);
            return PartialView("_CompanyGridViewPartial", companiesView);
        }

          [HttpPost]
        public ActionResult CompanyAdd(CompanyManageModel item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    Company company = new Company(item.CompanyName);
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
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyManageModel>>(companies);
            return PartialView("_CompanyGridViewPartial", companiesView);
        }

        [HttpPost]
        public ActionResult CompanyUpdate(CompanyManageModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Company company = companyService.GetCompany(item.CompanyId);
                    if(company != null)
                    {
                        company.CompanyName = item.CompanyName;
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
                ViewData["EditError"] = "Please, correct all errors.";
            var companies = companyService.GetCompanies();
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyManageModel>>(companies);
            return PartialView("_CompanyGridViewPartial", companiesView);
        }

        [HttpPost]
        public ActionResult CompanyDelete(int CompanyId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Company company = companyService.GetCompany(CompanyId);
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
            var companiesView = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyManageModel>>(companies);
            return PartialView("_CompanyGridViewPartial", companiesView);
        }
	}
}