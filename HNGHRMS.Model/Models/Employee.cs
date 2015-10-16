using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Enums;
namespace HNGHRMS.Model.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }       
        public string Name { get; set; }
//        public bool Gender { get; set; }
        public Gender Gender { get; set; }
        public int CompanyId { get; set; }
        public string Address { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Departement { get; set; }
        public String Position { get; set; }
        public Double Salary { get; set; }
        public EmployeeStatus Status { get; set; }        
        public virtual Company Company { get; set; }

       public virtual Termination Termination { get; set; }


        public Employee()
       {
           this.Status = EmployeeStatus.Present;
       }

    }
}
