using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyDatabase.Database;
using CompanyDatabase.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CompanyDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CompanyDbContext _context;

        public ProductController(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }

        // Other actions...

        // New action to get products by order ID
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetProductsByOrderId(int orderId)
        {
            try
            {
                var orderProducts = await _context.OrderProduct
                    .Where(op => op.OrderId == orderId)
                    .Include(op => op.Product) // Include product details
                    .Select(op => op.Product)
                    .ToListAsync();

                if (orderProducts == null || !orderProducts.Any())
                    return NotFound("No products found for this order");

                return Ok(orderProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
