using Microsoft.EntityFrameworkCore;
using SimpleStoreSite.Data;
using SimpleStoreSite.Models;

namespace SimpleStoreSite.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext db) : base(db) { }

    public async Task<List<Order>> GetAllWithProductAsync()
        => await _db.Orders.Include(x => x.Product).ThenInclude(p => p!.Category)
            .OrderByDescending(x => x.Id).ToListAsync();

    public async Task<Order?> GetWithProductAsync(int id)
        => await _db.Orders.Include(x => x.Product).ThenInclude(p => p!.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
}
