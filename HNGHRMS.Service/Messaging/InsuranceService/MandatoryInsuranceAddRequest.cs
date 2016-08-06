using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Service.Messaging
{
    public class MandatoryInsuranceAddRequest
    {
        public int EmployeeId { get; set; }
        public string InssuranceNo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public double Amount { get; set; }

        public bool IsDefined { get; set; }
    }
}
