using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyPoints.Identity.Configurations;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyPoints.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellKnownController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
