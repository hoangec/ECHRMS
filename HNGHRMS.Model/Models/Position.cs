using System;
using System.Collections.Generic;
using System.Linq;
using HNGHRMS.Infrastructure.Domain;

namespace HNGHRMS.Model.Models
{
    public class Position : EntityBase
    {
        public string PositionName { get; set; }
        public double InsuranceRate { get; set; }        
        public virtual ICollection<Employee> Employees { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
