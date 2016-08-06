using System;
using System.Collections.Generic;
using System.Linq;
using HNGHRMS.Infrastructure.Domain;
namespace HNGHRMS.Model.Models
{
    public class Identity : ValueObject<Identity>
    {
        public string IdentityNo { get; set; }
        public DateTime DateOfIssue { get; set; }

        //public Identity(string IdentityNo, DateTime DateOfIssue)
        //{
        //    this.IdentityNo = IdentityNo;
        //    this.DateOfIssue = DateOfIssue;
        //}
        protected override bool EqualsCore(Identity other)
        {
            return IdentityNo == other.IdentityNo && DateOfIssue == other.DateOfIssue;
        }
        protected override int GetHashCodeCore()
        {
            return IdentityNo.GetHashCode() ;
        }
    }
}
