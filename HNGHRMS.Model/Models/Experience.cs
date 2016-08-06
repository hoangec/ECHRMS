using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Enums;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Infrastructure.Domain;
namespace HNGHRMS.Model.Models
{
    public class Experience : EntityBase, IAggregateRoot
    {
        public string OldCompanyName { get; set; }
        public string OldPositionName { get; set; }
        public string OldDepartement { get; set; }
        public DateTime OldJoinedDate { get; set; }
        public double OldSalary { get; set; }
        public double OldInsurance { get; set; }
        public string  Reason { get; set; }
        public DateTime TransferDate { get; set; }
        public string ExperienceYears  { get; set; }
        public string AttachFile { get; set; }

        public virtual Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public int PositionId { get; set; }        
        //public virtual Company Company { get; set; }
        public int CompanyId { get; set; }
        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
