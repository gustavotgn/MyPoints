using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPoints.Identity.Domain.Commands.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MyPoints.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MyPoints.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ILogger<UserController> logger, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _userManager = userManager;
        }

        //[HttpGet()]
        //[Authorize]
        //public async Task<IActionResult> Get()
        //{
        //    var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        //    return Ok(await _context.User.GetAsync(id));
        //}


        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);
            return Ok(result);
        }


        //[HttpPost()]
        //[AllowAnonymous]
        //public async Task<IActionResult> Add([FromBody] AddUserCommand command)
        //{
        //    command.Validate();
        //    if (command.Invalid)
        //    {
        //        return BadRequest(command.Notifications);
        //    }

        //    var result = await _mediator.Send(command);
        //    if (result.Succeeded)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.Errors);
        //}
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
