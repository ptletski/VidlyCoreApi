using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<int> Post([FromBody] Customer customer)
        {
            try
            {   // Apply validation to Customer via ModelState by default 
                // via ControllerBase.
                // 
                // A ModelState is a collection of name and value pairs 
                // submitted to the server during a POST request. It also 
                // contains a collection of error messages for each value. 
                // The Modelstate represents validation errors in submitted
                // HTML form values.
                //
                CustomerResourceModel customerResource = new CustomerResourceModel(); 
                var id = customerResource.Add(customer);

                if (id == 0)
                {
                    return ValidationProblem();
                }

                return new CreatedResult("/api/customers", new { Id = id });
            }
            catch (ResourceAddException)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
            catch(Exception)
            {
                return StatusCode(VidlyApiServiceFailure.ServiceFailure);
            }
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Customer customer)
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
            catch(ResourceUpdateException)
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
        public ActionResult Delete(int id)
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
