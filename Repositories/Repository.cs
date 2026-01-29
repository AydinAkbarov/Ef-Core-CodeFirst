using Microsoft.EntityFrameworkCore;
using SimpleStoreSite.Data;

namespace SimpleStoreSite.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _db;
    protected readonly DbSet<T> _set;

    public Repository(AppDbContext db)
    {
        _db = db;
        _set = _db.Set<T>();
    }

    public virtual async Task<List<T>> GetAllAsync() => await _set.ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id) => await _set.FindAsync(id);

    public virtual async Task AddAsync(T entity)
    {
        _set.Add(entity);
        await _db.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _set.Update(entity);
        await _db.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _set.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
