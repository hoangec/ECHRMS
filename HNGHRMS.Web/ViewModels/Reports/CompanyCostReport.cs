using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace HNGHRMS.Web.ViewModels
{
    public class CompanyCostReport
    {
        [DisplayName("Mã công ty")]
        public int Id { get; set; }
        [DisplayName("Tên công ty")]
        public string CompanyName { get; set; }
        [DisplayName("Qũy lương hiện tại")]
        public double CurrentSalary { get; set; }

        public double RealSalary1 { get; set; }
        public double Salary1 { get; set; }
        public double Insurance1 { get; set; }
        public double OtherCost1 { get; set; }

        public double RealSalary2 { get; set; }
        public double Salary2 { get; set; }
        public double Insurance2 { get; set; }
        public double OtherCost2 { get; set; }

        public double RealSalary3 { get; set; }
        public double Salary3 { get; set; }
        public double Insurance3 { get; set; }
        public double OtherCost3 { get; set; }

        public double RealSalary4 { get; set; }
        public double Salary4 { get; set; }
        public double Insurance4 { get; set; }
        public double OtherCost4 { get; set; }

        public double RealSalary5 { get; set; }
        public double Salary5 { get; set; }
        public double Insurance5 { get; set; }
        public double OtherCost5 { get; set; }

        public double RealSalary6 { get; set; }
        public double Salary6 { get; set; }
        public double Insurance6 { get; set; }
        public double OtherCost6 { get; set; }

        public double RealSalary7 { get; set; }
        public double Salary7 { get; set; }
        public double Insurance7 { get; set; }
        public double OtherCost7 { get; set; }

        public double RealSalary8 { get; set; }
        public double Salary8 { get; set; }
        public double Insurance8 { get; set; }
        public double OtherCost8 { get; set; }

        public double RealSalary9 { get; set; }
        public double Salary9 { get; set; }
        public double Insurance9 { get; set; }
        public double OtherCost9 { get; set; }

        public double RealSalary10 { get; set; }
        public double Salary10 { get; set; }
        public double Insurance10 { get; set; }
        public double OtherCost10 { get; set; }

        public double RealSalary11 { get; set; }
        public double Salary11 { get; set; }
        public double Insurance11 { get; set; }
        public double OtherCost11 { get; set; }

        public double RealSalary12 { get; set; }
        public double Salary12 { get; set; }
        public double Insurance12 { get; set; }
        public double OtherCost12 { get; set; }
    }
}