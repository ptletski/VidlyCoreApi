using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.ResourceModels
{
	public class ContentProviderResourceModel : CommonResourceModel
    {
        public ContentProviderResourceModel() : base()
        {
        }

        public List<ContentProvider> ContentProviders
        {
            get
            {
                try
                {
                    var contentProviders = _dbContext.ContentProviders;

                    if (contentProviders == null)
                    {
                        return null;
                    }

                    if (contentProviders.Any() == false)
                    {
                        return null;
                    }

                    var result = contentProviders.ToList();

                    return result;
                }
                catch (Exception exception)
                {
                    Debug.Assert(false, "Failure Gathering Content Providers");
                    Debug.Assert(false, exception.Message);

                    throw new ResourceFindAllException(exception.Message);
                }
            }
        }
    }
}
