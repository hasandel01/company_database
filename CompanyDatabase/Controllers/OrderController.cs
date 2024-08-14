using CompanyDatabase.Database;
using CompanyDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyDatabase.DTOs;

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

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetOrdersByBranchId(int branchId)
        {
            try
            {
                var branchOrders = await _context.Order
                    .Where(o => o.BranchId == branchId)
                    .ToListAsync();

                if (branchOrders == null || !branchOrders.Any())
                    return NotFound("No orders found in this branch");

                return Ok(branchOrders);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.ToString()}");
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrderByCustomerId(int customerId)
        {
            try
            {
                // Fetch orders for the specified branchId
                var customerOrders = await _context.Order
                    .Where(o => o.CustomerId == customerId)
                    .ToListAsync();

                if (customerOrders == null || !customerOrders.Any())
                    return NotFound("No orders found in this branch");


                return Ok(customerOrders);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.ToString()}");
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                // Create a new order
                var order = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    DeliveryDate = orderRequest.DeliveryDate,
                    Status = orderRequest.Status,
                    CustomerId = orderRequest.CustomerId,
                    BranchId = orderRequest.BranchId,
                };

                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                // Add order products
                foreach (var productQuantity in orderRequest.ProductQuantities)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = productQuantity.ProductId,
                        Quantity = productQuantity.Quantity
                    };

                    _context.OrderProduct.Add(orderProduct);
                }

                await _context.SaveChangesAsync();

                return Ok(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }


    }
}
