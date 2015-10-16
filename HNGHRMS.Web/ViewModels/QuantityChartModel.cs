using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNGHRMS.Web.ViewModels
{
    public class QuantityChartModel
    {
        private string name;
        private double area;
        
        public string Name { get { return name; } }
        public double Area { get { return area; } }

        public QuantityChartModel(string Name, double Area)
        {
            this.name = Name;
            this.area = Area;
        }
    }
}