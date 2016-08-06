using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNGHRMS.Web.ViewModels
{
    public class CompanySalaryReportChartModel
    {
        
        public List<QuantityChartModel> SalaryChart { get; set; }
        public List<QuantityChartModel> InsuranceChart { get; set; }
        public double TotalSalary { get; set; }
        public double TotalInsurance { get; set; }
        public CompanySalaryReportChartModel()
        {
            this.SalaryChart = new List<QuantityChartModel>();
            this.InsuranceChart = new List<QuantityChartModel>();
        }

    }
}