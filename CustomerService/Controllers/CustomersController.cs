using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.Interfaces;
using UserService.Models;
using UserService.Repository;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomer _customerRepo;
        public CustomersController(ILogger<CustomersController> logger, ICustomer customerRepo)
        {
            _logger = logger;
            _customerRepo = customerRepo;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAll()
        {
            var all = await _customerRepo.GetAllCustomers();
            return Ok(all);
        }

        //[HttpGet("GetCustomer/{customerId}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerRepo.GetCustomer(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] RegisterDto registerDto)
        {
            var customer = await _customerRepo.CreateCustomer(registerDto);
            return Ok(customer);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            var updatedCustomer = await _customerRepo.UpdateCustomer(customer);
            return Ok(updatedCustomer);
        }

        [HttpDelete("DeleteCustomer/{customerId}")]
        public IActionResult DeleteCustomer([FromRoute] string customerId)
        {
            var result = _customerRepo.DeleteCustomer(customerId);
            if (result.Result == false)
            {
                return NotFound("Customer not found.");
            }
            return Ok("Customer deleted successfully.");
        }
    }
}
