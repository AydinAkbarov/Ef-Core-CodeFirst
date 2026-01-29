using Microsoft.AspNetCore.Mvc;
using SimpleStoreSite.Models;
using SimpleStoreSite.Repositories;
using SimpleStoreSite.ViewModels;

namespace SimpleStoreSite.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;

    public ProductsController(IProductRepository products, ICategoryRepository categories)
    {
        _products = products;
        _categories = categories;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _products.GetAllWithCategoryAsync();
        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        return View(new ProductFormVM
        {
            Product = new Product(),
            Categories = await _categories.GetAllAsync()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductFormVM vm)
    {
        if (string.IsNullOrWhiteSpace(vm.Product.Name))
            ModelState.AddModelError("Product.Name", "Name required");
        if (vm.Product.Price < 0)
            ModelState.AddModelError("Product.Price", "Price invalid");
        if (!ModelState.IsValid)
        {
            vm.Categories = await _categories.GetAllAsync();
            return View(vm);
        }

        await _products.AddAsync(vm.Product);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var p = await _products.GetByIdAsync(id);
        if (p == null) return NotFound();

        return View(new ProductFormVM
        {
            Product = p,
            Categories = await _categories.GetAllAsync()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductFormVM vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Categories = await _categories.GetAllAsync();
            return View(vm);
        }

        await _products.UpdateAsync(vm.Product);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var p = await _products.GetWithCategoryAsync(id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var p = await _products.GetByIdAsync(id);
        if (p == null) return NotFound();
        await _products.DeleteAsync(p);
        return RedirectToAction(nameof(Index));
    }
}
