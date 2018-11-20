using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using VidlyCoreApp.Models;
using VidlyCoreApp.BusinessRules;

namespace VidlyCoreApp.ViewModels
{
    public class MpaRatingTypeValidation : ValidationAttribute
    {
        public MpaRatingTypeValidation()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var movie = (Movie)validationContext.ObjectInstance;
                MpaRatingRequirements mpaRatingRules = new MpaRatingRequirements();
                BusinessRulesResult result = mpaRatingRules.IsMpaRatingIdValidValue(movie.MpaRatingId);

                return (result.IsErrored == true)
                    ? new ValidationResult("Provide MPA Rating")
                    : ValidationResult.Success;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "MpaRatingTypeValidation use is relegated to Movie model");
                Debug.Assert(false, exception.Message);
            }

            return new ValidationResult("Attribute usage applies only to type Movie model");
        }
    }
}
