using SimpleStoreSite.Models;

namespace SimpleStoreSite.ViewModels;

public class ProductFormVM
{
    public Product Product { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}

public class OrderFormVM
{
    public Models.Order Order { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}
