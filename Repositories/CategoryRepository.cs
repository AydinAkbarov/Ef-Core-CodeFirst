using SimpleStoreSite.Data;
using SimpleStoreSite.Models;

namespace SimpleStoreSite.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext db) : base(db) { }
}
