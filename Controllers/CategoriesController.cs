using Microsoft.AspNetCore.Mvc;
using SimpleStoreSite.Models;
using SimpleStoreSite.Repositories;

namespace SimpleStoreSite.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryRepository _categories;

    public CategoriesController(ICategoryRepository categories)
    {
        _categories = categories;
    }

    public async Task<IActionResult> Index()
        => View(await _categories.GetAllAsync());

    public IActionResult Create() => View(new Category());

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            ModelState.AddModelError("Name", "Name required");

        if (!ModelState.IsValid) return View(category);

        await _categories.AddAsync(category);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var c = await _categories.GetByIdAsync(id);
        if (c == null) return NotFound();
        return View(c);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (!ModelState.IsValid) return View(category);

        await _categories.UpdateAsync(category);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var c = await _categories.GetByIdAsync(id);
        if (c == null) return NotFound();
        return View(c);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var c = await _categories.GetByIdAsync(id);
        if (c == null) return NotFound();
        await _categories.DeleteAsync(c);
        return RedirectToAction(nameof(Index));
    }
}
