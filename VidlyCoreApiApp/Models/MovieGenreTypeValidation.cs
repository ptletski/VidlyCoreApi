using System;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using VidlyCoreApp.Models;
using VidlyCoreApp.BusinessRules;

namespace VidlyCoreApp.ViewModels
{
    public class MovieGenreTypeValidation : ValidationAttribute
    {
        public MovieGenreTypeValidation()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var movie = (Movie)validationContext.ObjectInstance;
                MovieGenreRequirements movieGenreRules = new MovieGenreRequirements();
                BusinessRulesResult result = movieGenreRules.IsMovieGenreIdValidValue(movie.MovieGenreId);

                return (result.IsErrored == true)
                    ? new ValidationResult("Provide a Genre")
                    : ValidationResult.Success;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "MovieGenreTypeValidation use is relegated to Movie model.");
                Debug.Assert(false, exception.Message);
            }

            return new ValidationResult("Attribute usage applies only to type Movie model.");
        }
    }
}
