using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Infrastructure.Domain;
namespace HNGHRMS.Model.Models
{
   public class Termination : EntityBase
    {        
        public string Reason { get; set; }
        public DateTime TerminationDate { get; set; }
        public string Note { get; set; }
        public virtual Employee Employee { get; set; }
       
        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
