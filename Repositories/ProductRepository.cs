using Microsoft.EntityFrameworkCore;
using SimpleStoreSite.Data;
using SimpleStoreSite.Models;

namespace SimpleStoreSite.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db) { }

    public async Task<List<Product>> GetAllWithCategoryAsync()
        => await _db.Products.Include(x => x.Category).OrderBy(x => x.Id).ToListAsync();

    public async Task<Product?> GetWithCategoryAsync(int id)
        => await _db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
}
