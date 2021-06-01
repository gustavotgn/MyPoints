using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Domain.Commands.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyPoints.Account.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAccountContext _context;

        public TransactionController(IMediator mediator, IAccountContext context)
        {
            _mediator = mediator;
            _context = context;
        }
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var account = await _context.Transaction.GetByAccountIdAsync(id);

            if (account is null)
            {
                return NotFound(account);
            }

            return Ok(account);

        }
        [HttpPost()]
        public async Task<IActionResult> AddTransaction(AddTransactionCommand command)
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
