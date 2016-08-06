using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service.Messaging
{
    public class CreateExperienceForEmployeeRequest
    {
        public Employee Employee { get; set; }
        public int NewCompanyId { get; set; }
        public int NewPositionId { get; set; }
        public string NewDepartement { get; set; }
        public double NewSalary { get; set; }
        public string Reason { get; set; }
        public DateTime TransferDate { get; set; }
        public string AttachFile { get; set; }
        public double InsuranceTransferAmount { get; set; }
        public bool IsInsuranceTransfer { get; set; }
        public DateTime InsuranceApplyDate { get; set; }
    }
}
