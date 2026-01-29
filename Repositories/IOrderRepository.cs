using SimpleStoreSite.Models;

namespace SimpleStoreSite.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetAllWithProductAsync();
    Task<Order?> GetWithProductAsync(int id);
}
