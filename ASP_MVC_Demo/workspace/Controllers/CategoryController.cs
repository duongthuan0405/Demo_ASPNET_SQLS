using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workspace.Data;
using workspace.Models;

namespace workspace.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(AppDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("CategoryController.Index");
            return View(await _context.Categories.ToListAsync());
        }


        #region Create
        
        public IActionResult Create()
        {
            _logger.LogInformation("CategoryController.Create");
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            _logger.LogInformation("CategoryController.CreateCategory");
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #endregion Create


        # region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("CategoryController.Edit");
            if (id == null) return NotFound();
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, Category category)
        {
            _logger.LogInformation("CategoryController.EditCategory");
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #endregion Edit


        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("CategoryController.Delete");
            if (id == null) return NotFound();
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("CategoryController.DeleteConfirmed");
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion Delete
    }
}
