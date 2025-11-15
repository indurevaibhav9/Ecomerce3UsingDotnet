using FrontendByDotnetMVC.Models.Order;
using FrontendByDotnteMVC.Models.Order;
using FrontendByDotnteMVC.Services;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace FrontendByDotnetMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly FrontendByDotnteMVC.Services.OrderService _orderService;
        private readonly CustomerService _customerService;
        private readonly FrontendByDotnteMVC.Services.ProductService _productService;

        public OrderController(FrontendByDotnteMVC.Services.OrderService orderService, CustomerService customerService, FrontendByDotnteMVC.Services.ProductService productService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Products = await _productService.GetAllProductsAsync();
            return View(new CreateOrderDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = await _customerService.GetAllCustomersAsync();
                ViewBag.Products = await _productService.GetAllProductsAsync();
                return View(model);
            }

            var result = await _orderService.CreateOrderAsync(model);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create order");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction("Index");
        }
    }
}
