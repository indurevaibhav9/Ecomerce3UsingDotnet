using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILogger<LoginsController> _logger;
        private readonly ILogin _loginRepo;
        public LoginsController(ILogger<LoginsController> logger, ILogin login)
        {
            _logger = logger;
            _loginRepo = login;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string userName, [FromQuery] string password)
        {
            var customer = await _loginRepo.Login(userName, password);
            return Ok(customer);
        }

        [HttpPost("logout/{customerId}")]
        public async Task<IActionResult> Logout([FromRoute] int customerId)
        {
            var customer = await _loginRepo.Logout(customerId);
            if (customer == false)
            {
                return NotFound("Customer not found.");
            }
            return Ok("Logout successful.");
        }
    }


}
