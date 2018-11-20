using System;
using VidlyCoreApp.Models;

namespace VidlyCoreApp.BusinessRules
{
    public class MpaRatingRequirements
    {
        public BusinessRulesResult IsMpaRatingIdValidValue(byte mpaRatingId)
        {
            BusinessRulesResult result = new BusinessRulesResult();

            if ((mpaRatingId < MpaRating.First) || (mpaRatingId > MpaRating.Last))
            {
                result.IsErrored = true;
            }

            return result;
        }
    }
}
