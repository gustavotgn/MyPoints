using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICatalogContext _context;

        public ProductController(ICatalogContext context)
        {
            _context = context;
        }

        [HttpGet("Available")]
        public async Task<IActionResult> Get()
        {

            var products = await _context.Product.GetAvailableAsync();

            return Ok(products);

        }
    }
}
