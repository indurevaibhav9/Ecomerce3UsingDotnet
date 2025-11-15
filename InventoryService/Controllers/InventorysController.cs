using InventoryService.Clients;
using InventoryService.Interfaces;
using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventorysController : ControllerBase
    {
        private readonly IInventoryRepository _repository;
        private readonly ILogger<InventorysController> _logger;
        private readonly IProductClient _productClient;

        public InventorysController(IInventoryRepository repository,
                                    ILogger<InventorysController> logger,
                                    IProductClient productClient)
        {
            _repository = repository;
            _logger = logger;
            _productClient = productClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] Inventory inventory)
        {
            if (inventory == null)
                return BadRequest("Invalid inventory data.");

            // ✅ Validate product
            bool isValid = await _productClient.IsProductValidAsync(inventory.ProductId);
            if (!isValid)
                return BadRequest($"Product with ID {inventory.ProductId} does not exist.");

            var created = await _repository.CreateInventory(inventory);
            return CreatedAtAction(nameof(GetInventory), new { id = created.Id }, created);
        }

        // GET: api/inventorys
        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var inventories = await _repository.GetAllInventories();
            return Ok(inventories);
        }

        // GET: api/inventorys/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventory(int id)
        {
            // ✅ Validate product
            bool isValid = await _productClient.IsProductValidAsync(id);
            if (!isValid)
                return BadRequest($"Product with ID {id} does not exist.");
            var inventory = await _repository.GetInventory(id);
            if (inventory == null)
                return NotFound($"Inventory with ID {id} not found.");

            return Ok(inventory);
        }


        // PUT: api/inventorys/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] Inventory inventory)
        {
            if (id != inventory.ProductId)
                return BadRequest("Inventory ID mismatch.");

            var existing = await _repository.GetInventory(inventory.ProductId);
            if (existing == null)
                return NotFound($"Inventory with ProductId {inventory.ProductId} not found.");

            var updated = await _repository.UpdateInventory(inventory);
            return Ok(updated);
        }


        // DELETE: api/inventorys/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var result = await _repository.DeleteInventory(id);
            if (!result)
                return StatusCode(500, "Error deleting inventory record.");

            return Ok($"Inventory with ID {id} deleted successfully.");
        }
    }
}
