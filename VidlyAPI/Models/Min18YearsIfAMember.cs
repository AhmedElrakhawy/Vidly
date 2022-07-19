using System;
using System.ComponentModel.DataAnnotations;
using VidlyAPI.Models.DTO;

namespace VidlyAPI.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Customer = (CustomerDto)validationContext.ObjectInstance;
            if (Customer.MemberShipTypeId == MemberShipType.UnKnown || Customer.MemberShipTypeId == MemberShipType.UnKnown)
                return ValidationResult.Success;

            if (Customer.BirthDay == null)
                return new ValidationResult("BirthDate is Required..");

            var Age = DateTime.Today.Year - Customer.BirthDay.Value.Year;

            return (Age >= 18)
                ? ValidationResult.Success :
                new ValidationResult("Customer Age Must be At Least 18 years old...");
        }
    }
}
