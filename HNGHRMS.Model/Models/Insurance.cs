using System;
using System.Collections.Generic;
using System.Linq;
using HNGHRMS.Infrastructure.Domain;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Model.Models 
{
    public class Insurance : EntityBase ,IAggregateRoot
    {
        public string InsuranceNo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public Boolean IsMandatory { get; set; }                  
        public Double Values { get; set; }
        public Double CompanyValue { get; set; }
        public Double Amount { get; set; }
        public Double CompanyRatePercent { get; set; }
        public Double LabaratorRatePercent { get; set; }
        public string AttachFile { get; set; }
        public int EmployeeId { get; set; }
        public bool IsHistory { get; set; }
        public string HistoryCompanyName { get; set; }
        public string HistoryPositionName { get; set; }
        public virtual Employee Employee { get; set; }
        public override void Validate()
        {
            throw new NotImplementedException();
        }

    }
}
