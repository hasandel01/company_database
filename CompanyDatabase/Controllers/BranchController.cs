using CompanyDatabase.Database;
using CompanyDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly CompanyDbContext _context;

        public BranchController(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var branch = await _context.Products.FindAsync(id);

                if (branch == null)
                    throw new Exception("Not found?");

                return Ok(branch);

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
            var branches = await _context.Branches.ToListAsync();
            return Ok(branches);
        }


        [HttpPost]
        public async Task<IActionResult> AddBranch(Branch branch)
        {
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return Ok(branch);
        }

    }
}
