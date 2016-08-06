using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Model.Models
{
    public class EmployeeEducation
    {
        [Key]
        public int EmployeeEducationId { get; set; }
        public int EmployeeId { get; set; }
        public int EducationId { get; set; }
        public string UniversityName { get; set; }
        public string Major { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Education Education { get;set;}

    }
}
