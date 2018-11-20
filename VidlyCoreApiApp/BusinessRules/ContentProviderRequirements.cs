using System;
using VidlyCoreApp.Models;

namespace VidlyCoreApp.BusinessRules
{
    public class ContentProviderRequirements
    {
        public BusinessRulesResult IsContentProviderIdValidValue(int contentProviderId)
        {
            BusinessRulesResult result = new BusinessRulesResult();

            if ((contentProviderId < ContentProvider.First) || (contentProviderId > ContentProvider.Last))
            {
                result.IsErrored = true;
            }

            return result;
        }
    }
}
