using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using VidlyCoreApiApp.ResourceModels;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET api/customers
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                CustomerResourceModel customerResource = new CustomerResourceModel(); 
                var customers = customerResource.Customers;

                if (customers == null)
                {
                    return NoContent();
                }

                return customers.ToList();
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

        // GET api/customers/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type=typeof(Customer))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<Customer> Get(int id)
        {
            try
            {
                CustomerResourceModel customerResource = new CustomerResourceModel(); 
                var customer = customerResource.Find(id);

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

        // POST api/customers
        [HttpPost]
        [ProducesResponseType(201, Type=typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] Customer customer)
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
                CustomerResourceModel customerResource = new CustomerResourceModel(); 
                var target = customerResource.Add(customer);

                return new CreatedResult($"{Request.GetRawTarget()}/{target.CustomerId}", target);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
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

        // PUT api/customers/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                CustomerResourceModel customerResource = new CustomerResourceModel(); 
                bool isUpdated = customerResource.Update(customer);

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

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            try
            {
                CustomerResourceModel customerResource = new CustomerResourceModel(); 
                bool isDeleted = customerResource.Delete(id);

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
