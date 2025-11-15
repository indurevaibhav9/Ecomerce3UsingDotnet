using FrontendByDotnteMVC.Models;
using FrontendByDotnteMVC.Services;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace FrontendByDotnteMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // ========================= INDEX ============================
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        // ========================= DETAILS ============================
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // ========================= CREATE ============================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryService.CreateCategoryAsync(model);

            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create category.");
            return View(model);
        }

        // ========================= EDIT ============================
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryService.UpdateCategoryAsync(model);

            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to update category.");
            return View(model);
        }

        // ========================= DELETE ============================
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
