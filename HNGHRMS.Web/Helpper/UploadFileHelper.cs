using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web;
using System.Web.UI;
using DevExpress.Web.Mvc;
namespace HNGHRMS.Web.Helpper
{
    public class UploadFileHelper
    {
        public const string ContarctUploadDirectory = "~/Upload/Contracts/";
        public const string InsuranceUploadDirectory = "~/Upload/Insurances/";
        public const string ExperienceUploadDirectory = "~/Upload/Experiences/";
        public const string EmployeeImportUploadDirectory = "~/Upload/EmployeesImport/";

        public static readonly UploadControlValidationSettings PdfValidationSettings = new UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".pdf"},
            MaxFileSize = 1048576,
        };
        public static readonly UploadControlValidationSettings ExcelValidationSettings = new UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".xlsx", ".xls" },
            MaxFileSize = 1048576,
        };
        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                string resultFilePath = HttpContext.Current.Request.MapPath(ContarctUploadDirectory + e.UploadedFile.FileName);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(e.UploadedFile.FileName);
                }
            }
        }

        public static void uc_ContractFileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                string resultFilePath = HttpContext.Current.Request.MapPath(ContarctUploadDirectory + e.UploadedFile.FileName);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(e.UploadedFile.FileName);
                }
            }
        }
        public static void uc_ExperienceFileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                string resultFilePath = HttpContext.Current.Request.MapPath(ExperienceUploadDirectory + e.UploadedFile.FileName);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(e.UploadedFile.FileName);
                }
            }
        }
        public static void uc_ExperienceTabFileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                var employeeId = EditorExtension.GetValue<string>("txtExperienceTabEmployeeId");
                var experienceId = EditorExtension.GetValue<string>("txtExperienceTabExperienceId");
                if (employeeId != null && experienceId != null)
                {
                    string fileName = string.Format("{0}-{1}-experiences.pdf", employeeId, experienceId);
                    string resultFilePath = HttpContext.Current.Request.MapPath(ExperienceUploadDirectory + fileName);
                    e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                    IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                    if (urlResolver != null)
                    {
                        e.CallbackData = urlResolver.ResolveClientUrl(fileName);
                    }
                }
                else {
                    e.CallbackData = "error";
                }
               
            }
        }
        public static void uc_VoluntaryInsuranceFileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                var employeeId = EditorExtension.GetValue<int>("VoluntaryEployeeId");
                string fileName =  employeeId.ToString().PadLeft(5, '0') + "-INSURANCE-" + "VL" + ".pdf";
                string resultFilePath = HttpContext.Current.Request.MapPath(InsuranceUploadDirectory + fileName);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(fileName);
                }
            }
        }
        public static void uc_MadatoryInsuranceFileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                var employeeId = EditorExtension.GetValue<int>("EployeeId");
                string fileName = employeeId.ToString().PadLeft(5, '0') + "-INSURANCE-" + "MA" + ".pdf";
                string resultFilePath = HttpContext.Current.Request.MapPath(InsuranceUploadDirectory + fileName);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(fileName);
                }
            }
        }
        public static void uc_EmployeesImportFileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                var positionId = ComboBoxExtension.GetValue<int>("PositionList");
                var companyId = ComboBoxExtension.GetValue<int>("CompanyList");
                string fileName = string.Format("{0}-{1}-EMPIMPORT.xls", companyId, positionId);
                string resultFilePath = HttpContext.Current.Request.MapPath(EmployeeImportUploadDirectory + fileName);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(fileName);
                }
            }
        }
    }
}