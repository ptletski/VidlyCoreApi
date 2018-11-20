using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.ResourceModels
{
    public class MpaRatingResourceModel : CommonResourceModel
    {
        public MpaRatingResourceModel() : base()
        {
        }

        public List<MpaRating> MpaRatings
        {
            get
            {
                try
                {
                    var mpaRatings = _dbContext.MpaRatings;

                    if (mpaRatings == null)
                    {
                        return null;
                    }

                    if (mpaRatings.Any() == false)
                    {
                        return null;
                    }

                    var resultList = mpaRatings.ToList();

                    return resultList;
                }
                catch (Exception exception)
                {
                    Debug.Assert(false, "Failure Gathering MPA Ratings");
                    Debug.Assert(false, exception.Message);

                    throw new ResourceFindAllException(exception.Message);
                }
            }
        }
    }
}

