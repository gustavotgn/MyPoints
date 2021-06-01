using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Queries;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICatalogContext _context;

        public OrderController(IMediator mediator, ICatalogContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrderCommand command)
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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _context.Order.GetAsync(userId);

            if (result!= null && result.Count>0)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
