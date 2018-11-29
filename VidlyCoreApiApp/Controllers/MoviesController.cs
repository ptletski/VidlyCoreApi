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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<Movie> Get(int id)
        {
            try
            {
                MovieResourceModel movieResource = new MovieResourceModel();
                var movie = movieResource.Find(id);

                if (movie == null)
                {
                    return NotFound();
                }

                return movie;
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
        [ProducesResponseType(201, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] MovieAndInventory movie)
        {
            try
            {   // Apply validation to Customer via ModelState by default 
                // via ControllerBase.
                // 
                // A ModelState is a collection of name and value pairs 
                // submitted to the server during a POST request. It also 
                // contains a collection of error messages for each value. 
                // The Modelstate represents validation errors in submitted
                // HTML form values. They will result in 400 class result 
                // codes automatically sent to the client.
                MovieResourceModel movieResource = new MovieResourceModel();
                var target = movieResource.Add(movie.Movie, movie.InventoryControlEntry);

                if (target == null)
                {
                    return ValidationProblem();
                }

                return new CreatedResult($"{Request.GetRawTarget()}/{target.MovieId}", target);
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Put(int id, [FromBody] Movie movie)
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
            catch (ArgumentNullException)
            {
                return BadRequest();
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
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
