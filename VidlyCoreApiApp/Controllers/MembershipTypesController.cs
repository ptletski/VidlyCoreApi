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
    public class MembershipTypesController : Controller
    {
        // GET: api/membershiptypes
        [HttpGet]
        public ActionResult<IEnumerable<MembershipType>> Get()
        {
            try
            {
                MembershipTypeResourceModel resource = new MembershipTypeResourceModel();
                var membershipTypes = resource.MembershipTypes;

                if (membershipTypes == null)
                {
                    return NoContent();
                }

                return membershipTypes.ToList();
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
