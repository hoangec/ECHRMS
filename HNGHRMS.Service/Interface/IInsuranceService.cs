using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Service.Messaging;
namespace HNGHRMS.Service.Interface
{
    public interface IInsuranceService
    {
        GetInsuranceByEmployeeIdResponse GetInsuranceByEmployeeId(GetInsuranceByEmployeeIdRequest request);
        MandatoryInsuranceAddResponse AddMandatoryInsurance(MandatoryInsuranceAddRequest request);
        InsuranceDeleteByIdResponse DeleteInsuranceById(InsuranceDeleteByIdRequest request);

        VoluntaryInsuranceAddResponse AddVoluntaryInsurance(VoluntaryInsuranceAddRequest request);

     
        void SaveInsurance();
    }
}
