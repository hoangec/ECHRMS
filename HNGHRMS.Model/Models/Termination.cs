using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Model.Models
{
   public class Termination
    {

        public int EmployeeId { get; set; }
        
        public string Reason { get; set; }

        public DateTime TerminationDate { get; set; }
        public string Note { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
