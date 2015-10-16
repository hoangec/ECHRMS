using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace HNGHRMS.Web.Core.Validations
{
    public class DateLargerCurrentDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime selectDate = DateTime.Parse(value.ToString());
                if(selectDate > DateTime.Now)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }              
            }
            return ValidationResult.Success;
        }
    }
}
