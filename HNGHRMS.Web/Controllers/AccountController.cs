using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

using HNGHRMS.Web.Models;
using HNGHRMS.Service;
using HNGHRMS.Model.Models;
using AutoMapper;
using DevExpress.Web.Mvc;
using System.Web.Script.Serialization;
using HNGHRMS.Service.Interface;
namespace HNGHRMS.Web
{
   [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        //public AccountController()
        //    : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        //{
        //}
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ICompanyService companyService;
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController(ICompanyService companyService,IUserService userService,IRoleService roleService,UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
        {
            this.UserManager = userManager;
            this.userService = userService;
            this.roleService = roleService;
            this.RoleManager = roleManager;
            this.companyService = companyService;
        }

       
        public ActionResult Index()
        {
            var companies = companyService.GetCompanies();
            var roles = roleService.GetRoles();
            EditUserViewModel editUser = new EditUserViewModel()
            {
                Roles = roles.ToList(),
                CompanyList = companies.ToList()
            };
            ViewData["companies"] = companies;
            ViewData["EditUser"] = editUser;
            return View();

        }

        #region Login Function

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {                
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe.Value);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Tên và mật khẩu không đúng.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

    
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region Account Manage

        [HttpPost]
        public ActionResult UserAccountAdd(EditUserViewModel item)
        {
            int[] companySelected = CheckBoxListExtension.GetSelectedValues<int>("CompaniesCheckboxList");
            JavaScriptSerializer js = new JavaScriptSerializer();            
            string jsonCompanies = js.Serialize(companySelected);
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    LastName = item.LastName,
                    FirstName = item.FirstName,
                    CompaniesRole = jsonCompanies,
                    RoleId = item.RoleId
                };
                try
                {
                    var result1 = UserManager.Create(user, item.Password);
                    if(result1.Succeeded)
                    {
                        var result2 = UserManager.AddToRole(user.Id, user.RoleId);
                        if (result2.Succeeded)
                        {
                            return Json(new { status = "Succeeded", messeage = "OK" });
                        }
                        else
                        {
                            return Json(new { status = "Fail", messeage = result2.Errors.FirstOrDefault() });
                        }
                    }
                    else
                    {
                        string message = result1.Errors.FirstOrDefault();
                        if (message.Contains("is already taken"))
                        {

                            return Json(new { status = "Fail", messeage = "Người dùng " + user.UserName + " đã tồn tại trên hệ thống"});
                        }
                        else
                        {
                            return Json(new { status = "Fail", messeage = message });
                        }
                       
                    }
                                   
                }
                catch (Exception ex)
                {
                    return Json(new { status = "Fail", messeage = ex.Message});
                }

            }
            else
            {
                return Json(new { status = "Fail", messeage = "Kiểm tra nhập liệu" });
            }
        }
        [ValidateInput(false)]
        public ActionResult UserAccountGridViewPartial()
        {
            var users = userService.GetUsers();
            List<ManageUserViewModel> model = new List<ManageUserViewModel>();
            foreach (var user in users)
            {
                var u = new ManageUserViewModel(user);
                var companies = companyService.GetCompaniesByUser(user);
                u.CompaniesList = companies.ToList();
                model.Add(u);
            }
            
            return PartialView("_UserAccountGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserAccountGridViewPartialUpdate(ManageUserViewModel item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var user = UserManager.FindByName(item.UserName);                    
                    if (user != null)
                    {
                        string newPassword = item.NewPassword;
                        string newHasPassword = UserManager.PasswordHasher.HashPassword(newPassword);
                        user.PasswordHash = newHasPassword;
                        var check = UserManager.Update(user);
                        if(!check.Succeeded)
                        {
                            ViewData["EditError"] = "Lỗi cập nhập mật khẩu";
                        }
                    }
                    else
                    {
                        ViewData["EditError"] = "Tên người dùng không tồn tại";
                    }
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Lổi, kiểm tra thông tin nhập.";
           
            var users = userService.GetUsers();
            var model = new List<ManageUserViewModel>();
            foreach (var user in users)
            {
                var u = new ManageUserViewModel(user);
                var companies = companyService.GetCompaniesByUser(user);
                u.CompaniesList = companies.ToList();
                model.Add(u);
            }
            return PartialView("_UserAccountGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserAccountGridViewPartialDelete(System.String UserName)
        {
            
            if (UserName != null)
            {
                try
                {
                    var user = UserManager.FindByName(UserName);
                    if (user != null)
                    {
                        UserManager.Delete(user);
                    }
                    else
                    {
                        ViewData["EditError"] = "Tên người dùng không tồn tại";
                    }
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var users = userService.GetUsers();
            var model = new List<ManageUserViewModel>();
            foreach (var user in users)
            {
                var u = new ManageUserViewModel(user);
                var companies = companyService.GetCompaniesByUser(user);
                u.CompaniesList = companies.ToList();
                model.Add(u);
            }
            return PartialView("_UserAccountGridViewPartial",model);
        }
        #endregion

        #region Role Manage
        public ActionResult Role()
        {
            var roles = roleService.GetRoles();
            //var roles = RoleManager.Roles;
            return View(roles);
        }

        #endregion
    }
}