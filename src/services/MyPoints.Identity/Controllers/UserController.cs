using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPoints.Identity.Data.Interfaces;
using MyPoints.Identity.Domain.Commands.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MyPoints.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IIdentityContext _context;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IIdentityContext context, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _context.User.GetAsync(id));
        }


        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] AddUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return BadRequest(command.Notifications);
            }

            var result = await _mediator.Send(command);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("Address")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddUserAddressCommand command)
        {
            command.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            command.Validate();
            if (command.Invalid)
            {
                return BadRequest(command.Notifications);
            }

            var result = await _mediator.Send(command);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Errors);
        }
    }
}
