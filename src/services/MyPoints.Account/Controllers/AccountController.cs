using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Domain.Commands.Input;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyPoints.Account.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAccountContext _context;

        public AccountController(IMediator mediator, IAccountContext context)
        {
            _mediator = mediator;
            _context = context;
        }
  

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var account = await _context.Account.GetByUserIdAsync(id);

            if (account is null)
            {
                return NotFound(account);
            }

            return Ok(account);

        }

    }
}
