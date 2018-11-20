using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VidlyCoreApiApp.ResourceModels;
using VidlyCoreApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VidlyCoreApiApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        // GET: api/movies
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            try
            {
                MovieResourceModel movieResource = new MovieResourceModel();
                var movies = movieResource.Movies;

                if (movies == null)
                {
                    return NoContent();
                }

                return movies.ToList();
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

        // GET api/movies/5
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            try
            {
                MovieResourceModel movieResource = new MovieResourceModel();
                var customer = movieResource.Find(id);

                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (ResourceFindException)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
            catch (Exception)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
        }

        // POST api/movies
        [HttpPost]
        public ActionResult<int> Post([FromBody] MovieAndInventory movie)
        {
            try
            {   // Apply validation to Movie via ModelState by default 
                // via ControllerBase.
                //
                // A ModelState is a collection of name and value pairs 
                // submitted to the server during a POST request. It also 
                // contains a collection of error messages for each value. 
                // The Modelstate represents validation errors in submitted
                // HTML form values.
                //
                MovieResourceModel movieResource = new MovieResourceModel();
                var id = movieResource.Add(movie.Movie, movie.InventoryControlEntry);

                if (id == 0)
                {
                    return ValidationProblem();
                }

                return new CreatedResult("/api/movies", new { Id = id });
            }
            catch (ResourceAddException)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
            catch (Exception)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Movie movie)
        {
            try
            {
                MovieResourceModel movieResource = new MovieResourceModel();
                bool isUpdated = movieResource.Update(movie);

                if (isUpdated == false)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (ResourceUpdateException)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
            catch (Exception)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                MovieResourceModel movieResource = new MovieResourceModel();
                bool isDeleted = movieResource.Delete(id);

                if (isDeleted == false)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (ResourceDeleteException)
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
