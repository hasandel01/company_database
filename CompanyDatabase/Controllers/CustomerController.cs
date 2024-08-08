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
    public class CustomerController : ControllerBase
    {

        private readonly CompanyDbContext _context;

        public CustomerController(CompanyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    throw new Exception("Element is not in the db?");
                }

                return Ok(customer);

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
            var customer = await _context.Customers.FindAsync(id);

            if(customer == null) 
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            return Ok("Item is deleted."); ;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
                return NotFound(customer);

            existingCustomer.Phone = customer.Phone;
            existingCustomer.Name = customer.Name;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            existingCustomer.Orders = customer.Orders;

            _context.Customers.Update(existingCustomer);
            await _context.SaveChangesAsync();

            return Ok("Item is updated");
        }


    }
}
