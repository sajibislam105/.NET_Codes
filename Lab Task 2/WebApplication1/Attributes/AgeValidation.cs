using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Attributes
{
    public class AgeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }            

            DateTime dateTime = new DateTime(2022, 1, 1);            
            var age = dateTime.Year - Convert.ToDateTime(value).Year;

            if (age > 18)
            {                
                return null; //success                
            }
            else
            {
                return new ValidationResult("You are not old enough. You must be 18 years to Submit");
            }

           // return ValidationResult.Success;
        }
    }
}