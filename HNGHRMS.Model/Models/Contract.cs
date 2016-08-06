using System;
using System.Collections.Generic;
using System.Linq;
using HNGHRMS.Infrastructure.Domain;
using NodaTime;
namespace HNGHRMS.Model.Models
{
    public class Contract : EntityBase
    {
        public int EmployeeId { get; set; }
        public string ContractNo { get; set; }
        public DateTime  StartDate { get; set; }
        //public DateTime EndDate { 
        //    get {
        //        if (this.ContractType.Duration == 0)
        //            return DateTime.MaxValue;
        //        else
        //            return this.StartDate.AddMonths(this.ContractType.Duration);
        //    } 
        //}
        //public string WorkingDuration
        //{
        //    get {
        //        LocalDate startDate = new LocalDate(this.StartDate.Year, this.StartDate.Month, this.StartDate.Day);
        //        //DateTimeZone timezone = DateTimeZoneProviders.Bcl.GetSystemDefault();
        //        //LocalDate today = now.InZone(timezone).Date;
        //        LocalDate endDate = new LocalDate(this.EndDate.Year, this.EndDate.Month, this.EndDate.Day);
        //        Period interval = Period.Between(startDate, endDate, PeriodUnits.YearMonthDay);
        //        string result = string.Format("{0} năm,{1} tháng,{2} ngày ", interval.Years, interval.Months, interval.Days);
        //        return result;
        //    }
        //}
        public string ContractAttachFile { get; set; }
        public string Remark { get; set; }
        public int  ContractTypeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ContractType ContractType { get; set; }
        public override void Validate()
        {
            if (string.IsNullOrEmpty(ContractNo))
                this.AddBrokenRule(new BrokenRule("ContractNo", "Không được để trống"));            

        }
        
    }
}
