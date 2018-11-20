using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VidlyCoreApiApp.ResourceModels;
using VidlyCoreApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VidlyCoreApiApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentProvidersController : Controller
    {
        // GET: api/contentproviders
        [HttpGet]
        public ActionResult<IEnumerable<ContentProvider>> Get()
        {
            try
            {
                ContentProviderResourceModel contentProviderResource = new ContentProviderResourceModel();
                var providers = contentProviderResource.ContentProviders;

                if (providers == null)
                {
                    return NoContent();
                }

                return providers.ToList();
            }
            catch (ResourceFindAllException)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
            catch (Exception)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
        }
    }
}
