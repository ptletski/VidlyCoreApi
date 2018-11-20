using System;
using System.Diagnostics;
using VidlyCoreApp.BusinessRules;
using System.ComponentModel.DataAnnotations;

namespace VidlyCoreApp.Models
{
    public class ContentProviderTypeValidation : ValidationAttribute
    {
        public ContentProviderTypeValidation()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var inventory = (InventoryControlEntry)validationContext.ObjectInstance;
                ContentProviderRequirements providerRequirements = new ContentProviderRequirements();
                BusinessRulesResult result = providerRequirements.IsContentProviderIdValidValue(inventory.ContentProviderId);

                return (result.IsErrored == true)
                    ? new ValidationResult("Provide Content Provider")
                    : ValidationResult.Success;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "ContentProviderTypeValidation use is relegated to Movie model.");
                Debug.Assert(false, exception.Message);
            }

            return new ValidationResult("Attribute usage applies only to type Movie model.");
        }
    }
}
