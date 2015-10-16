using Microsoft.AspNet.Identity.EntityFramework; using System.ComponentModel.DataAnnotations; using System.Collections.Generic; using HNGHRMS.Model.Models; namespace HNGHRMS.Web.Models {     public class ManageUserViewModel     {                 [Display(Name = "Tên đăng nhập")]         public string UserName { get; set; }          [Display(Name = "Tên")]         public string FirstName { get; set; }          [Display(Name = "Họ")]         public string LastName { get; set; }          [Display(Name = "Quyền")]         public string RoleId { get; set; }          [Display(Name = "Email")]         public string Email { get; set; }          [Required]         [StringLength(100, ErrorMessage = "{0} Phải ít nhất {2} số ký tự", MinimumLength = 6)]         [DataType(DataType.Password)]         [Display(Name = "Mật khẩu")]         public string NewPassword { get; set; }          [DataType(DataType.Password)]         [Display(Name = "Xác nhận mật khẩu")]         [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không đúng với mật khẩu.")]         public string ConfirmPassword { get; set;}          public IList<Company> CompaniesList { get; set;}         public ManageUserViewModel(ApplicationUser user)         {             UserName = user.UserName;             FirstName = user.FirstName;             LastName = user.LastName;             Email = user.Email;
            NewPassword = "";
            ConfirmPassword = "";             RoleId = user.RoleId;         }
        public ManageUserViewModel()
        {
           
        }
        [Display(Name="Công ty phần quyền")]
        public string CompaniesListName
        {
            get
            {
                string result = "";
                int counter = 1;
                int max = this.CompaniesList.Count;
                foreach (Company co in this.CompaniesList)
                {
                    if (counter < max)
                    {
                        result += co.CompanyName + "-";
                        counter++;
                    }
                    else
                    {
                        result += co.CompanyName;
                    }

                }
                return result;
            }
        }      }      public class LoginViewModel     {         //[Required(ErrorMessage="Nhập Email")]         //[Display(Name = "Email")]         //public string Email { get; set; }          [Required(ErrorMessage = "Nhập Tên")]         [Display(Name = "User name")]         public string UserName { get; set; }          [Required(ErrorMessage="Nhập mật khẩu")]         [DataType(DataType.Password)]         [Display(Name = "Password")]         public string Password { get; set; }          bool? rememberMe;         [Display(Name = "Ghi nhớ ?")]         public bool? RememberMe         {             get { return rememberMe ?? false; }             set { rememberMe = value; }         }     }       public class EditUserViewModel     {         public EditUserViewModel() { }          // Allow Initialization with an instance of ApplicationUser:         public EditUserViewModel(ApplicationUser user)         {             this.UserName = user.UserName;             this.FirstName = user.FirstName;             this.LastName = user.LastName;             this.Email = user.Email;             this.RoleId = user.RoleId;
                                  }         [Required(ErrorMessage="Nhập tên đăng nhập")]         [Display(Name = "Tên đăng nhập")]         public string UserName { get; set; }          [Required(ErrorMessage="Nhập tên")]         [Display(Name = "Tên")]         public string FirstName { get; set; }          [Required(ErrorMessage="Nhập họ")]         [Display(Name = "Họ")]         public string LastName { get; set; }          [Required(ErrorMessage="Nhập Email")]         [EmailAddress(ErrorMessage="Email không đúng")]         [Display(Name = "Email")]         public string Email { get; set; }                                   [Required(ErrorMessage="Nhập mật khẩu")]         [StringLength(100, ErrorMessage = "Mật khẩu {0} phải ít nhất {2} ký tự.", MinimumLength = 6)]         [DataType(DataType.Password)]         [Display(Name = "Mật khẩu")]         public string Password { get; set; }          [DataType(DataType.Password)]         [Display(Name = "Xác nhận mật khẩu")]         [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]         public string ConfirmPassword { get; set; }           [Display(Name = "Quyền")]         [Required(ErrorMessage="Chọn một quyền")]         public string RoleId { get; set; }          public List<ApplicationRole> Roles { get; set; }          public List<Company> CompanyList { get; set; }

             } } 