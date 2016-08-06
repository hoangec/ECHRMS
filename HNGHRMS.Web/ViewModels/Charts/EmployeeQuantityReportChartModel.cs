using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeQuantityReportChartModel
    {
         public List<QuantityChartModel> QuantityChart { get; set; }
        public int TotalQuantity { get; set; }

        public EmployeeQuantityReportChartModel()
        {
            this.QuantityChart = new List<QuantityChartModel>();
        }
    }
}