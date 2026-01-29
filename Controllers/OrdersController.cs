using Microsoft.AspNetCore.Mvc;
using SimpleStoreSite.Models;
using SimpleStoreSite.Repositories;
using SimpleStoreSite.ViewModels;

namespace SimpleStoreSite.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderRepository _orders;
    private readonly IProductRepository _products;

    public OrdersController(IOrderRepository orders, IProductRepository products)
    {
        _orders = orders;
        _products = products;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _orders.GetAllWithProductAsync();
        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        return View(new OrderFormVM
        {
            Order = new Order { Quantity = 1 },
            Products = await _products.GetAllAsync()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderFormVM vm)
    {
        if (vm.Order.Quantity <= 0)
            ModelState.AddModelError("Order.Quantity", "Quantity invalid");

        var product = await _products.GetByIdAsync(vm.Order.ProductId);
        if (product == null)
            ModelState.AddModelError("Order.ProductId", "Product required");

        if (!ModelState.IsValid)
        {
            vm.Products = await _products.GetAllAsync();
            return View(vm);
        }

        vm.Order.UnitPrice = product!.Price;
        vm.Order.CreatedAt = DateTime.UtcNow;

        await _orders.AddAsync(vm.Order);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var o = await _orders.GetWithProductAsync(id);
        if (o == null) return NotFound();
        return View(o);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var o = await _orders.GetByIdAsync(id);
        if (o == null) return NotFound();
        await _orders.DeleteAsync(o);
        return RedirectToAction(nameof(Index));
    }
}
