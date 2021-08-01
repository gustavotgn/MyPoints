using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoints.Identity.Domain.Commands.Input;
using System.Threading.Tasks;

namespace MyPoints.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController( IMediator mediator)
        {
            _mediator = mediator;
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
