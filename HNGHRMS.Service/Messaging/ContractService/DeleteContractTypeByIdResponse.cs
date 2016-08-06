using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Service.Messaging
{
    public class DeleteContractTypeByIdResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int NumOfContractsUsed { get; set; }

        public DeleteContractTypeByIdResponse()
        {
            Status = false;
            Message = "";
            NumOfContractsUsed = 0;
        }
    }
}
