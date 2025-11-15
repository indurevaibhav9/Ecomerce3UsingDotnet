using FrontendByDotnetMVC.Models.Inventory;
using FrontendByDotnteMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendByDotnetMVC.Controllers
{
    public class InventoryController : Controller
    {
        private readonly FrontendByDotnteMVC.Services.InventoryService _inventoryService;
        public InventoryController(FrontendByDotnteMVC.Services.InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _inventoryService.GetInventoryAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool result = await _inventoryService.CreateInventoryAsync(model);

            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create inventory.");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _inventoryService.GetInventoryByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, InventoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool result = await _inventoryService.UpdateInventoryAsync(id, model);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to update inventory.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _inventoryService.GetInventoryByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _inventoryService.DeleteInventoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
