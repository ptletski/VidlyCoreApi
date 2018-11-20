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
    public class MovieGenresController : Controller
    {
        // GET: api/moviegenres
        [HttpGet]
        public ActionResult<IEnumerable<MovieGenre>> Get()
        {
            try
            {
                MovieGenreResourceModel resource = new MovieGenreResourceModel();
                var movieGenres = resource.MovieGenres;

                if (movieGenres == null)
                {
                    return NoContent();
                }

                return movieGenres.ToList();
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
