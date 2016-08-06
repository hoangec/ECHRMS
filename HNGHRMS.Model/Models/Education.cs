using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HNGHRMS.Model.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeEducation> EmployeeEducation { get; set; }
    }
}
