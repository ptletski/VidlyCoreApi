using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.ResourceModels
{
    public class MembershipTypeResourceModel : CommonResourceModel
    {
        public MembershipTypeResourceModel() : base()
        {
        }

        public List<MembershipType> MembershipTypes
        {
            get
            {
                try
                {
                    var membershipTypes = _dbContext.MembershipTypes;

                    if (membershipTypes == null)
                    {
                        return null;
                    }

                    if (membershipTypes.Any() == false)
                    {
                        return null;
                    }

                    var resultList = membershipTypes.ToList();

                    return resultList;
                }
                catch (Exception exception)
                {
                    Debug.Assert(false, "Failure Gathering Membership Types");
                    Debug.Assert(false, exception.Message);

                    throw new ResourceFindAllException(exception.Message);
                }
            }
        }
    }
}
