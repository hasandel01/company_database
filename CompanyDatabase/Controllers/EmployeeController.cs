using CompanyDatabase.Database;
using CompanyDatabase.Models;
using CompanyDatabase.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly CompanyDbContext _context;

        public EmployeeController(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);

                if (employee == null)
                    throw new Exception("Not found?");

                return Ok(employee);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetAll(int branchId)
        {
            var employees = await _context.Employee.Where(e => e.BranchId == branchId).ToListAsync();


            if (employees == null)
                return BadRequest();


            return Ok(employees);
        }


        [HttpPost("branch/add/{branchId}")]
        public async Task<IActionResult> AddEmployee(int branchId, [FromBody] EmployeeDTO employeeDTO)
        {
            var branchExists = await _context.Branch.AnyAsync(b => b.Id ==  branchId);

            if (branchExists == false)
                return BadRequest("No branch is matched.");


            var employee = new Employee
            {
                Name = employeeDTO.Name,
                Phone = employeeDTO.Phone,
                BranchId = branchId,
            };

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
 
        }

    }
}
