using SimpleStoreSite.Models;

namespace SimpleStoreSite.ViewModels;

public class ProductViewModel
{
    public Product Item { get; set; } = new();
    public List<Category> AllCategories { get; set; } = new();
}

public class OrderViewModel
{
    public Order CurrentOrder { get; set; } = new();
    public List<Product> AvailableProducts { get; set; } = new();
}
