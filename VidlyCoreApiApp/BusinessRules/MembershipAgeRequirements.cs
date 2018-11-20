﻿using System;
using VidlyCoreApp.Models;

namespace VidlyCoreApp.BusinessRules
{
    public class MembershipAgeRequirements
    {
        public BusinessRulesResult IsCustomerAgeAcceptable(byte membershipTypeId, DateTime? customerBirthDate)
        {
            BusinessRulesResult result = new BusinessRulesResult();
            MembershipTypeRequirements membershipTypeRules = new MembershipTypeRequirements();

            if ((membershipTypeRules.IsMembershipTypeIdValidValue(membershipTypeId).IsErrored == true) 
                || (membershipTypeId == MembershipType.PayAsYouGo))
            {   // Birthdate need is unknown in this case.
                result.IsErrored = false;
                return result;
            }

            if (customerBirthDate == null)
            {
                result.IsErrored = true;
                result.ErrorMessage = "Birthdate is required for this memberhip type.";
                return result;
            }

            var age = DateTime.Today.Year - customerBirthDate.Value.Year;

            result.IsErrored = (age < 18);
            result.ErrorMessage = "Customer should be at least 18 years old to receive a membership.";

            return result;
        }
    }
}
