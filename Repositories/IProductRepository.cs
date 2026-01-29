using SimpleStoreSite.Models;

namespace SimpleStoreSite.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllWithCategoryAsync();
    Task<Product?> GetWithCategoryAsync(int id);
}
