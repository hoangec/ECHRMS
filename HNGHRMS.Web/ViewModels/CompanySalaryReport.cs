using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNGHRMS.Web.ViewModels
{
    public class CompanySalaryReport
    {
        
        public List<QuantityChartModel> SalaryChart { get; set; }
        public double TotalSalary { get; set; }

        public CompanySalaryReport()
        {
            this.SalaryChart = new List<QuantityChartModel>();
        }

    }
}