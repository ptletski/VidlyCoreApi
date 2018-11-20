using System;
using VidlyCoreApp.Models;

namespace VidlyCoreApp.BusinessRules
{
    public class MovieGenreRequirements
    {
        public BusinessRulesResult IsMovieGenreIdValidValue(byte movieGenreId)
        {
            BusinessRulesResult result = new BusinessRulesResult();

            if ((movieGenreId < MovieGenre.First) || (movieGenreId > MovieGenre.Last))
            {
                result.IsErrored = true;
            }

            return result;
        }
    }
}
