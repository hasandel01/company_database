using CompanyDatabase.Database;
using CompanyDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly CompanyDbContext _context;

        public IssueController(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var issue = await _context.Issues.FindAsync(id);

                if (issue == null)
                    throw new Exception("Not found?");

                return Ok(issue);

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
            var issues = await _context.Issues.ToListAsync();
            return Ok(issues);
        }


        [HttpPost]
        public async Task<IActionResult> AddIssue(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            return Ok(issue);
        }
    }
}
