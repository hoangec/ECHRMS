using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Service.ViewModels;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service.Messaging
{
    public class GetInsuranceByEmployeeIdResponse
    {
        public Insurance MadatoryInsurance { get; set; }
        public Insurance VoluntaryInsurance { get; set; }
        private List<Insurance> insuranceList;

        public IEnumerable<Insurance> InsuranceHistory{ get; set; }
        public List<Insurance> InsuranceList { get {     

            if(this.MadatoryInsurance != null)
                insuranceList.Add(this.MadatoryInsurance);
            if(this.VoluntaryInsurance != null)
                insuranceList.Add(this.VoluntaryInsurance);
            if (this.InsuranceHistory.Count() >= 0)
                insuranceList.AddRange(this.InsuranceHistory);
            return insuranceList;
        }}
        public GetInsuranceByEmployeeIdResponse()
        { 
             this.insuranceList = new List<Insurance>();
        }
        public bool HasMandatoryInsurance
        {
            get
            {
                if (this.MadatoryInsurance != null)
                    return true;
                else
                    return false;
            }
        }

        public bool HasVoluntaryInsurance
        {
            get
            {
                if (this.VoluntaryInsurance != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
