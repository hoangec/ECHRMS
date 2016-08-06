using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Model.Enums;
using HNGHRMS.Service.ViewModels;
using AutoMapper;
namespace HNGHRMS.Service.Mapping
{
    public static class EmployeeSalaryComponentMapper
    {
        //public static EmployeeSalaryComponentViewModel ConvertToEmployeeSalaryComponnetView(this EmployeeSalaryComponents empSalaryComponent)
        //{
        //    ////EmployeeSalaryComponentViewModel empSalaryCmpView = Mapper.Map<EmployeeSalaryComponents, EmployeeSalaryComponentViewModel>(empSalaryComponent);
        //    ////empSalaryCmpView.SalaryComponentName = empSalaryComponent.SalaryComponent.ComponentName;
        //    ////if (empSalaryComponent.SalaryComponent.IsExtra)
        //    ////{
        //    ////    empSalaryCmpView.SalaryComponentType = "Điều chỉnh tăng";
        //    ////}else{
        //    ////    empSalaryCmpView.SalaryComponentType = "Điều chỉnh giảm";
        //    ////   // empSalaryCmpView.Amount = 0 - empSalaryCmpView.Amount;
        //    ////}
        //    ////if (empSalaryComponent.SalaryComponent.SalaryPayFrequency == SalaryPayFerequency.Monthly)
        //    ////{
        //    ////    empSalaryCmpView.SalaryPayFerequency = "Hàng tháng";
        //    ////}
        //    ////else if (empSalaryComponent.SalaryComponent.SalaryPayFrequency == SalaryPayFerequency.OneTime)
        //    ////{
        //    ////    empSalaryCmpView.SalaryPayFerequency = "Một lần";
        //    ////}
        //    ////else {
        //    ////    empSalaryCmpView.SalaryPayFerequency = "Khác";
        //    ////}
        //    ////return empSalaryCmpView;
        //}
    }
}
