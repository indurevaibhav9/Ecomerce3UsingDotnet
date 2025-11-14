using Microsoft.AspNetCore.Mvc;
using OrderService.Client;
using OrderService.Clients;
using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IProductClient _productClient;
        private readonly IInventoryClient _inventoryClient;
        private readonly ICustomerClient _customerClient;

        public OrdersController(
            IOrderRepository orderRepository,
            ILogger<OrdersController> logger,
            IProductClient productClient,
            IInventoryClient inventoryClient,
            ICustomerClient customerClient
            )
        {
            _customerClient = customerClient;
            _orderRepository = orderRepository;
            _logger = logger;
            _productClient = productClient;
            _inventoryClient = inventoryClient;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            if (orders == null || !orders.Any())
                return NotFound("No orders found.");

            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound($"Order with ID {id} not found.");

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDto order)
        {
            if (order == null)
                return BadRequest("Order data is null.");

            try
            {
                bool result = await _customerClient.IsCustomerValidAsync(order.CustomerId);
                if (result == false) return BadRequest($"Customer with id {order.CustomerId} does not exist");
                // ✅ Step 1: Validate products and inventory before placing order
                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (var item in order.orderItemDtos)
                {
                    // --- Check if product exists ---
                    bool isProductValid = await _productClient.IsProductValidAsync(item.ProductId);
                    if (!isProductValid)
                        return BadRequest($"Product with ID {item.ProductId} does not exist.");

                    // --- Check inventory availability ---
                    bool hasStock = await _inventoryClient.HasSufficientStockAsync(item.ProductId, item.Quantity);
                    if (!hasStock)
                        return BadRequest($"Insufficient stock for Product ID {item.ProductId}.");

                    Product product = await _productClient.GetProductByIdAsync(item.ProductId);
                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        Price = product.Price,
                        ProductName = product.ProductName
                    });
                }


                Order order1 = new Order
                {
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    OrderItems = orderItems
                };

                // ✅ Step 2: Create order
                var createdOrder = await _orderRepository.CreateOrderAsync(order1);
                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, "An error occurred while creating the order.");
            }
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null || id != order.Id)
                return BadRequest("Invalid order data.");

            try
            {
                var updatedOrder = await _orderRepository.UpdateOrderAsync(id, order);
                if (updatedOrder == null)
                    return NotFound($"Order with ID {id} not found.");

                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order");
                return StatusCode(500, "An error occurred while updating the order.");
            }
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var success = await _orderRepository.DeleteOrderAsync(id);
                if (!success)
                    return NotFound($"Order with ID {id} not found or already deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order");
                return StatusCode(500, "An error occurred while deleting the order.");
            }
        }
    }
}
