using FrontendByDotnteMVC.Models;
using FrontendByDotnteMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductService.Models;

namespace FrontendByDotnteMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly FrontendByDotnteMVC.Services.ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            FrontendByDotnteMVC.Services.ProductService productService,
            ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // ==============================================================
        // INDEX
        // ==============================================================
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Fetching all products...");
            var products = await _productService.GetAllProductsAsync();
            _logger.LogInformation("Received {Count} products", products?.Count ?? 0);

            return View(products);
        }

        // ==============================================================
        // DETAILS
        // ==============================================================
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Fetching details for product ID {Id}", id);
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {Id} not found", id);
                return NotFound();
            }

            return View(product);
        }

        // ==============================================================
        // CREATE (GET)
        // ==============================================================
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("Loading create product page.");
            await LoadCategoryDropdown();
            return View(new CreateProductDTO());
        }

        // ==============================================================
        // CREATE (POST)
        // ==============================================================
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO model)
        {
            _logger.LogInformation("POST Create Product started.");

            // Log the incoming model
            _logger.LogInformation("Incoming product model: {@Product}", model);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid for Create Product.");
                await LoadCategoryDropdown();
                return View(model);
            }

            try
            {
                var result = await _productService.CreateProductAsync(model);

                _logger.LogInformation("API CreateProductAsync result: {Result}", result);

                if (result)
                {
                    _logger.LogInformation("Product created successfully.");
                    return RedirectToAction("Index");
                }

                _logger.LogWarning("Product creation failed, API returned false.");
                ModelState.AddModelError("", "Something went wrong while creating the product.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating product.");
                ModelState.AddModelError("", "Unexpected error.");
            }

            await LoadCategoryDropdown();
            return View(model);
        }

        // ==============================================================
        // EDIT (GET)
        // ==============================================================
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Loading edit page for product ID {Id}", id);

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product not found for editing. ID {Id}", id);
                return NotFound();
            }

            await LoadCategoryDropdown();
            return View(product);
        }

        // ==============================================================
        // EDIT (POST)
        // ==============================================================
        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            _logger.LogInformation("POST Edit Product started with ID {Id}", model.Id);

            _logger.LogInformation("Incoming Edit model: {@Product}", model);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState invalid for Edit Product.");
                await LoadCategoryDropdown();
                return View(model);
            }

            var result = await _productService.UpdateProductAsync(model.Id, model);

            _logger.LogInformation("API UpdateProductAsync result: {Result}", result);

            if (result)
            {
                _logger.LogInformation("Product updated successfully.");
                return RedirectToAction("Index");
            }

            _logger.LogWarning("Product update failed for ID {Id}", model.Id);

            ModelState.AddModelError("", "Failed to update product.");
            await LoadCategoryDropdown();
            return View(model);
        }

        // ==============================================================
        // DELETE (GET)
        // ==============================================================
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Loading delete page for product ID {Id}", id);

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product not found for delete. ID {Id}", id);
                return NotFound();
            }

            return View(product);
        }

        // ==============================================================
        // DELETE (POST)
        // ==============================================================
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("Deleting product with ID {Id}", id);

            var result = await _productService.DeleteProductAsync(id);

            _logger.LogInformation("DeleteProductAsync returned {Result}", result);

            return RedirectToAction("Index");
        }

        // ==============================================================
        // CATEGORY DROPDOWN
        // ==============================================================
        private async Task LoadCategoryDropdown()
        {
            _logger.LogInformation("Fetching categories for dropdown...");

            var categories = await _productService.GetAllCategoriesAsync();

            if (categories == null || categories.Count == 0)
                _logger.LogWarning("No categories found!");

            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
        }
    }
}
