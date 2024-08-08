using CompanyDatabase.Database;
using CompanyDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CompanyDbContext _context;

        public CategoryController(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                    throw new Exception("Not found?");

                return Ok(category);

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
            var category = await _context.Categories.ToListAsync();
            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}
