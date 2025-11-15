using FrontendByDotnteMVC.Models;
using FrontendByDotnteMVC.Models.Customer;
using FrontendByDotnteMVC.Services;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace FrontendByDotnteMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // ---------------------- INDEX ----------------------
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return View(customers);
        }

        // ---------------------- DETAILS ----------------------
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // ---------------------- CREATE GET ----------------------
        public IActionResult Create()
        {
            return View();
        }

        // ---------------------- CREATE POST ----------------------
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            var success = await _customerService.CreateCustomerAsync(customer);
            if (success) return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create customer.");
            return View(customer);
        }

        // ---------------------- EDIT GET ----------------------
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // ---------------------- EDIT POST ----------------------
        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            var success = await _customerService.UpdateCustomerAsync(customer);
            if (success) return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to update customer.");
            return View(customer);
        }

        // ---------------------- DELETE GET ----------------------
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // ---------------------- DELETE POST ----------------------
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _customerService.DeleteCustomerAsync(id);
            if (success) return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to delete customer.");
            return RedirectToAction("Index");
        }
    }
}
