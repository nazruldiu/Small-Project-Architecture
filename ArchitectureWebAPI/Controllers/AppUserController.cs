using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok("Here I am");
        }
    }
}
