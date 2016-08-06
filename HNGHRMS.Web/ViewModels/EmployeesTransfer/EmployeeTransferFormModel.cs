using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using HNGHRMS.Model.Enums;
using HNGHRMS.Model.Models;
using System.Globalization;
using HNGHRMS.Web.Validations;
namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeTransferFormModel
    {
        [DisplayName("Mã nhân viên")]

        public string EmployeeCode { get; set; }
        public int EmployeeId { get; set; }
        [DisplayName("Họ tên")]
        public string  Name { get; set; }
       
        public Gender Gender { get; set; }

         [DisplayName("Giới tính")]
        public string GenderName
        {
            get { 
                if(this.Gender == Model.Enums.Gender.Male)
                {
                    return "Nam";
                }
                else
                {
                    return "Nữ";
                }

            }
        }

        [DisplayName("Địa chỉ")]
        public string  Address { get; set; }
        [DisplayName("Phòng ban")]
        public string  Departement { get; set; }
        [DisplayName("Chức vụ")]
        public string PositionName { get; set; }

        [DisplayName("Mức lương")]
        public double Salary { get; set; }

        [DisplayName("Bảo hiểm")]
        public double OldMandatoryInsurance { get; set; }
          
        [DisplayName("Ngày vào làm")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime JoinedDate { get; set; }
        
        [DisplayName("Tình trạng")]
        public EmployeeStatus Status { get; set; }

        [DisplayName("Công ty")]
        public string CompanyName { get; set; }

        [DisplayName("Công ty mới")]
        public int NewCompanyId { get; set; }
         
        [DisplayName("Chức vụ mới")]
        public int NewPositionId { get; set; }
         
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]            
        [DisplayName("Ngày điều chuyển")]
        public DateTime TransferDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Ngày áp dụng bảo hiểm")]
        public DateTime InsuranceApplyDate { get; set; }           
        public IEnumerable<Company> NewCompanyList { get; set; }           
        public IEnumerable<Position> NewPositionList { get; set; }

        [DisplayName("Phòng ban mới")]
        [Required(ErrorMessage="Nhập phòng ban mới")]
        public string NewDepartement { get; set; }

        [DisplayName("Mức lương")]
        [Required(ErrorMessage="Nhập mức lương")]
        public double NewSalary { get; set; }

        [DisplayName("Mức BH")]
        [Required(ErrorMessage = "Nhập mức bảo hiểm")]
        public double InsuranceAmount { get; set; }
        
        [DisplayName("Lý do điều chuyển")]
        [Required(ErrorMessage="Nhập lý do")]
        public string Reason { get; set; }

        [DisplayName("Điều chuyển BH")]        
        public bool IsInsuranceTransfer { get; set; }        

        [DisplayName("File đính kèm")]
        public string FileAttach { get; set; }

          public EmployeeTransferFormModel(Employee Employee)
          {
              TransferDate = DateTime.Now;
              this.EmployeeCode = Employee.EmployeeCode;
              this.EmployeeId = Employee.Id;
              this.Name = Employee.FullName;
              this.CompanyName = Employee.Company.CompanyName;
              this.NewDepartement = Employee.Departement;
              this.NewCompanyId = Employee.Company.Id;
              this.NewPositionId = Employee.Position.Id;
              this.Address = Employee.Address;
              this.Gender = Employee.Gender;
              this.Departement = Employee.Departement;
              this.PositionName = Employee.Position.PositionName;
              this.JoinedDate = Employee.JoinedDate;
              this.Salary = Employee.Salary;
              this.NewSalary = this.Salary;
              this.IsInsuranceTransfer = false;
              this.InsuranceAmount = 0;
              this.InsuranceApplyDate = (Employee.GetMadatoryInsurance() != null ) ? Employee.GetMadatoryInsurance().DateOfIssue : DateTime.Now;
              this.OldMandatoryInsurance = (Employee.GetMadatoryInsurance() != null) ? Employee.GetMadatoryInsurance().Values : 0;
          }

          public EmployeeTransferFormModel()
          {
              TransferDate = DateTime.Now;
              this.InsuranceAmount = 0;
              this.IsInsuranceTransfer = false;
          }
    }
}