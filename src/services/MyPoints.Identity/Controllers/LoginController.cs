using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoints.Identity.Data.Interfaces;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Interfaces;
using MyPoints.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IIdentityContext _context;
        private readonly IMediator _mediator;
        private readonly IRestService _queue;

        public LoginController(IIdentityContext context, IMediator mediator, IRestService queue)
        {
            _context = context;
            _mediator = mediator;
            _queue = queue;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            _queue.SendAsync(UrlNames.VerifyAccount,"teste",RestSharp.Method.GET);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginCommand command)
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
    }
}
