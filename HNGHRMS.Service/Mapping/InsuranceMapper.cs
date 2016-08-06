using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HNGHRMS.Service.ViewModels;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service.Mapping
{
    public static class InsuranceMapper
    {
        public static InsuranceView ConvertToInsuranceView(this Insurance insurance,double labaratorInsuranceRate,double companyInsuranceRate,double insuranceLevel)
        {
            InsuranceView insuranceView =  Mapper.Map<Insurance, InsuranceView>(insurance);
            if(insurance.IsMandatory)
            {
                insuranceView.InsuranceType = "Bắt buộc";
                insuranceView.InsuranceLevel = insuranceLevel;
                insuranceView.Amount = insuranceLevel * labaratorInsuranceRate ;
                insuranceView.Rate = labaratorInsuranceRate;
                insuranceView.CompanyRate = companyInsuranceRate;
                insuranceView.CompanyAmount = insuranceLevel * companyInsuranceRate;                
            }else{
                insuranceView.InsuranceType = "Tự nguyện";
                insuranceView.InsuranceLevel = insurance.Values;
                insuranceView.Rate = companyInsuranceRate + labaratorInsuranceRate;
                insuranceView.Amount = insurance.Values * (companyInsuranceRate + labaratorInsuranceRate);
            }
            insuranceView.IsHistory = insurance.IsHistory;
            insuranceView.HistoryCompanyName = insurance.HistoryCompanyName;
            insuranceView.HistoryPositionName = insurance.HistoryPositionName;
            return insuranceView;
        }
    }
}
