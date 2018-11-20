using System;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using VidlyCoreApp.BusinessRules;

namespace VidlyCoreApp.Models
{
    public class MembershipTypeValidation : ValidationAttribute
    {
        public MembershipTypeValidation()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var customer = (Customer)validationContext.ObjectInstance;
                MembershipTypeRequirements membershipTypeRules = new MembershipTypeRequirements();
                BusinessRulesResult result = membershipTypeRules.IsMembershipTypeIdValidValue(customer.MembershipTypeId);

                return (result.IsErrored == true)
                    ? new ValidationResult("Provide a Membership Type")
                    : ValidationResult.Success;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "MembershipTypeValidation use is relegated to Customer model.");
                Debug.Assert(false, exception.Message);
            }

            return new ValidationResult("Attribute usage applies only to type Customer model.");
        }
    }
}
