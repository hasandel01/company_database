using CompanyDatabase.Database;
using CompanyDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly CompanyDbContext _context;

        public OrderController(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);

                if (order == null)
                    throw new Exception("Not found?");

                return Ok(order);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }


        [HttpPost]
        public async Task<IActionResult> AddBranch(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}
