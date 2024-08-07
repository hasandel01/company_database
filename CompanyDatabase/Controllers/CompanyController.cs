using CompanyDatabase.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CompanyDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CompanyDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly CompanyDbContext _context;

        public CompanyController(CompanyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);

                if (company == null)
                {
                    throw new Exception("Element is not in the db?");
                }

                return Ok(company);

            }
            catch (Exception ex)
            {
                  Console.Write(ex.ToString()); 

                return StatusCode(StatusCodes.Status500InternalServerError, "An error is occured while processing your request!");
            }


        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if(company == null) 
                return NotFound();

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            
            return Ok("Item is deleted."); ;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Company company)
        {
            var existingCompany = await _context.Companies.FindAsync(id);

            if (existingCompany == null)
                return NotFound(company);
            existingCompany.PhoneNumber = company.PhoneNumber;
            existingCompany.Name = company.Name;
            existingCompany.Address = company.Address;
            existingCompany.Products = company.Products;
            existingCompany.Description = company.Description;
            existingCompany.Employees = company.Employees;

            _context.Companies.Update(existingCompany);
            await _context.SaveChangesAsync();

            return Ok("Item is updated");
        }


    }
}
