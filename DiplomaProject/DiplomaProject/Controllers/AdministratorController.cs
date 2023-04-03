using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ApiBaseController
    {
        //admins controller

        public AdministratorController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
