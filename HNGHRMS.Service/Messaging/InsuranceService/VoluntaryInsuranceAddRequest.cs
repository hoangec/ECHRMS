using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Service.Messaging
{
    public class VoluntaryInsuranceAddRequest
    {
        public int EmployeeId { get; set; }
        public string InssuranceNo { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfIssue { get; set; }

        public string AttachFile { get; set; }
    }
}
