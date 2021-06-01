using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WellKnownController : ControllerBase
    {

        private readonly ILogger<WellKnownController> _logger;

        public WellKnownController(ILogger<WellKnownController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("Catalog work's");
        }

        [HttpGet(nameof(Authorized))]
        [Authorize]
        public IActionResult Authorized()
        {
            return Ok();
        }
    }
}
