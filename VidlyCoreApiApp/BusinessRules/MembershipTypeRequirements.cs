using System;
using VidlyCoreApp.Models;

namespace VidlyCoreApp.BusinessRules
{
    public class MembershipTypeRequirements
    {
        public BusinessRulesResult IsMembershipTypeIdValidValue(byte membershipTypeId)
        {
            BusinessRulesResult result = new BusinessRulesResult();

            if ((membershipTypeId < MembershipType.First) || (membershipTypeId > MembershipType.Last))
            {
                result.IsErrored = true;
            }

            return result;
        }
    }
}
