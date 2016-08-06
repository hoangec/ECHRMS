using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Enums;
using NodaTime;
using HNGHRMS.Infrastructure.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.RegularExpressions;
namespace HNGHRMS.Model.Models
{
    public class Employee : EntityBase , IAggregateRoot
    {
        // Infor
        public string EmployeeNo { 
            get {
                return this.Id.ToString().PadLeft(7, '0');
            }
        } 
        public string EmployeeCode { get; set; }
        public Identity Identity { get; set; }
        public string LastName { get; set; }
        public string  FirstName { get; set; }
        public string FullName { get{
            return string.Format("{0} {1}",this.LastName,this.FirstName);
        }}
        public string Nationality { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public byte[] Photo { get; set; }

        // Contact Details
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //Contracts
        public DateTime JoinedDate { get; set; }
        public DateTime? TerminatedDate { get; set; }
        public int PositionId { get; set; }
        public int CompanyId { get; set; }
        public string Departement { get; set; }

        public string WorkingTimeSpan
        {
            get
            {
                LocalDate endDate;
                LocalDate startDate = new LocalDate(this.JoinedDate.Year, this.JoinedDate.Month, this.JoinedDate.Day); 
                if (!this.TerminatedDate.HasValue || this.TerminatedDate == null)
                {

                    Instant now = SystemClock.Instance.Now;
                    DateTimeZone timezone = DateTimeZoneProviders.Bcl.GetSystemDefault();
                    endDate = now.InZone(timezone).Date;                                      
                }
                else {
                    endDate = new LocalDate(this.TerminatedDate.Value.Year, this.TerminatedDate.Value.Month, this.TerminatedDate.Value.Day); 
                }
                Period interval = Period.Between(startDate, endDate, PeriodUnits.YearMonthDay);
                string result = string.Format("{0} năm,{1} tháng,{2} ngày ", interval.Years, interval.Months, interval.Days);
                return result;
            }
        }
        public virtual Company Company { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
                                        
        //Insurance
        public Double MadatoryInsurance { get; set; }
        public Double VoluntaryInsurance { get; set; }
        public DateTime? MadotoryInsuranceDate { get; set; }
        public virtual ICollection<Insurance> Insurances { get; set; }
      
        //Salary
        public Double Salary { get; set; }
        public virtual ICollection<EmployeeSalaryComponents> EmployeeSalaryComponents { get; set; }
        
        //Experience
        public virtual ICollection<Experience> Experiences { get; set; }
        //
        public EmployeeStatus Status { get; set; }                       
        public virtual Termination Termination { get; set; }

        //public virtual ICollection<EmployeeEducation> EmployeeEducation { get; set; }
        public Employee()
       {
           this.Status = EmployeeStatus.Present;
       }

        public Employee(DataRow row)
        { 
            this.LastName = row["LASTNAME"].ToString();                
            this.FirstName = row["FIRSTNAME"].ToString();
            this.EmployeeCode = row["EMPLOYEECODE"].ToString();
            Identity idNO = new Identity();
            idNO.IdentityNo = row["IDENTITYNO"].ToString();
            idNO.DateOfIssue = DateTime.Parse(row["ISSUEDATE"].ToString());
            this.Identity = idNO;
            this.Gender = Boolean.Parse(row["GENDER"].ToString()) ? Gender.Male : Gender.Female;
            this.Nationality = row["NATIONAL"].ToString();
            this.Address = row["ADDRESS"].ToString();
            DateTime birthday;
            DateTime.TryParse(row["BIRTHDAY"].ToString(), out birthday);
            this.BirthDay = birthday;
            this.JoinedDate = DateTime.Parse(row["JOINDATE"].ToString());
            this.Departement = row["DEPARTMENT"].ToString();
            this.Salary = Double.Parse(row["SALARY"].ToString());
            this.CompanyId = int.Parse(row["COMPANY"].ToString());
            this.PositionId = int.Parse(row["POSITION"].ToString());

        }
        #region Validation 
        public override void Validate()
        {
            if (String.IsNullOrEmpty(this.FirstName))
                base.AddBrokenRule(new BrokenRule("EmployeeName", "Employee Name is a required value"));
            var companyCodePrefix = Regex.Replace(this.EmployeeCode, @"[\d-]", string.Empty).Trim();
            //if (this.Company.CompanyCode != companyCodePrefix)
            //{
            //    base.AddBrokenRule(new BrokenRule("EmployeeCode", "Mã nhân viên không đúng với công ty"));
            //}
        }
        #endregion  

        public EmployeeSalaryComponents GetMainSalaryComponent()
        {
            IEnumerable<EmployeeSalaryComponents> salaryComponents = this.EmployeeSalaryComponents;
            if (salaryComponents != null)
            {
                foreach (EmployeeSalaryComponents sal in salaryComponents)
                {
                    if (sal.IsMainSalary && sal.IsSalary)
                    {
                        return sal;
                    }
                }
            }
            return null;
        }

        public Insurance GetMadatoryInsurance()
        {
            IEnumerable<Insurance> insurances = this.Insurances;
            foreach (Insurance ins in insurances)
            {
                if (ins.IsMandatory)
                {
                    return ins;
                }
            }
            return null;
        }
    }
}
