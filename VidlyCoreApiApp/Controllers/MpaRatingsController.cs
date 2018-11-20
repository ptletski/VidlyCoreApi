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
    public class MpaRatingsController : Controller
    {
        // GET: api/mparatings
        [HttpGet]
        public ActionResult<IEnumerable<MpaRating>> Get()
        {
            try
            {
                MpaRatingResourceModel resource = new MpaRatingResourceModel();
                var mpaRatings = resource.MpaRatings;

                if (mpaRatings == null)
                {
                    return NoContent();
                }

                return mpaRatings.ToList();
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
