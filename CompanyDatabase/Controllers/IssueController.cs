using CompanyDatabase.Database;
using CompanyDatabase.DTOs;
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
                var issue = await _context.Issue.FindAsync(id);

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
            var issues = await _context.Issue.ToListAsync();
            return Ok(issues);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetIssuesByProductId(int productId)
        {
            try
            {
                var issues = await _context.Issue
                    .Where(i => i.ProductId == productId).ToListAsync();

                if (issues == null || !issues.Any())
                    return NotFound();

                return Ok(issues);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        [HttpPost("product/add/{productId}")]
        public async Task<IActionResult> AddIssueToProduct(int productId, [FromBody] IssueDTO issueDto)
        {
            // Check if the product exists
            var productExists = await _context.Product.AnyAsync(p => p.Id == productId);
            if (!productExists)
            {
                return NotFound("Product not found");
            }

            // Map IssueDTO to Issue
            var issue = new Issue
            {
                Title = issueDto.Title,
                Description = issueDto.Description,
                ReportedDate = issueDto.ReportedDate,
                ProductId = productId,
            };

            // Add the issue to the database
            _context.Issue.Add(issue);
            await _context.SaveChangesAsync();

            return Ok(issue);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var issue = await _context.Issue.FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }

            _context.Issue.Remove(issue);
            await _context.SaveChangesAsync();

            return Ok("Item is deleted."); ;


        }
    }
}
