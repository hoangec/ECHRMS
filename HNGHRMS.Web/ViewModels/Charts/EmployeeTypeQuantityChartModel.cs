using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNGHRMS.Web.ViewModels
{
    public class EmployeeTypeQuantityChartModel
    {
        private string company;
        private string type;
        private int quantity;
        public string Company { get { return company; } }
        public string Types { get { return type; } }
        public int Quantity { get { return quantity; } }

        public EmployeeTypeQuantityChartModel(string Company,string Type,int Quantity)
        {
            this.company = Company;
            this.type = Type;
            this.quantity = Quantity;
        }

    }
}