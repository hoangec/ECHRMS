using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Service.ViewModels
{
    public class SalaryComponentView
    {
        public int Id { get; set; }
        public string ComponentName { get; set; }
        public bool IsExtra { get; set; }

        public string SalaryPayFrequencyName { get; set; }
    }
}
