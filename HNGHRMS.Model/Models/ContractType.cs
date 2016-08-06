using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Infrastructure.Domain;
namespace HNGHRMS.Model.Models
{
    public class ContractType : EntityBase
    {
        public String ContractTypeName { get; set; }
        public int Duration { get; set; }
        public virtual ICollection<Contract> Contracts {get;set;}
        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
